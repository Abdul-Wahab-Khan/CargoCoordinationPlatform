
using CargoCoordinationPlatform.Application.Trip.Queries.GetTrips;
using CargoCoordinationPlatform.Infrastructure.Interfaces;
using CargoCoordinationPlatform.Infrastructure.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CargoCoordinationPlatform.Web.Endpoints;

public class Trips : EndpointGroupBase
{
    private readonly ICacheService _cacheService;
    public Trips()
    {
        _cacheService = new RedisCacheService();
    }

    public Trips(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetTrips).RequireAuthorization();
    }

    public async Task<Ok<TripsDto>> GetTrips(ISender sender, [AsParameters] GetTripQuery query)
    {
        string cacheKey = $"trip-{query.Id}";
        var trip = await _cacheService.GetOrCreateAsync(
            cacheKey,
            async () => await sender.Send(query),
            TimeSpan.FromMinutes(5)
        );

        return TypedResults.Ok(trip);
    }
}