using EventBus.Messages.Common;
using FluentValidation;
using FluentValidation.AspNetCore;
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
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    // Console
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    // File sinks (your existing)
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File("Logs/info-.txt",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .WriteTo.File("Logs/error-.txt",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
    .WriteTo.File(new RenderedCompactJsonFormatter(),
              "Logs/log-.json",
              rollingInterval: RollingInterval.Day)
    // Seq (optional)
    .WriteTo.Seq("http://localhost:5341")
    // ElasticSearch
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "catalog-api-log-{0:yyyy.MM.dd}",
        NumberOfShards = 1,
        NumberOfReplicas = 0
    })
    .CreateLogger();

builder.Host.UseSerilog();

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



var app = builder.Build();

// Middleware
app.UseSerilogRequestLogging(); // logs all HTTP requests
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
