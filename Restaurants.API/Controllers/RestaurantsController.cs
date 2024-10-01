using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Queries;
using Restaurants.Application.DTOs;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = UserRoles.USER)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]
    public async Task<IActionResult> Get([FromQuery] GetAllRestaurantsQuery query) => Ok(await restaurantService.GetAll(query));

    [HttpGet("{id}")]
    // [Authorize(Policy = PolicyNames.AT_LEAST_20)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var restaurant = await restaurantService.Get(id);

        return Ok(restaurant);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantDto))]
    public async Task<IActionResult> Post([FromBody] CreateRestaurantDto restaurant)
    {
        var id = await restaurantService.Create(restaurant);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await restaurantService.Delete(id);
        
        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CreateRestaurantDto restaurant)
    {
        await restaurantService.Update(restaurant);

        return Ok();
    }
    
    [HttpPost("{id}/logo")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateLogo([FromRoute] int id, IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        await restaurantService.UploadRestaurantLogo(id, file.FileName, stream);

        return NoContent();
    }
}