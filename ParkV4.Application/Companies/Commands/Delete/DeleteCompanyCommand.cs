using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Companies.Commands.Delete;

public class DeleteCompanyCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteCompanyCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _context.Companies
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (company == null)
            {
                throw new Exception("Silinecek şirket bulunamadı.");
            }

            List<User> users = await _context.Users
                .Where(c => c.CompanyId == request.Id)
                .ToListAsync(cancellationToken);

            List<Location> locations = await _context.Locations
                .Where(c => c.CompanyId == request.Id)
                .ToListAsync(cancellationToken);

            List<long> locationIds = locations.Select(c => c.Id).ToList();

            List<Entry> entries = await _context.Entries
                .Where(c => locationIds.Contains(c.LocationId))
                .ToListAsync(cancellationToken);
            
            _context.Entries.RemoveRange(entries);
            await _context.SaveChangesAsync(cancellationToken);
            
            _context.Locations.RemoveRange(locations);
            await _context.SaveChangesAsync(cancellationToken);
            
            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync(cancellationToken);

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Şirket başarıyla sistemden silinmiştir.");
        }
    }
}