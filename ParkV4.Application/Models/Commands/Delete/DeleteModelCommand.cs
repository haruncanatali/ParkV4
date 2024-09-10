using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Models.Commands.Delete;

public class DeleteModelCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteModelCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            Model? model = await _context.Models
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (model == null)
            {
                throw new Exception("Model bulunamadı.");
            }

            List<Vehicle> vehicles = await _context.Vehicles
                .Where(c => c.ModelId == request.Id)
                .ToListAsync(cancellationToken);

            List<long> vehicleIds = vehicles.Select(c => c.Id).ToList();

            List<Entry> entries = await _context.Entries
                .Where(c => vehicleIds.Contains(c.VehicleId))
                .ToListAsync(cancellationToken);
            
            _context.Entries.RemoveRange(entries);
            await _context.SaveChangesAsync(cancellationToken);
            
            _context.Vehicles.RemoveRange(vehicles);
            await _context.SaveChangesAsync(cancellationToken);

            _context.Models.Remove(model);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Model başarıyla silindi.");
        }
    }
}