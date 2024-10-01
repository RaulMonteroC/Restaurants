using FluentValidation;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Validators;

public class RestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    private readonly string[] _validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
    public RestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
           .Length(3, 100);

        RuleFor(x => x.Description)
           .NotEmpty()
           .WithMessage("Description is required");

        RuleFor(x => x.Category)
           .Must(_validCategories.Contains)
           .WithMessage("Please provide a valid category");

        RuleFor(x => x.ContactEmail)
           .EmailAddress()
           .WithMessage("Please provide a valid email address");
    }
}