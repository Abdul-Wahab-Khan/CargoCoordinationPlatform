using CargoCoordinationPlatform.Application.Bid.Queries.GetBids;
using CargoCoordinationPlatform.Application.Load.Queries.GetLoads;
using CargoCoordinationPlatform.Domain.Entities;
using CleanArchitecture.Application.Bid.Commands.UpdateBid;

namespace CargoCoordinationPlatform.Application.Common.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UpdateBidCommand, Bids>();

        CreateMap<Bids, BidsDto>()
            .ForMember(dest => dest.LoadInfo, opt =>
                opt.MapFrom(src => src.Load != null ? src.Load.ToString() : "No load attached"));

        CreateMap<Loads, LoadsDto>()
            .ForMember(dest => dest.BidsInfo, opt =>
                opt.MapFrom(src => src.Bids != null ? src.Bids.ToString() : "No Bid available"));
    }
}