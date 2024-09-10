using AutoMapper;
using ParkV4.Application.Common.Mappings;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Customers.Queries.Dtos;

public class CustomerDto : BaseDto, IMapFrom<Customer>
{
    public string IdentityNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string TelephoneNumber { get; set; }
    public string Photo { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Customer, CustomerDto>();
    }
}