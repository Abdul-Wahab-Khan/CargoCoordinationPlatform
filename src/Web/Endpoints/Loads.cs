using CargoCoordinationPlatform.Application.Load.Queries.GetLoads;
using CleanArchitecture.Application.Load.Commands.AssignLoad;
using CleanArchitecture.Application.Load.Commands.CreateLoad;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CargoCoordinationPlatform.Web.Endpoints;

public class Loads : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetLoads).RequireAuthorization();
        groupBuilder.MapPost(CreateLoad).RequireAuthorization();
        groupBuilder.MapPatch("{id}/accept", AcceptLoad).RequireAuthorization();
    }

    public async Task<Ok<IList<LoadsDto>>> GetLoads(ISender sender, [AsParameters] GetLoadsQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateLoad(ISender sender, CreateLoadCommand command)
    {
        int id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Domain.Entities.Loads)}/{id}", id);
    }

    public async Task<Results<ContentHttpResult, BadRequest>> AcceptLoad(ISender sender, int id, AssignLoadCommand command)
    {
        if (id != command.LoadId) return TypedResults.BadRequest();

        string message = await sender.Send(command);
        return TypedResults.Content(message);
    }
}