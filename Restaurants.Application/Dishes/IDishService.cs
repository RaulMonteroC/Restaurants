using Restaurants.Application.DTOs;

namespace Restaurants.Application.Dishes;

public interface IDishService
{
    Task Create(DishDto dish);
    Task<DishDto?> Get(int restaurantId, int id);
    Task<IEnumerable<DishDto?>> Get(int restaurantId);
    Task Delete(int restaurantId);
}