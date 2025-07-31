using CargoCoordinationPlatform.Domain.Entities;

namespace CargoCoordinationPlatform.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Loads> Loads { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
