using CargoCoordinationPlatform.Application.Common.Interfaces;

namespace CargoCoordinationPlatform.Application.Bid.Queries.GetBids;

public record GetBidsQuery : IRequest<IList<BidsDto>>;

public class GetBidsHandler : IRequestHandler<GetBidsQuery, IList<BidsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBidsHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<BidsDto>> Handle(GetBidsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Bids
            .ProjectTo<BidsDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
