using CargoCoordinationPlatform.Application.Common.Exceptions;
using CargoCoordinationPlatform.Application.Common.Interfaces;
using CargoCoordinationPlatform.Application.Load.Queries.GetLoads;

namespace CargoCoordinationPlatform.Application.Load.Queries.GetLoad;

public record GetLoadQuery : IRequest<LoadsDto>
{
    public int Id { get; set; }
};

public class GetLoadHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetLoadQuery, LoadsDto>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<LoadsDto> Handle(GetLoadQuery query, CancellationToken cancellationToken)
    {
        var load = await _context.Loads.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
        return load is null ? throw new LoadNotFoundException() : _mapper.Map<LoadsDto>(load);
    }
}