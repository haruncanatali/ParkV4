using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Models.Commands.Update;

public class UpdateModelCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public long BrandId { get; set; }
    public string Name { get; set; }
    
    public class Handler : IRequestHandler<UpdateModelCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            Model? model = await _context.Models
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (model == null)
            {
                throw new Exception("Güncellenecek model bulunamadı.");
            }

            bool isBrandExists = await _context.Brands
                .AnyAsync(c => c.Id == request.BrandId, cancellationToken);

            if (!isBrandExists)
            {
                throw new Exception("Güncelleme için marka bulunamadı.");
            }

            model.BrandId = request.BrandId;
            model.Name = request.Name;

            _context.Models.Update(model);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Model başarıyla güncellendi.");
        }
    }
}