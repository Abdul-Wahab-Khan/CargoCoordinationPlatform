using CargoCoordinationPlatform.Domain.Entities;

namespace CargoCoordinationPlatform.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Loads> Loads { get; }
    DbSet<Bids> Bids { get; }
    DbSet<Trips> Trips { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
