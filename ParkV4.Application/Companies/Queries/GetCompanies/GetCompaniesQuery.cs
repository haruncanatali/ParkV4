using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Companies.Queries.GetCompanies;

public class GetCompaniesQuery : IRequest<BaseResponseModel<GetCompaniesVm>>
{
    public string? Name { get; set; }
}