namespace Restaurants.Application.DTOs;

public record UserDto(DateOnly? DateOfBirth,
                      string Nationality)
{
    public Domain.User ToEntity()
        => new()
           {
               DateOfBirth = DateOfBirth,
               Nationality = Nationality
           };
}