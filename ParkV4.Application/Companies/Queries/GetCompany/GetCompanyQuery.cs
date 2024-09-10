using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Companies.Queries.GetCompany;

public class GetCompanyQuery : IRequest<BaseResponseModel<GetCompanyVm>>
{
    public long Id { get; set; }
}