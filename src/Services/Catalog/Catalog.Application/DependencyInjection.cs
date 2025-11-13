

using Catalog.Application.Features.Categories.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Register MediatR handlers (CQRS)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // Register CategoryService
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
