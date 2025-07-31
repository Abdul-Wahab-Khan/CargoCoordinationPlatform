
using CargoCoordinationPlatform.Application.Common.Exceptions;
using CargoCoordinationPlatform.Application.Common.Interfaces;
using CargoCoordinationPlatform.Domain.Entities;
using CargoCoordinationPlatform.Domain.Enums;

namespace CleanArchitecture.Application.Load.Commands.AssignLoad;

public record AssignLoadCommand : IRequest<string>
{
    public int LoadId { get; set; }
    public required string Driver { get; set; }
}

public class AssignLoadCommandHandler(IApplicationDbContext context) : IRequestHandler<AssignLoadCommand, string>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<string> Handle(AssignLoadCommand request, CancellationToken cancellationToken)
    {
        var load = await _context.Loads.FindAsync(request.LoadId) ?? throw new LoadNotFoundException();
        load.Status = LoadStatus.Assigned;

        var trip = new Trips
        {
            Driver = request.Driver,
            LoadId = request.LoadId
        };

        _context.Trips.Add(trip);

        await _context.SaveChangesAsync(cancellationToken);

        return trip.ToString();
    }
}
