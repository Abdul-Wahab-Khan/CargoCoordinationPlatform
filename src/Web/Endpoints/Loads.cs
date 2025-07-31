using CargoCoordinationPlatform.Domain.Entities;
using CleanArchitecture.Application.Load.Commands.CreateLoad;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CargoCoordinationPlatform.Web.Endpoints;

public class Loads : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(CreateLoad).RequireAuthorization();
    }

    public async Task<Created<int>> CreateLoad(ISender sender, CreateLoadCommand command)
    {
        int id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Domain.Entities.Loads)}/{id}", id);
    }
}