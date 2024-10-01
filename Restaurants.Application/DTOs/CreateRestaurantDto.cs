using Restaurants.Domain;

namespace Restaurants.Application.DTOs;

public record CreateRestaurantDto(int Id,
                                  string Name,
                                  string Description,
                                  string Category,
                                  bool HasDelivery,
                                  string? ContactEmail,
                                  string? ContactNumber,
                                  string? City,
                                  string? Street,
                                  string? PostalCode)

{
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
            Address = new Address
                      {
                          City = City,
                          Street = Street,
                          PostalCode = PostalCode
                      }
        };
}