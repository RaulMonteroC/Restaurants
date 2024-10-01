using Microsoft.AspNetCore.Http;
using Restaurants.API.Queries;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAll(GetAllRestaurantsQuery query);
    Task<RestaurantDto?> Get(int id);
    Task<int> Create(CreateRestaurantDto restaurant);
    Task Delete(int id);
    Task Update(CreateRestaurantDto restaurant);
    Task UploadRestaurantLogo(int id, string filename, Stream file);
}