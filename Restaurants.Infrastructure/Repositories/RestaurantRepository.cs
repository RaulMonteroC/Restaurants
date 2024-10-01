using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantRepository(RestaurantDbContext context) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant>> GetAll() => await context.Restaurants.ToListAsync();

    public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase)
        => await context.Restaurants
                        .Where(x => string.IsNullOrEmpty(searchPhrase) || 
                                    x.Name.ToLower().Contains(searchPhrase) ||
                                    x.Description.ToLower().Contains(searchPhrase))
                        .ToListAsync();

    public async Task<Restaurant?> Get(int id) => 
        await context.Restaurants
                     .Include(x => x.Dishes)
                     .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<int> Create(Restaurant restaurant)
    {
        context.Restaurants.Add(restaurant);
        
        await context.SaveChangesAsync();

        return restaurant.Id;
    }

    public async Task Delete(Restaurant restaurant)
    {
        context.Restaurants.Remove(restaurant);

        await context.SaveChangesAsync();
    }

    public async Task Update(Restaurant restaurant)
    {
        context.Entry(restaurant).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }

    public Task SaveChangesAsync() => context.SaveChangesAsync();
}