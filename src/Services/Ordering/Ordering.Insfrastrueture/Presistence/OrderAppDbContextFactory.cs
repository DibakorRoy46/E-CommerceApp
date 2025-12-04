

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ordering.Insfrastrueture.Presistence;

public class OrderAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // This allows EF Core to ALWAYS know how to create your DbContext at design-time
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new AppDbContext(builder.Options);
    }
}
