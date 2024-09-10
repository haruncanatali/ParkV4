using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Vehicles.Queries.Dtos;

namespace ParkV4.Application.Vehicles.Queries.GetVehicle;

public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, BaseResponseModel<GetVehicleVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetVehicleQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetVehicleVm>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
    {
        VehicleDto? vehicle = await _context.Vehicles
            .Where(c => c.Id == request.Id)
            .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<GetVehicleVm>.Success(new GetVehicleVm
        {
            Vehicle = vehicle
        }, "Araç başarıyla getirildi.");
    }
}