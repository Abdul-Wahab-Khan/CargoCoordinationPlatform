namespace CargoCoordinationPlatform.Domain.Entities;

public class Trips : BaseAuditableEntity
{
    public int LoadId { get; set; }
    public Loads? Load { get; set; }
    public string? Driver { get; set; }
    public TripStatus Status { get; set; } = TripStatus.NotStarted;

    public override string ToString()
    {
        return $"Load {LoadId} is given to {Driver}, and it is {Status}";
    }
}