using MediatR;
using Microsoft.AspNetCore.Http;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Companies.Commands.Create;

public class CreateCompanyCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    public IFormFile? Photo { get; set; }
    
    public class Handler : IRequestHandler<CreateCompanyCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly FileManager _fileManager;

        public Handler(IApplicationContext context, FileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            await _context.Companies.AddAsync(new Company
            {
                Name = request.Name,
                Photo = _fileManager.Upload(request.Photo, ImagePath.CompanyPhoto)
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Şirket başarıyla oluşturuldu.");
        }
    }
}