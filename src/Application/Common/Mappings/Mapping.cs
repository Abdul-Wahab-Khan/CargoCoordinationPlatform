using CargoCoordinationPlatform.Domain.Entities;
using CleanArchitecture.Application.Bid.Commands.UpdateBid;

namespace CargoCoordinationPlatform.Application.Common.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UpdateBidCommand, Bids>();
    }
}