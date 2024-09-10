using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public class Handler : IRequestHandler<UpdateBrandCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _context.Brands
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (brand == null)
            {
                throw new Exception("Güncellenecek marka bulunamadı.");
            }

            brand.Name = request.Name;

            _context.Brands.Update(brand);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value,"Marka başarıyla güncellendi.");
        }
    }
}