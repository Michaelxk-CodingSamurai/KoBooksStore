using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using KoBooksStoreWeb.Models;


namespace KoBooksStoreWeb.Data;
public static class SeedGenres
{
    public static void EnsurePopulated(IApplicationBuilder app)
    {
        KoBooksStoreDbContext context = app.ApplicationServices.CreateScope().
            ServiceProvider.GetRequiredService<KoBooksStoreDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        foreach (var genre in context.Genres)
        {
            context.Genres.Remove(genre);
        }

        context.Genres.AddRange(
            new Genre { GenreID = 1, GenreName = "Fantasy" },
            new Genre { GenreID = 2, GenreName = "Science Fiction" },
            new Genre { GenreID = 3, GenreName = "Horror" },
            new Genre { GenreID = 4, GenreName = "Historical Fiction" },
            new Genre { GenreID = 5, GenreName = "Technology" },
            new Genre { GenreID = 6, GenreName = "Biographies" }
            );

        context.SaveChanges();

        }
}
