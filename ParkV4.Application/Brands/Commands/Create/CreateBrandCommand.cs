using MediatR;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    
    public class Handler : IRequestHandler<CreateBrandCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _applicationContext.Brands.AddAsync(new Brand
            {
                Name = request.Name
            });

            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value,"Marka başarıyla eklendi.");
        }
    }
}