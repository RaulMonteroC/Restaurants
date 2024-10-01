using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using Restaurants.Application.Identity;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController(IIdentityService identityService) : ControllerBase
{
    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UserDto user)
    {
        await identityService.Update(user);

        return NoContent();
    }
}