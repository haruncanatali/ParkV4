using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Locations.Queries.Dtos;

namespace ParkV4.Application.Locations.Queries.GetLocations;

public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, BaseResponseModel<GetLocationsVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetLocationsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetLocationsVm>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        List<LocationDto> locations = await _context.Locations
            .Where(c => (request.CompanyId == null || c.CompanyId == request.CompanyId) &&
                        (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())))
            .Include(c => c.Company)
            .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<GetLocationsVm>.Success(new GetLocationsVm
        {
            Locations = locations,
            Count = locations.Count
        }, "Konumlar başarıyla getirildi.");
    }
}