using Microsoft.EntityFrameworkCore;
using BookStore.DataAccess;

namespace BookStore.API;

public static class MigrationsExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();

        dbContext.Database.Migrate();
    }
}