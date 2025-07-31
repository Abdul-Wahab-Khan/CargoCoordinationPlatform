using CargoCoordinationPlatform.Application.Common.Interfaces;
using CleanArchitecture.Application.Bid.Commands.UpdateBid;

namespace CargoCoordinationPlatform.Application.Bid.Commands.UpdateBid;

public class UpdateBidCommandValidator : AbstractValidator<UpdateBidCommand>
{
    public UpdateBidCommandValidator(IBidsLoadValidatorService bidsLoadValidatorService)
    {
        RuleFor(x => x.LoadId)
            .CustomAsync(bidsLoadValidatorService.ValidateLoad);
    }
}