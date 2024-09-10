using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Models.Commands.Create;

public class CreateModelCommand : IRequest<BaseResponseModel<Unit>>
{
    public long BrandId { get; set; }
    public string Name { get; set; }
    
    public class Handler : IRequestHandler<CreateModelCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            bool isBrandExists = await _context.Brands
                .AnyAsync(c => c.Id == request.BrandId, cancellationToken);

            if (!isBrandExists)
            {
                throw new Exception("Marka bulunamadı.");
            }

            await _context.Models.AddAsync(new Model
            {
                BrandId = request.BrandId,
                Name = request.Name
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Model başarıyla eklendi.");
        }
    }
}