using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantUserClaimPrincipalFactory(UserManager<User> userManager,
                                                 RoleManager<IdentityRole> roleManager,
                                                 IOptions<IdentityOptions> options) 
    : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var claims = await GenerateClaimsAsync(user);

        if (user.Nationality != null)
        {
            claims.AddClaim(new Claim(AppClaimTypes.NATIONALITY, user.Nationality));
        }

        if (user.DateOfBirth != null)
        {
            claims.AddClaim(new Claim(AppClaimTypes.DATE_OF_BIRTH, user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty));
        }

        return new ClaimsPrincipal(claims);
    }
}