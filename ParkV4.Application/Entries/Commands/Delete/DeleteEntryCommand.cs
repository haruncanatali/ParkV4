using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Entries.Commands.Delete;

public class DeleteEntryCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteEntryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;
        public async Task<BaseResponseModel<Unit>> Handle(DeleteEntryCommand request, CancellationToken cancellationToken)
        {
            Entry? entry = await _applicationContext.Entries
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entry == null)
            {
                throw new Exception("Faaliyet sistemde bulunamadı.");
            }

            _applicationContext.Entries.Remove(entry);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Faaliyet başarıyla silindi.");
        }
    }
}