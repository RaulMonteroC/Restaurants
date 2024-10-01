using Restaurants.Application.DTOs;

namespace Restaurants.Application.Identity;

public interface IIdentityService
{
    Task Update(UserDto user);
}