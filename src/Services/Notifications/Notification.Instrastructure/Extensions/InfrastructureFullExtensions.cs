
using Notification.Application.Interfaces;
using Notification.Instrastructure.Consumers;
using Notification.Instrastructure.Dispatcher;
using Notification.Instrastructure.Interfaces;
using Notification.Instrastructure.Messaging;
using Notification.Instrastructure.Persistence;
using Notification.Instrastructure.Projections;
using Notification.Instrastructure.Providers;
using Notification.Instrastructure.Repositories;
using Notification.Instrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Interfaces;


namespace Notification.Instrastructure.Extensions;
public static class InfrastructureFullExtensions
{
    public static IServiceCollection AddFullInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // Bind settings
        var section = config.GetSection("Mongo");
        var settings = section.Get<MongoSettings>() ?? new MongoSettings();
        services.Configure<MongoSettings>(section);

        // Mongo client & database
        var client = MongoClientFactory.Create(settings);
        services.AddSingleton(client);
        services.AddSingleton(sp => client.GetDatabase(settings.Database));

        // Register repos
        services.AddSingleton<INotificationRepository, MongoNotificationRepository>();
        services.AddSingleton<IDeliveryEventRepository, MongoDeliveryEventRepository>();
        services.AddSingleton<ITemplateRepository, MongoTemplateRepository>();
        services.AddSingleton<IOutboxRepository, MongoOutboxRepository>();

        // Bootstrapper
        services.AddSingleton<IMongoBootstrap, MongoBootstrap>();
        services.AddHostedService<MongoBootstrapHostedService>();

        // HttpClients for providers - configure BaseAddress and headers via appsettings
        services.AddHttpClient<SendGridEmailProvider>(c =>
        {
            c.BaseAddress = new Uri(config["Providers:SendGrid:BaseUrl"] ?? "https://api.sendgrid.com");
            var key = config["Providers:SendGrid:ApiKey"];
            if (!string.IsNullOrEmpty(key)) c.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
        });
        services.AddHttpClient<TwilioSmsProvider>(c =>
        {
            c.BaseAddress = new Uri(config["Providers:Twilio:BaseUrl"] ?? "https://api.twilio.com");
        });
        services.AddHttpClient<FcmPushProvider>(c =>
        {
            c.BaseAddress = new Uri(config["Providers:FCM:BaseUrl"] ?? "https://fcm.googleapis.com");
            var key = config["Providers:FCM:ServerKey"];
            if (!string.IsNullOrEmpty(key)) c.DefaultRequestHeaders.Add("Authorization", $"key={key}");
        });

        // Register providers (allow multi registration for fallback)
        services.AddSingleton<IEmailProvider, SendGridEmailProvider>();
        services.AddSingleton<ISmsProvider, TwilioSmsProvider>();
        services.AddSingleton<IPushProvider, FcmPushProvider>();
        services.AddSingleton<IProviderResolver, ProviderResolver>();

        // MassTransit (RabbitMQ)
        services.AddMassTransit(x =>
        {
            x.AddConsumer<NotificationCreatedConsumer>();
            x.AddConsumer<OutboxMessageConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var r = config.GetSection("Messaging:RabbitMq");
                cfg.Host(r["Host"] ?? "localhost", "/", h =>
                {
                    h.Username(r["User"] ?? "guest");
                    h.Password(r["Password"] ?? "guest");
                });

                cfg.ReceiveEndpoint("notification-created-queue", e =>
                {
                    e.ConfigureConsumer<NotificationCreatedConsumer>(context);
                    e.PrefetchCount = 16;
                });

                cfg.ReceiveEndpoint("outbox-events-queue", e =>
                {
                    e.ConfigureConsumer<OutboxMessageConsumer>(context);
                    e.PrefetchCount = 16;
                });
            });
        });

        services.AddSingleton<IMessagePublisher, MassTransitMessagePublisher>();

        // Hosted services
        services.AddHostedService<OutboxDispatcherHostedService>();
        // NOTE: consumers run inside MassTransit; no DB-polling worker required

        return services;
    }
}
