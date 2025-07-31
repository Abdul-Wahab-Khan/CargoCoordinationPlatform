namespace CargoCoordinationPlatform.Application.Common.Exceptions;

public class BidNotFoundException : Exception
{
    public BidNotFoundException() : base("Bid was not found") { }
}