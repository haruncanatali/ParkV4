using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Locations.Commands.Update;

public class UpdateLocationCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public long CompanyId { get; set; }
    
    public class Handler : IRequestHandler<UpdateLocationCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            Location? location = await _context.Locations
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (location == null)
            {
                throw new Exception("Konum sistemde bulunamadı.");
            }
            
            bool isCompanyExists = await _context.Companies
                .AnyAsync(c => c.Id == request.CompanyId, cancellationToken);

            if (!isCompanyExists)
            {
                throw new Exception("Şirket bulunamadı.");
            }

            location.Name = request.Name;
            location.Description = request.Description ?? "Yok";
            location.CompanyId = request.CompanyId;

            _context.Locations.Update(location);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Konum başarıyla güncellendi.");
        }
    }
}