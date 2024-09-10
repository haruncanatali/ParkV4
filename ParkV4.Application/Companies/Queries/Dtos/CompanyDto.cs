using AutoMapper;
using ParkV4.Application.Common.Mappings;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Companies.Queries.Dtos;

public class CompanyDto : BaseDto, IMapFrom<Company>
{
    public string Name { get; set; }
    public string Photo { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Company, CompanyDto>();
    }
}