using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Vehicles.Queries.GetVehicles;

public class GetVehiclesQuery : IRequest<BaseResponseModel<GetVehiclesVm>>
{
    public string? Plate { get; set; }
}