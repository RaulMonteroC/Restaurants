using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) 
    : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
                    .OwnsOne(x => x.Address);

        modelBuilder.Entity<Restaurant>()
                    .HasMany(x => x.Dishes)
                    .WithOne()
                    .HasForeignKey(x => x.RestaurantId);
    }
}