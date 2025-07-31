using CleanArchitecture.Application.Bid.Commands.CreateBid;
using CleanArchitecture.Application.Bid.Commands.UpdateBid;

namespace CargoCoordinationPlatform.Application.Common.Interfaces;

public interface IBidsLoadValidatorService
{
    Task ValidateLoad(int loadId, ValidationContext<CreateBidCommand> context, CancellationToken cancellationToken);
    Task ValidateLoad(int? loadId, ValidationContext<UpdateBidCommand> context, CancellationToken cancellationToken);
}