namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType, int id)
    : Exception($"{resourceType} with id {id} doesn't exist");