using Common.Logging.Extensions;
using Notification.Application.Commands;
using Notification.Instrastructure;
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
