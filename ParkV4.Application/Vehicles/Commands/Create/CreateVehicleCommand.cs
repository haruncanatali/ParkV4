using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;
using ParkV4.Domain.Enums;

namespace ParkV4.Application.Vehicles.Commands.Create;

public class CreateVehicleCommand : IRequest<BaseResponseModel<Unit>>
{
    public IFormFile? Photo { get; set; }
    public VehicleType VehicleType { get; set; }
    public FuelType FuelType { get; set; }
    public string Plate { get; set; }
    public string Color { get; set; }
    public long BrandId { get; set; }
    public long ModelId { get; set; }
    
    public class Handler : IRequestHandler<CreateVehicleCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly FileManager _fileManager;

        public Handler(IApplicationContext context, FileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var dependenciesCheck = await CheckDependencies(request.BrandId, request.ModelId, _context, cancellationToken);

            if (dependenciesCheck.IsError)
            {
                throw new Exception(dependenciesCheck.ErrorMessage);
            }

            await _context.Vehicles.AddAsync(new Vehicle
            {
                VehicleType = request.VehicleType,
                Plate = request.Plate,
                Color = request.Color,
                BrandId = request.BrandId,
                ModelId = request.ModelId,
                FuelType = request.FuelType,
                Photo =  _fileManager.Upload(request.Photo, ImagePath.VehiclePhoto)
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Araç başarıyla oluşturuldu.");
        }
    }

    public static async Task<(bool IsError, string ErrorMessage)> 
        CheckDependencies(
            long brandId, long modelId, 
            IApplicationContext context, CancellationToken token)
    {
        bool brandExists = await context.Brands.AnyAsync(brand => brand.Id == brandId, token);
        if (!brandExists)
        {
            return (true, "Marka bulunamadı.");
        }

        bool modelExists = await context.Models.AnyAsync(model => model.Id == modelId, token);
        if (!modelExists)
        {
            return (true, "Model bulunamadı.");
        }

        return (false, string.Empty);
    }
}