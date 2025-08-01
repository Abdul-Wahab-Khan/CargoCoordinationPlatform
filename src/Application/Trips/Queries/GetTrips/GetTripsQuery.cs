using CargoCoordinationPlatform.Application.Common.Interfaces;
using CargoCoordinationPlatform.Domain.Entities;

namespace CargoCoordinationPlatform.Application.Trip.Queries.GetTrips;

public record GetTripQuery : IRequest<TripsDto>
{
    public int Id { get; set; }
}

public class GetTripHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetTripQuery, TripsDto>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<TripsDto> Handle(GetTripQuery query, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips.FirstAsync(x => x.Id == query.Id);
        return _mapper.Map<TripsDto>(trip);
    }
}
