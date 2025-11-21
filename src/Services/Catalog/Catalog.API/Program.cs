using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Application.Mapping;
using Catalog.Application.Validators;
using Catalog.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Configuration
var configuration = builder.Configuration;

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

// Repositories
builder.Services.AddScoped<IProductHierarchyRepository,ProductHierarchyRepository>();
builder.Services.AddScoped<IBrandRepository,BrandRepository>();
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
