using CargoCoordinationPlatform.Application.Common.Interfaces;
using CargoCoordinationPlatform.Domain.Enums;
using CleanArchitecture.Application.Bid.Commands.CreateBid;
using CleanArchitecture.Application.Bid.Commands.UpdateBid;

namespace CargoCoordinationPlatform.Application.Common.Services;

public class BidsLoadValidatorService : IBidsLoadValidatorService
{
    private readonly IApplicationDbContext _context;
    public BidsLoadValidatorService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ValidateLoad(int loadId, ValidationContext<CreateBidCommand> context, CancellationToken cancellationToken)
    {
        bool exists = await ValidateLoadExists(loadId, cancellationToken);
        if (!exists)
        {
            context.AddFailure($"Load doesn't exist for provided id {loadId}");
            return;
        }

        bool isOpen = await ValidateLoadIsOpen(loadId, cancellationToken);
        if (!isOpen) context.AddFailure($"Load {loadId} is not in open status");

        bool isTaken = await ValidateLoadNotAttachedToOtherBids(loadId, cancellationToken);
        if (isTaken) context.AddFailure($"Load {loadId} is taken by other bid");
    }

    public async Task ValidateLoad(int? loadId, ValidationContext<UpdateBidCommand> context, CancellationToken cancellationToken)
    {
        if (loadId != null)
        {
            bool exists = await ValidateLoadExists(loadId.Value, cancellationToken);
            if (!exists)
            {
                context.AddFailure($"Load doesn't exist for provided id {loadId}");
                return;
            }

            bool isOpen = await ValidateLoadIsOpen(loadId.Value, cancellationToken);
            if (!isOpen) context.AddFailure($"Load {loadId} is not in open status");

            bool isTaken = await ValidateLoadNotAttachedToOtherBids(loadId.Value, cancellationToken);
            if (isTaken) context.AddFailure($"Load {loadId} is taken by other bid");
        }
    }

    private Task<bool> ValidateLoadExists(int loadId, CancellationToken cancellationToken)
    {
        return _context.Loads.AnyAsync(x => x.Id == loadId, cancellationToken);
    }

    private async Task<bool> ValidateLoadIsOpen(int loadId, CancellationToken cancellationToken)
    {
        var load = await _context.Loads.FirstOrDefaultAsync(x => x.Id == loadId, cancellationToken);
        return load != null && load.Status == LoadStatus.Open;
    }

    private Task<bool> ValidateLoadNotAttachedToOtherBids(int loadId, CancellationToken cancellationToken)
    {
        return _context.Bids.AnyAsync(x => x.LoadId == loadId, cancellationToken);
    }
}