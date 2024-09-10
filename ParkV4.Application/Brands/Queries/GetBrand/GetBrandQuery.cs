using MediatR;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Brands.Queries.GetBrand;

public class GetBrandQuery : IRequest<BaseResponseModel<GetBrandVm>>
{
    public long Id { get; set; }
}