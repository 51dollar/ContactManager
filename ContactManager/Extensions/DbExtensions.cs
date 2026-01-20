using ContactManager.Data;
using ContactManager.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Extensions;

public static class DbExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });
    }
    
    public static async Task InitDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var logger = scope.ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("DatabaseMigration");

        try
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await db.Database.MigrateAsync();
            await DatabaseSeeder.SeedAsync(db);

            logger.LogInformation("Database migrated and seeded successfully");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Database migration or seeding failed");
            throw;
        }
    }
}