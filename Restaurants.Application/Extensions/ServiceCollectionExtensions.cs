using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Dishes;
using Restaurants.Application.Identity;
using Restaurants.Application.Restaurants;
using Restaurants.Application.User;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;
        
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IDishService, DishService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddValidatorsFromAssembly(assembly)
                .AddFluentValidationAutoValidation();

        services.AddHttpContextAccessor();
    }
}