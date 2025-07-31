using CargoCoordinationPlatform.Infrastructure.Identity;

namespace CargoCoordinationPlatform.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
         groupBuilder.MapGroup("test")
            .MapIdentityApi<ApplicationUser>();
    }
}
