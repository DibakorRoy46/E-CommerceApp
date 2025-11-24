using Catalog.API.Exceptions;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Application.Logger;
using Catalog.Application.Mapping;
using Catalog.Application.Validators;
using Catalog.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);
// Configuration
var configuration = builder.Configuration;

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

// Add Application services
builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(CreateProductHierarchyCommand).Assembly));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CreateProductHierarchyCommandValidator).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// MediatR pipeline logging
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

// Repositories
builder.Services.AddScoped<IProductHierarchyRepository,ProductHierarchyRepository>();
builder.Services.AddScoped<IBrandRepository,BrandRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSerilogRequestLogging(); // logs all HTTP requests
app.UseMiddleware<ExceptionMiddleware>(); // global exception logging
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
// Run DB migrations on startup (optional, production be cautious)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
