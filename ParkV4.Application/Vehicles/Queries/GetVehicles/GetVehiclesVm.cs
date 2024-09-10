using ParkV4.Application.Vehicles.Queries.Dtos;

namespace ParkV4.Application.Vehicles.Queries.GetVehicles;

public class GetVehiclesVm
{
    public List<VehicleDto> Vehicles { get; set; }
    public long Count { get; set; }
}