using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Brands.Queries.Dtos;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Brands.Queries.GetBrands;

public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, BaseResponseModel<GetBrandsVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetBrandsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetBrandsVm>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        List<BrandDto> brands = await _context.Brands
            .Where(c => (request.Name == null || c.Name.ToLower() == request.Name.ToLower()))
            .Include(c => c.Models)
            .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<GetBrandsVm>.Success(new GetBrandsVm
        {
            Brands = brands,
            Count = brands.Count
        }, "Markalar başarıyla getirildi.");
    }
}