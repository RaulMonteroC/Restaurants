using Restaurants.Domain;

namespace Restaurants.Application.DTOs;

public record DishDto(int Id,
                      string Name,
                      string Description,
                      decimal Price,
                      int? KiloCalories,
                      int RestaurantId)
{
    public static DishDto? FromEntity(Dish? dish)
    {
        if (dish == null)
            return null;
        
        return new DishDto(dish.Id,
                           dish.Name,
                           dish.Description,
                           dish.Price,
                           dish.KiloCalories,
                           dish.RestaurantId);
    }

    public Dish ToEntity() =>
        new()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            KiloCalories = KiloCalories,
            RestaurantId = RestaurantId
        };

    public int RestaurantId { get; set; } = RestaurantId;
}