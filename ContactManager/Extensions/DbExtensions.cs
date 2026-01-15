using ContactManager.Data;
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
}