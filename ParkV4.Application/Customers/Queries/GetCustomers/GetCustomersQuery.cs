using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<BaseResponseModel<GetCustomersVm>>
{
    public string? IdentityNumber { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}