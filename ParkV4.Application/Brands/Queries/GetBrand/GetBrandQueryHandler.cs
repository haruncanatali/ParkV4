using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Brands.Queries.Dtos;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Brands.Queries.GetBrand;

public class GetBrandQueryHandler : IRequestHandler<GetBrandQuery, BaseResponseModel<GetBrandVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetBrandQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetBrandVm>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
    {
        BrandDto? brand = await _context.Brands
            .Where(c => c.Id == request.Id)
            .Include(c => c.Models)
            .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (brand == null)
        {
            throw new Exception("Marka bulunamadı.");
        }
        
        return BaseResponseModel<GetBrandVm>.Success(new GetBrandVm
        {
            Brand = brand
        }, "Marka başarıyla getirildi.");
    }
}