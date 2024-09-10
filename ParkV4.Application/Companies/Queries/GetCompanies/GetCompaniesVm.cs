using ParkV4.Application.Companies.Queries.Dtos;

namespace ParkV4.Application.Companies.Queries.GetCompanies;

public class GetCompaniesVm
{
    public List<CompanyDto> Companies { get; set; }
    public long Count { get; set; }
}