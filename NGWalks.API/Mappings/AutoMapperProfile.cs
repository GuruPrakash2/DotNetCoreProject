using AutoMapper;
using NGWalks.API.Models.Domain;
using NZWalks.API.Models.DT;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<RegionRequestDto, Region>().ReverseMap();
        }
    }
}
