using ParkV4.Application.Customers.Queries.Dtos;

namespace ParkV4.Application.Customers.Queries.GetCustomers;

public class GetCustomersVm
{
    public List<CustomerDto> Customers { get; set; }
    public long Count { get; set; }
}