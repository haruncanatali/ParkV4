using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Customers.Commands.Delete;

public class DeleteCustomerCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteCustomerCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = await _context.Customers
                .Include(c=>c.Entries)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null)
            {
                throw new Exception("Silinecek  müşteri bulunamadı.");
            }

            if (customer.Entries.Count > 0)
            {
                throw new Exception("Bu müşteriye ait giriş/çıkış verileri bulunmaktadır. İlgili veriler silinmeden müşteri silinemez.");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Müşteri başarıyla sistemden silindi.");
        }
    }
}