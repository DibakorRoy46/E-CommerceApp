using Common.Logging.Extensions;
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
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSharedSerilog();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(8002, o =>
    {
        o.Protocols = HttpProtocols.Http2;
    });

    // REST + Swagger
    options.ListenLocalhost(8007, o =>
    {
        o.UseHttps();
        o.Protocols = HttpProtocols.Http1;
    });
});


// -------------------- Services --------------------
builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Auth
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCouponCommand).Assembly));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CreateCouponCommandValidator).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

// -------------------- App --------------------
var app = builder.Build();

app.MigrateDatabase<Program>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// -------------------- Endpoints --------------------
app.MapGrpcService<DiscountService>();

app.MapControllers();

app.MapGet("/", () =>
    "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();
