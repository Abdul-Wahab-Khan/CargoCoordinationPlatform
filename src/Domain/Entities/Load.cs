namespace CargoCoordinationPlatform.Domain.Entities;

public class Load : BaseAuditableEntity
{
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public CargoType CargoType { get; set; }
    public decimal Weight { get; set; }
    public DateTime PickupTime { get; set; }
    public PricingMode PricingMode { get; set; }
    public LoadStatus Status { get; set; } = LoadStatus.Open;
    public IList<Bid> Bids { get; private set; } = [];
}