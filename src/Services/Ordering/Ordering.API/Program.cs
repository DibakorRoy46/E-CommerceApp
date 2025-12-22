using Common.Logging;
using Common.Logging.Extensions;
using EventBus.Messages.Common;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Logging.Abstractions;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Commands;
using Ordering.Application.Logger;
using Ordering.Application.Repositories;
using Ordering.Application.Validators;
using Ordering.Insfrastrueture.Dispatcher;
using Ordering.Insfrastrueture.Mapping;
using Ordering.Insfrastrueture.MessageConsumer;
using Ordering.Insfrastrueture.Presistence;
using Ordering.Insfrastrueture.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Serilog
builder.Host.UseSharedSerilog();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));

builder.Services.AddControllers();

//Database Connection
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

//Add Services
builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// MediatR pipeline logging
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

// Repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Outbox Message Dispatcher
builder.Services.AddHostedService<OutboxMessageDispatcher>();

//Consume RabiitMQ Events

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();
    config.AddConsumer<PaymentSuccessConsumer>(); 
    config.AddConsumer<PaymentFailedConsumer>(); 

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });

        //Payement Success
        cfg.ReceiveEndpoint(EventBusConstant.PaymentSuccessQueue, c =>
        {
            c.ConfigureConsumer<PaymentSuccessConsumer>(ctx);
        });

        //Payment Failed
        cfg.ReceiveEndpoint(EventBusConstant.PaymentFailedQueue, c =>
        {
            c.ConfigureConsumer<PaymentFailedConsumer>(ctx);
        });
    });
});

//Register Hangfire
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.FromSeconds(15),
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        });
});
builder.Services.AddHangfireServer();
builder.Services.AddScoped<OutboxOrderCreatedNotificationDispatcher>();

var app = builder.Build();
app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<OutboxOrderCreatedNotificationDispatcher>(
    "outbox-ordercreated-publisher",job => job.ExecuteAsync(),Cron.Minutely); 

// Middleware
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
