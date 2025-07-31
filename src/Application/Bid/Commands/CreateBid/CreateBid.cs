using CargoCoordinationPlatform.Application.Common.Interfaces;
using CargoCoordinationPlatform.Domain.Entities;

namespace CleanArchitecture.Application.Bid.Commands.CreateBid;

public record CreateBidCommand : IRequest<object>
{
    public int LoadId { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
}

public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, object>
{
    private readonly IApplicationDbContext _context;

    public CreateBidCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(CreateBidCommand request, CancellationToken cancellationToken)
    {
        var entity = new Bids
        {
            LoadId = request.LoadId,
            Amount = request.Amount,
            Notes = request.Notes
        };

        _context.Bids.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
