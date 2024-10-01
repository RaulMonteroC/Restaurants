using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes;
using Restaurants.Application.DTOs;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
[Authorize]
public class DishesController(IDishService dishService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromRoute] int restaurantId, [FromBody] DishDto dish)
    {
        dish.RestaurantId = restaurantId;

        await dishService.Create(dish);

        return CreatedAtAction(nameof(Post), new { dish.Id }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int restaurantId, int id) => 
        Ok(await dishService.Get(restaurantId, id));

    [HttpGet]
    public async Task<IActionResult> Get(int restaurantId) => 
        Ok(await dishService.Get(restaurantId));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int restaurantId)
    {
        await dishService.Delete(restaurantId);
        
        return NoContent();
    }
}