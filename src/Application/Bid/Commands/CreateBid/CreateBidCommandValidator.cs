using CargoCoordinationPlatform.Application.Common.Interfaces;
using CleanArchitecture.Application.Bid.Commands.CreateBid;

namespace CargoCoordinationPlatform.Application.Bid.Commands.CreateBid;

public class CreateBidCommandValidator : AbstractValidator<CreateBidCommand>
{
    public CreateBidCommandValidator(IBidsLoadValidatorService bidsLoadValidatorService)
    {
        RuleFor(x => x.LoadId)
            .CustomAsync(bidsLoadValidatorService.ValidateLoad);
    }
}