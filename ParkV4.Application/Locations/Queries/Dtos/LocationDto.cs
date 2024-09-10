using AutoMapper;
using ParkV4.Application.Common.Mappings;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Locations.Queries.Dtos;

public class LocationDto : BaseDto, IMapFrom<Location>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long CompanyId { get; set; }
    public string CompanyName { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Location, LocationDto>();
    }
}