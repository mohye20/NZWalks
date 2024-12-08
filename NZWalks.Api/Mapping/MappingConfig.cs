using AutoMapper;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Mapping;

public class MappingConfig  : Profile
{
    public MappingConfig()
    {
        CreateMap<Region, RegionDTO>().ReverseMap();
        CreateMap<Region, CreateRegionRequestDto>().ReverseMap();
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        CreateMap<CreateWalksRequestDto, Walk>().ReverseMap();
        CreateMap<Walk,WalkDto>().ReverseMap();
    }
}