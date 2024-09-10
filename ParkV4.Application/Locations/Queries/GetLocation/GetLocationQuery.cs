using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Locations.Queries.GetLocation;

public class GetLocationQuery : IRequest<BaseResponseModel<GetLocationVm>>
{
    public long Id { get; set; }
}