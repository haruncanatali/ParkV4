using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Vehicles.Commands.Create;
using ParkV4.Domain.Enums;

namespace ParkV4.Application.Vehicles.Commands.Update;

public class UpdateVehicleCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public IFormFile? Photo { get; set; }
    public VehicleType VehicleType { get; set; }
    public FuelType FuelType { get; set; }
    public string Plate { get; set; }
    public string Color { get; set; }
    public long BrandId { get; set; }
    public long ModelId { get; set; }
    
    public class Handler : IRequestHandler<UpdateVehicleCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly FileManager _fileManager;

        public Handler(IApplicationContext context, FileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle? vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (vehicle == null)
            {
                throw new Exception("Silinecek araç sistemde bulunamadı.");
            }
            
            var dependenciesCheck = 
                await CreateVehicleCommand.CheckDependencies(request.BrandId, request.ModelId, _context, cancellationToken);

            if (dependenciesCheck.IsError)
            {
                throw new Exception(dependenciesCheck.ErrorMessage);
            }

            if (request.Photo != null)
            {
                vehicle.Photo = _fileManager.Upload(request.Photo, ImagePath.VehiclePhoto);
            }

            vehicle.VehicleType = request.VehicleType;
            vehicle.FuelType = request.FuelType;
            vehicle.Plate = request.Plate;
            vehicle.Color = request.Color;
            vehicle.BrandId = request.BrandId;
            vehicle.ModelId = request.ModelId;

            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Araç başarıyla güncellendi.");
        }
    }
}