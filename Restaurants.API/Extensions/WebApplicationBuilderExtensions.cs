using Microsoft.OpenApi.Models;
using Serilog;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
                                       {
                                           c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                                                                                 {
                                                                                     Type = SecuritySchemeType.Http,
                                                                                     Scheme = "Bearer"
                                                                                 });
                                   
                                           c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                    {
                                                                        {
                                                                            new OpenApiSecurityScheme
                                                                            {
                                                                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerAuth" }
                                                                            },
                                                                            []
                                                                        }
                                                                    });
                                       });
        builder.Services.AddControllers();
        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    }
}