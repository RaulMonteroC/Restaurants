using FluentValidation;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Validators;

public class DishDtoValidator : AbstractValidator<DishDto>
{
    public DishDtoValidator()
    {
        RuleFor(dish => dish.Price)
           .GreaterThanOrEqualTo(0)
           .WithMessage("Price must be a non-negative number.");

        RuleFor(dish => dish.KiloCalories)
           .GreaterThanOrEqualTo(0)
           .WithMessage("Kilocalories must be non-negative number.");
    }
}