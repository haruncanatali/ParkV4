using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Vehicles.Commands.Delete;

public class DeleteVehicleCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteVehicleCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle? vehicle = await _context.Vehicles
                .Include(c=>c.Entries)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (vehicle == null)
            {
                throw new Exception("Silinecek araç sistemde bulunamadı.");
            }

            if (vehicle.Entries.Count > 0)
            {
                throw new Exception("Bu araca ait giriş/çıkış verileri bulunmaktadır. İlgili veriler silinmeden araç silinemez.");
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Araç başarıyla silindi.");
        }
    }
}