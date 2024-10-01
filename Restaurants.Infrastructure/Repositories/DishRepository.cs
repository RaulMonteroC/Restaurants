using Restaurants.Domain;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository(RestaurantDbContext context) : IDishRepository
{
    public async Task<int> Create(Dish dish)
    {
        context.Dishes.Add(dish);

        return await context.SaveChangesAsync();
    }

    public async Task Delete(IEnumerable<Dish> dishes)
    {
        context.Dishes.RemoveRange(dishes);

        await context.SaveChangesAsync();
    }
}