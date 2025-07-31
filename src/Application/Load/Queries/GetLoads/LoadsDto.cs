using CargoCoordinationPlatform.Domain.Enums;

namespace CargoCoordinationPlatform.Application.Load.Queries.GetLoads;

public class LoadsDto
{
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public CargoType CargoType { get; set; }
    public decimal Weight { get; set; }
    public DateTime PickupTime { get; set; }
    public PricingMode PricingMode { get; set; }
    public LoadStatus Status { get; set; }

    public string? BidsInfo { get; set; }
}