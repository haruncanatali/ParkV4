using AutoMapper;
using ParkV4.Application.Common.Mappings;

namespace ParkV4.Application.Models.Queries.Dtos;

public class ModelForBrandDto : IMapFrom<Model>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Model, ModelForBrandDto>();
    }
}