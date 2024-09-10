using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Models.Queries.GetModels;

public class GetModelsQuery : IRequest<BaseResponseModel<GetModelsVm>>
{
    public string? Name { get; set; }
}