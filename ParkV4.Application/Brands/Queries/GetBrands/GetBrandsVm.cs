using ParkV4.Application.Brands.Queries.Dtos;

namespace ParkV4.Application.Brands.Queries.GetBrands;

public class GetBrandsVm
{
    public List<BrandDto> Brands { get; set; }
    public long Count { get; set; }
}