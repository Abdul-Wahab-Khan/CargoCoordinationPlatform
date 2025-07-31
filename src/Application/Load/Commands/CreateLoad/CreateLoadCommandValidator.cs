using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Load.Commands.CreateLoad;

namespace CargoCoordinationPlatform.Application.Load.Commands.CreateLoad;

public class CreateLoadCommandValidator : AbstractValidator<CreateLoadCommand>
{
    public CreateLoadCommandValidator()
    {
        RuleFor(x => x.CargoType)
            .IsInEnum().WithMessage("Invalid cargo type.");

        RuleFor(x => x.PricingMode)
            .IsInEnum().WithMessage("Invalid pricing mode.");


        RuleFor(x => x.PickupTime)
            .Must(time => time >= DateTime.Now.AddHours(2))
            .WithMessage("The pickup time must be at least 2 hours from now.");
    }
}