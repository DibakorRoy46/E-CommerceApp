using Common.Logging.Extensions;
using EventBus.Messages.Common;
using MassTransit;
using Notification.Application.Commands;
using Notification.Instrastructure;
using Notification.Instrastructure.Consumers;
using Notification.Instrastructure.Mongo;

var builder = WebApplication.CreateBuilder(args);


// Controllers
builder.Host.UseSharedSerilog();
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR (Application layer)
builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(CreatePaymentNotificationCommand).Assembly));

// Infrastructure (MongoDB, Outbox, Retry)
builder.Services.AddNotificationInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add RabbitMQ MassTransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderCreatedNotificationConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstant.OrderCreatedMessageQueue, c =>
        {
            c.ConfigureConsumer<OrderCreatedNotificationConsumer>(ctx);
        });
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<NotificationMongoContext>();

    await MongoIndexInitializer.CreateIndexesAsync(context);
}

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
