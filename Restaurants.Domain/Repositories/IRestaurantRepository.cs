namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAll();
    Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);
    Task<Restaurant?> Get(int id);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);
    Task Update(Restaurant restaurant);
    Task SaveChangesAsync();
}