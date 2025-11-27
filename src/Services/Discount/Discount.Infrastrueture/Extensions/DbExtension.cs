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
        //using var connection = new NpgsqlConnection(config.GetValue<string>("ConnectionString:DefaultConnection"));
        //connection.Open();
        //using var cmd = new NpgsqlCommand()
        //{
        //    Connection = connection
        //};
        //cmd.CommandText = "DROP TABLE IF EXISTS Coupons";
        //cmd.ExecuteNonQuery();
        //cmd.CommandText = @"CREATE TABLE Coupons(Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY, 
        //                                        Name VARCHAR(500) NOT NULL,
        //                                        Code VARCHAR(50) NOT NULL,
        //                                        Description TEXT,
        //                                        Amount INT,
        //                                        IsActive INT ,
        //                                        CreatedBy VARCHAR(100),
        //                                        CreatedDate TIMESTAMP,
        //                                        ModifiedBy VARCHAR(100),
        //                                        ModifiedDate TIMESTAMP
        //                                        )";
        //cmd.ExecuteNonQuery();

        //cmd.CommandText = "INSERT INTO Coupons(Name,Code, Description, Amount,IsActive) VALUES('Adidas Quick Force Indoor Badminton Shoes','Dis500', 'Shoe Discount', 500,1);";
        //cmd.ExecuteNonQuery();

        //cmd.CommandText = "INSERT INTO Coupons(Name,Code, Description, Amount,IsActive) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)','Dis700', 'Racquet Discount', 700,1);";
        //cmd.ExecuteNonQuery();
    }
}

