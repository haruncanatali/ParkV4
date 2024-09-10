using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Locations.Commands.Create;

public class CreateLocationCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public long CompanyId { get; set; }
    
    public class Handler : IRequestHandler<CreateLocationCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            bool isCompanyExists = await _context.Companies
                .AnyAsync(c => c.Id == request.CompanyId, cancellationToken);

            if (!isCompanyExists)
            {
                throw new Exception("Şirket bulunamadı.");
            }

            await _context.Locations.AddAsync(new Location
            {
                Name = request.Name,
                Description = request.Description ?? "-",
                CompanyId = request.CompanyId
            }, cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Konum başarıyla oluşturuldu.");
        }
    }
}