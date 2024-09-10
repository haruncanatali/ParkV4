using AutoMapper;
using ParkV4.Application.Common.Mappings;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Models.Queries.Dtos;

namespace ParkV4.Application.Brands.Queries.Dtos;

public class BrandDto : BaseDto, IMapFrom<Brand>
{
    public string Name { get; set; }
    private List<ModelForBrandDto> Models { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Brand, BrandDto>()
            .ForMember(dest => dest.Models, opt =>
                opt.MapFrom(c => c.Models));
    }
}