using Restaurants.Domain;

namespace Restaurants.Application.DTOs;

public record RestaurantDto(int Id,
                            string Name,
                            string Description,
                            string Category,
                            bool HasDelivery,
                            string? ContactEmail,
                            string? ContactNumber,
                            DishDto?[] Dishes)

{

    public string? LogoSasUrl { get; set; }
    
    public static RestaurantDto? FromEntity(Restaurant? restaurant)
    {
        if (restaurant == null)
            return null;

        return new RestaurantDto(restaurant.Id,
                                 restaurant.Name,
                                 restaurant.Description,
                                 restaurant.Category,
                                 restaurant.HasDelivery,
                                 restaurant.ContactEmail,
                                 restaurant.ContactNumber,
                                 restaurant.Dishes.Select(DishDto.FromEntity).ToArray());
    }
    
    public Restaurant ToEntity() =>
        new()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Category = Description,
            HasDelivery = HasDelivery,
            ContactEmail = ContactEmail,
            ContactNumber = ContactNumber,
            Dishes = Dishes?.Select(x => x.ToEntity()).ToList()
        };
}