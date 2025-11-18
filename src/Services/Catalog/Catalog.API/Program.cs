using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Catalog.Application.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
// Configuration
var configuration = builder.Configuration;

// Add Application services
builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(CreateProductHierarchyCommand).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(CreateProductHierarchyCommandValidator).Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IProductHierarchyRepository,
ProductHierarchyRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
