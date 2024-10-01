using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Configuration;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Storage;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantDbContext>(x => x.UseSqlServer(connectionString)
                                                         .EnableSensitiveDataLogging());
        services.AddAuthorization();
        services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantUserClaimPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantDbContext>();

        services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HAS_NATIONALITY, builder => builder.RequireClaim(AppClaimTypes.NATIONALITY))
                .AddPolicy(PolicyNames.AT_LEAST_20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
        
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();
        
        services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        
    }
}