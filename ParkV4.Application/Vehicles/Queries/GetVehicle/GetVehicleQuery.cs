using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Vehicles.Queries.GetVehicle;

public class GetVehicleQuery : IRequest<BaseResponseModel<GetVehicleVm>>
{
    public long Id { get; set; }
}