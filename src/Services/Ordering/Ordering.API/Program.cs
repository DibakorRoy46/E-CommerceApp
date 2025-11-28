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
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
