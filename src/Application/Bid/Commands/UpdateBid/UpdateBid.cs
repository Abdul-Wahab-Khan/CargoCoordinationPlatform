using CargoCoordinationPlatform.Application.Common.Exceptions;
using CargoCoordinationPlatform.Application.Common.Interfaces;
using CargoCoordinationPlatform.Domain.Entities;

namespace CleanArchitecture.Application.Bid.Commands.UpdateBid;

public record UpdateBidCommand : IRequest<object>
{
    public int Id { get; set; }
    public int? LoadId { get; set; }
    public decimal? Amount { get; set; }
    public string? Notes { get; set; }
}

public class UpdateBidCommandHandler : IRequestHandler<UpdateBidCommand, object>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBidCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<object> Handle(UpdateBidCommand request, CancellationToken cancellationToken)
    {
        var bid = await _context.Bids.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new BidNotFoundException();

        _mapper.Map(request, bid);

        await _context.SaveChangesAsync(cancellationToken);

        return bid;
    }
}
