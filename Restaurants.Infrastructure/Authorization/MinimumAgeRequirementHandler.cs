using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;

namespace Restaurants.Infrastructure.Authorization;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
                                          IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser == null)
        {
            logger.LogWarning("User is not logged in");
            context.Fail();
            return Task.CompletedTask;
        }

        logger.LogInformation("User {Email}, date of birth {DoB} - Handling MinimumAgeRequirement",
                              currentUser?.Email, currentUser?.DateOfBirth);

        if (currentUser?.DateOfBirth == null)
        {
            logger.LogWarning("User date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser?.DateOfBirth.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization Completed");
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        context.Fail();
        return Task.CompletedTask;
    }
}