using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Companies.Commands.Update;

public class UpdateCompanyCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IFormFile? Photo { get; set; }
    
    public class Handler : IRequestHandler<UpdateCompanyCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly FileManager _fileManager;

        public Handler(IApplicationContext context, FileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _context.Companies
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (company == null)
            {
                throw new Exception("Güncellenecek şirket bulunamadı.");
            }

            company.Name = request.Name;

            if (request.Photo != null)
            {
                company.Photo = _fileManager.Upload(request.Photo, ImagePath.CompanyPhoto);
            }

            _context.Companies.Update(company);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Şirket başarıyla güncellendi.");
        }
    }
}