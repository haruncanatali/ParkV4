using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Locations.Queries.Dtos;

namespace ParkV4.Application.Locations.Queries.GetLocation;

public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, BaseResponseModel<GetLocationVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetLocationQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetLocationVm>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
    {
        LocationDto? location = await _context.Locations
            .Where(c => c.Id == request.Id)
            .Include(c => c.Company)
            .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<GetLocationVm>.Success(new GetLocationVm
        {
            Location = location
        }, "Konum başarıyla getirildi.");
    }
}