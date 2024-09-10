using AutoMapper;
using ParkV4.Application.Common.Mappings;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Models.Queries.Dtos;

public class ModelDto : BaseDto, IMapFrom<Model>
{
    public long BrandId { get; set; }
    public string BrandName { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Model, ModelDto>()
            .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(c=>c.Brand.Name));
    }
}