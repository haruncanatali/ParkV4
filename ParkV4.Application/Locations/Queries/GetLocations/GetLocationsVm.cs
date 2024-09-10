using ParkV4.Application.Locations.Queries.Dtos;

namespace ParkV4.Application.Locations.Queries.GetLocations;

public class GetLocationsVm
{
    public List<LocationDto> Locations { get; set; }
    public long Count { get; set; }
}