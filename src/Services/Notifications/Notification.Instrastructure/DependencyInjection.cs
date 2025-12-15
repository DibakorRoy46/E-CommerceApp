using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Interfaces.Repositories;
using Notification.Application.Interfaces.Services;
using Notification.Application.Services;
using Notification.Instrastructure.BackgroundJobs;
using Notification.Instrastructure.Mongo;
using Notification.Instrastructure.Mongo.Repositories;
using Notification.Instrastructure.Services.Senders;
using Notification.Instrastructure.Services.TemplateRendering;

namespace Notification.Instrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddNotificationInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<NotificationMongoContext>();

        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationTemplateRepository, NotificationTemplateRepository>();

        services.AddSingleton<ITemplateRenderer, SimpleTemplateRenderer>();

        services.AddScoped<INotificationSenderFactory, NotificationSenderFactory>();

        services.AddScoped<ITemplateSelectionService, DefaultTemplateSelectionService>();

        services.AddHostedService<NotificationOutboxService>();

        return services;
    }
}
