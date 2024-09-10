using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Locations.Commands.Delete;

public class DeleteLocationCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteLocationCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            Location? location = await _context.Locations
                .Include(c=>c.Entries)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (location == null)
            {
                throw new Exception("Konum sistemde bulunamadı.");
            }

            if (location.Entries.Count > 0)
            {
                throw new Exception("Bu konuma ait giriş/çıkış verileri bulunmaktadır. İlgili veriler silinmeden konum silinemez.");
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Konum başarıyla silindi.");
        }
    }
}