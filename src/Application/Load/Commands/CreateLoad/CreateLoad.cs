
using CargoCoordinationPlatform.Application.Common.Interfaces;
using CargoCoordinationPlatform.Domain.Entities;
using CargoCoordinationPlatform.Domain.Enums;

namespace CleanArchitecture.Application.Load.Commands.CreateLoad;

public record CreateLoadCommand : IRequest<int>
{
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public CargoType CargoType { get; set; }
    public decimal Weight { get; set; }
    public DateTime PickupTime { get; set; }
    public PricingMode PricingMode { get; set; }
}

public class CreateLoadCommandHandler : IRequestHandler<CreateLoadCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateLoadCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateLoadCommand request, CancellationToken cancellationToken)
    {
        var entity = new Loads
        {
            Destination = request.Destination,
            Origin = request.Origin,
            CargoType = request.CargoType,
            PickupTime = request.PickupTime,
            PricingMode = request.PricingMode,
            Weight = request.Weight,
        };

        _context.Loads.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
