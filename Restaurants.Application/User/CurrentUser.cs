namespace Restaurants.Application.User;

public record struct CurrentUser(string Id, 
                                 string Email, 
                                 string[] Roles,
                                 string? Nationality,
                                 DateOnly DateOfBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}