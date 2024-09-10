using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string IdentityNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string TelephoneNumber { get; set; }
    public IFormFile? Photo { get; set; }
    
    public class Handler : IRequestHandler<UpdateCustomerCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly FileManager _fileManager;

        public Handler(IApplicationContext context, FileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null)
            {
                throw new Exception("Güncellenecek müşteri bulunamadı.");
            }

            if (request.Photo == null)
            {
                customer.Photo = _fileManager.Upload(request.Photo, ImagePath.CustomerPhoto);
            }

            customer.IdentityNumber = request.IdentityNumber;
            customer.Name = request.Name;
            customer.Surname = request.Surname;
            customer.TelephoneNumber = request.TelephoneNumber;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Müşteri başarıyla güncellendi.");
        }
    }
}