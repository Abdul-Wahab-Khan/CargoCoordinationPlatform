using CargoCoordinationPlatform.Application.Common.Interfaces;

namespace CargoCoordinationPlatform.Application.Load.Queries.GetLoads;

public record GetLoadsQuery : IRequest<IList<LoadsDto>>;

public class GetLoadsHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetLoadsQuery, IList<LoadsDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IList<LoadsDto>> Handle(GetLoadsQuery query, CancellationToken cancellationToken)
    {
        return await _context.Loads
            .ProjectTo<LoadsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
