using MediatR;
using Microsoft.AspNetCore.Http;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<BaseResponseModel<Unit>>
{
    public string IdentityNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string TelephoneNumber { get; set; }
    public IFormFile? Photo { get; set; }
    
    public class Handler : IRequestHandler<CreateCustomerCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly FileManager _fileManager;

        public Handler(IApplicationContext context, FileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(new Customer
            {
                IdentityNumber = request.IdentityNumber,
                Name = request.Name,
                Surname = request.Surname,
                TelephoneNumber = request.TelephoneNumber,
                Photo = _fileManager.Upload(request.Photo, ImagePath.CustomerPhoto)
            });

            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Müşteri başarıyla eklendi.");
        }
    }
}