using Microsoft.AspNetCore.Identity;
using Restaurants.Application.DTOs;
using Restaurants.Application.User;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Identity;

public class IdentityService(IUserStore<Domain.User> userStore,
                             IUserContext userContext) : IIdentityService
{
    public async Task Update(UserDto user)
    {
        var currentUser = userContext.GetCurrentUser();
        var dbUser = await userStore.FindByIdAsync(currentUser?.Id!, CancellationToken.None);

        if (dbUser == null)
        {
            throw new NotFoundException(nameof(Domain.User), Int16.Parse(currentUser?.Id!));
        }

        dbUser.DateOfBirth = user.DateOfBirth;
        dbUser.Nationality = user.Nationality;

        await userStore.UpdateAsync(dbUser, CancellationToken.None);
    }
}