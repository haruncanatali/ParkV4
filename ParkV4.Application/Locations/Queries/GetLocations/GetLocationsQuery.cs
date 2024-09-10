using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Locations.Queries.GetLocations;

public class GetLocationsQuery : IRequest<BaseResponseModel<GetLocationsVm>>
{
    public long? CompanyId { get; set; }
    public string? Name { get; set; }
}