using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteBrandCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (brand == null)
            {
                throw new Exception("Silinecek marka bulunamadı.");
            }

            List<Model> models = await _context.Models
                .Where(c => c.BrandId == request.Id)
                .ToListAsync(cancellationToken);

            List<Vehicle> vehicles = await _context.Vehicles
                .Where(c => c.BrandId == request.Id)
                .ToListAsync(cancellationToken);

            List<long> vehicleIds = vehicles.Select(c => c.Id).ToList();

            List<Entry> entries = await _context.Entries
                .Where(c => vehicleIds.Contains(c.VehicleId))
                .ToListAsync(cancellationToken);

            if (entries.Count > 0)
            {
                throw new Exception("Bu markaya ait giriş/çıkış verileri bulunmaktadır. İlgili veriler silinmeden marka silinemez.");
            }
            
            _context.Entries.RemoveRange(entries);
            await _context.SaveChangesAsync(cancellationToken);
            
            _context.Vehicles.RemoveRange(vehicles);
            await _context.SaveChangesAsync(cancellationToken);
            
            _context.Models.RemoveRange(models);
            await _context.SaveChangesAsync(cancellationToken);

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync(cancellationToken);
            
            
            return BaseResponseModel<Unit>.Success(Unit.Value,"Marka başarıyla silindi.");
        }
    }
}