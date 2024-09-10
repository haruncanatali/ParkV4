using ParkV4.Application.Users.Queries.Dtos;

namespace ParkV4.Application.Users.Queries.GetUsers;

public class GetUsersVm
{
    public List<UserDto> Users { get; set; }
    public long Count { get; set; }
}