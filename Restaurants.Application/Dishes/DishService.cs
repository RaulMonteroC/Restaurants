using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes;

internal class DishService(IDishRepository dishRepository,
                           IRestaurantRepository restaurantRepository,
                           ILogger<DishService> logger) : IDishService
{
    public async Task Create(DishDto dish)
    {
        logger.LogInformation($"Creating new dish {dish.Name}");

        var restaurant = restaurantRepository.Get(dish.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), dish.RestaurantId);

        await dishRepository.Create(dish.ToEntity());
    }

    public async Task<DishDto?> Get(int restaurantId, int id)
    {
        logger.LogInformation("Retrieving dish {id}, for restaurant with id: {restaurantId}", id, restaurantId);

        var restaurant = await restaurantRepository.Get(restaurantId)
                      ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        return DishDto.FromEntity(restaurant.Dishes.FirstOrDefault(x => x!.Id == id))
            ?? throw new NotFoundException(nameof(Dish), id);

    }
    
    public async Task<IEnumerable<DishDto?>> Get(int restaurantId)
    {
        logger.LogInformation("Retrieving dishes for restaurant with id {restaurantId}", restaurantId);

        var restaurant = await restaurantRepository.Get(restaurantId)
                      ?? throw new NotFoundException(nameof(restaurantId), restaurantId);

        return restaurant.Dishes.Select(DishDto.FromEntity);
    }
    
    public async Task Delete(int restaurantId)
    {
        logger.LogInformation("Deleting all dishes for restaurant with id: {restaurantId}", restaurantId);

        var restaurant = await restaurantRepository.Get(restaurantId)
                      ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        await dishRepository.Delete(restaurant.Dishes!);
    }
}