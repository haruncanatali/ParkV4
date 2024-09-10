using MediatR;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Users.Queries.Dtos;

namespace ParkV4.Application.Users.Queries.GetUser;

public class GetUserQuery : IRequest<BaseResponseModel<GetUserVm>>
{
    public long Id { get; set; }
}