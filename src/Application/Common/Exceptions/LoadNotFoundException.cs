namespace CargoCoordinationPlatform.Application.Common.Exceptions;

public class LoadNotFoundException : Exception
{
    public LoadNotFoundException() : base("Load not found")
    {
    }
}