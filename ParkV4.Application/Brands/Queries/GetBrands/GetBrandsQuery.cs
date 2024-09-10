using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Brands.Queries.GetBrands;

public class GetBrandsQuery : IRequest<BaseResponseModel<GetBrandsVm>>
{
    public string? Name { get; set; }
}