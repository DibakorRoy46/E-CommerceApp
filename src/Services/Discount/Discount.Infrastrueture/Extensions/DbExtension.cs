using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastrueture.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var config = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();
            try
            {
                logger.LogInformation("Discount DB Migration Started");
                ApplyMigrations(config);
                logger.LogInformation("Discount DB Migration Completed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        return host;
    }

    private static void ApplyMigrations(IConfiguration config)
    {
        using var connection = new NpgsqlConnection(
            config.GetConnectionString("DefaultConnection"));

        connection.Open();

        using var transaction = connection.BeginTransaction();
        using var cmd = connection.CreateCommand();
        cmd.Transaction = transaction;

        cmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS Coupons (
            Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Name VARCHAR(500) NOT NULL,
            Code VARCHAR(50) NOT NULL UNIQUE,
            Description TEXT,
            Amount INT,
            IsActive BOOLEAN,
            CreatedBy VARCHAR(100),
            CreatedDate TIMESTAMP DEFAULT NOW(),
            ModifiedBy VARCHAR(100),
            ModifiedDate TIMESTAMP
        );";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"
        INSERT INTO Coupons (Name, Code, Description, Amount, IsActive)
        VALUES (@name, @code, @desc, @amount, @active)
        ON CONFLICT (Code) DO NOTHING;";

        cmd.Parameters.AddWithValue("name", "Adidas Quick Force Indoor Badminton Shoes");
        cmd.Parameters.AddWithValue("code", "Dis500");
        cmd.Parameters.AddWithValue("desc", "Shoe Discount");
        cmd.Parameters.AddWithValue("amount", 500);
        cmd.Parameters.AddWithValue("active", true);
        cmd.ExecuteNonQuery();

        cmd.Parameters.Clear();

        cmd.Parameters.AddWithValue("name", "Yonex VCORE Pro 100 A Tennis Racquet");
        cmd.Parameters.AddWithValue("code", "Dis700");
        cmd.Parameters.AddWithValue("desc", "Racquet Discount");
        cmd.Parameters.AddWithValue("amount", 700);
        cmd.Parameters.AddWithValue("active", true);
        cmd.ExecuteNonQuery();

        transaction.Commit();
    }

}

