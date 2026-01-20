using ContactManager.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Contacts.AnyAsync())
            return;

        var contacts = new List<Contact>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Dmitry Borushko",
                MobilePhone = "+375297187474",
                JobTitle = ".Net Developer",
                BirthDate = new DateTime(2001, 11, 21)
            }
        };

        await context.Contacts.AddRangeAsync(contacts);
        await context.SaveChangesAsync();
    }
}