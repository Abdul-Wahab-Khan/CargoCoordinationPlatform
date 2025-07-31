
using CargoCoordinationPlatform.Application.Trip.Queries.GetTrips;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CargoCoordinationPlatform.Web.Endpoints;

public class Trips : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetTrips).RequireAuthorization();
    }

    public async Task<Ok<TripsDto>> GetTrips(ISender sender, [AsParameters] GetTripQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }
}