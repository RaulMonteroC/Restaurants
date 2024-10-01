using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurants.API.Queries;
using Restaurants.Application.DTOs;
using Restaurants.Domain;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantService(IRestaurantRepository restaurantRepository,
                                 IBlobStorageService blobStorageService,
                                 ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDto>> GetAll(GetAllRestaurantsQuery query)
    {
        logger.LogInformation("Getting all restaurants");

        var restaurants = await restaurantRepository.GetAllMatchingAsync(query.SearchPhrase);

        return restaurants.Select(RestaurantDto.FromEntity)!;
    }
    
    public async Task<RestaurantDto?> Get(int id)
    {
        logger.LogInformation($"Getting restaurant with id {id}");

        var restaurant = await restaurantRepository.Get(id)
                      ?? throw new NotFoundException(nameof(Restaurant), id);
        
        var restaurantDto = RestaurantDto.FromEntity(restaurant);

        restaurantDto!.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

        return restaurantDto;
    }

    public async Task<int> Create(CreateRestaurantDto restaurant)
    {
        logger.LogInformation($"Creating restaurant {restaurant.Name}");
        
        return await restaurantRepository.Create(restaurant.ToEntity());
    }
    
    public async Task Delete(int id)
    {
        logger.LogInformation($"Deleting restaurant with id {id}");
        
        var restaurant = await Get(id) 
                             ?? throw new NotFoundException(nameof(Restaurant), id);

        await restaurantRepository.Delete(restaurant.ToEntity());
    }
    
    public async Task Update(CreateRestaurantDto restaurant)
    {
        logger.LogInformation($"Updating restaurant {restaurant.Name}");

        await restaurantRepository.Update(restaurant.ToEntity());
    }

    public async Task UploadRestaurantLogo(int id, string fileName, Stream file)
    {
        logger.LogInformation("Uploading restaurant logo for id: {RestaurantId}", id);
        var restaurant = await restaurantRepository.Get(id);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), id);
        
        restaurant.LogoUrl = await blobStorageService.UploadBlobAsync(file, fileName);

        await restaurantRepository.SaveChangesAsync();
    }
}