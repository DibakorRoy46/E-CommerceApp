using Discount.API.Services;
using Discount.Application.Commands;
using Discount.Application.Interfaces;
using Discount.Application.Mapping;
using Discount.Application.Validators;
using Discount.Infrastrueture.Extensions;
using Discount.Infrastrueture.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(8002, o =>
    {
        o.Protocols = HttpProtocols.Http2;
    });
});
// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(CreateCouponCommand).Assembly));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CreateCouponCommandValidator).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();
app.UseRouting();

app.MigrateDatabase<Program>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(
            "Communication with gRPC endpoints must be made through a gRPC client.");
    });
});

app.Run();
