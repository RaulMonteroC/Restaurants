using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.User;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;

        if (user == null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(x => x.Type == ClaimTypes.Role)!.Select(x => x.Value).ToArray();
        var nationality = user.FindFirst(x => x.Type == "Nationality")!.Value;
        DateOnly dateOfBirth = DateOnly.TryParseExact(user.FindFirst(x => x.Type == "DateOfBirth")?.Value, 
                                                      "yyyy-MM-dd", 
                                                      CultureInfo.InvariantCulture,
                                                      DateTimeStyles.None, 
                                                      out dateOfBirth) ? dateOfBirth : default;

        return new CurrentUser(userId, 
                               email, 
                               roles, 
                               nationality, 
                               dateOfBirth);
    }
}