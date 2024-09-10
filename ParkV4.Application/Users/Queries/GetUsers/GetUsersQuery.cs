using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<BaseResponseModel<GetUsersVm>>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Username { get; set; }
}