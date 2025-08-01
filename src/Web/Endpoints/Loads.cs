using CargoCoordinationPlatform.Application.Load.Queries.GetLoad;
using CargoCoordinationPlatform.Application.Load.Queries.GetLoads;
using CargoCoordinationPlatform.Infrastructure.Interfaces;
using CargoCoordinationPlatform.Infrastructure.Services;
using CleanArchitecture.Application.Load.Commands.AssignLoad;
using CleanArchitecture.Application.Load.Commands.CreateLoad;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CargoCoordinationPlatform.Web.Endpoints;

public class Loads : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetLoads).RequireAuthorization();
        groupBuilder.MapGet(GetLoad, "{id}").RequireAuthorization();
        groupBuilder.MapPost(CreateLoad).RequireAuthorization();
        groupBuilder.MapPatch("{id}/accept", AcceptLoad).RequireAuthorization();
    }

    public async Task<Ok<IList<LoadsDto>>> GetLoads(ISender sender, [AsParameters] GetLoadsQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Ok<LoadsDto>> GetLoad(ISender sender, [AsParameters] GetLoadQuery query, ICacheService cacheService)
    {
        string cacheKey = $"load-{query.Id}";
        var load = await cacheService.GetOrCreateAsync(
            cacheKey,
            async () => await sender.Send(query),
            TimeSpan.FromMinutes(5)
        );

        return TypedResults.Ok(load);
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