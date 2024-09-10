using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Vehicles.Queries.Dtos;

namespace ParkV4.Application.Vehicles.Queries.GetVehicles;

public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, BaseResponseModel<GetVehiclesVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetVehiclesQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetVehiclesVm>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        List<VehicleDto> vehicles = await _context.Vehicles
            .Where(c => (request.Plate == null || c.Plate.ToLower().Contains(request.Plate.ToLower())))
            .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<GetVehiclesVm>.Success(new GetVehiclesVm
        {
            Vehicles = vehicles,
            Count = vehicles.Count
        }, "Araçlar başarıyla getirildi.");
    }
}