using KoBooksStoreWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace KoBooksStoreWeb.Data;
public class KoBooksStoreDbContext : DbContext
{ 
    public KoBooksStoreDbContext(DbContextOptions<KoBooksStoreDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserLogin>(entity => {
            entity.HasKey(k => k.UserLoginID);
        });
    }

}
