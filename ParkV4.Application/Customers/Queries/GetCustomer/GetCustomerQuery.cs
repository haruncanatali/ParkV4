using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Customers.Queries.GetCustomer;

public class GetCustomerQuery : IRequest<BaseResponseModel<GetCustomerVm>>
{
    public long Id { get; set; }
}