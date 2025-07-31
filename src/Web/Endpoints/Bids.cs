
using CargoCoordinationPlatform.Application.Bid.Queries.GetBids;
using CargoCoordinationPlatform.Application.Common.Models;
using CleanArchitecture.Application.Bid.Commands.CreateBid;
using CleanArchitecture.Application.Bid.Commands.UpdateBid;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CargoCoordinationPlatform.Web.Endpoints;

public class Bids : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetBids).RequireAuthorization();
        groupBuilder.MapPost(CreateBid).RequireAuthorization();
        groupBuilder.MapPatch("{id}", UpdateBid).RequireAuthorization();
    }

    public async Task<Ok<IList<BidsDto>>> GetBids(ISender sender, [AsParameters] GetBidsQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateBid(ISender sender, CreateBidCommand command)
    {
        int id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Domain.Entities.Loads)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateBid(ISender sender, int id, UpdateBidCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }
}