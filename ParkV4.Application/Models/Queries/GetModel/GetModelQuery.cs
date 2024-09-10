using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Models.Queries.GetModel;

public class GetModelQuery : IRequest<BaseResponseModel<GetModelVm>>
{
    public long Id { get; set; }
}