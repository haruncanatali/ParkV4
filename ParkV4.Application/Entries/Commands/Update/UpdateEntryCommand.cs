using MediatR;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Entries.Commands.Create;

namespace ParkV4.Application.Entries.Commands.Update;

public class UpdateEntryCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public long VehicleId { get; set; }
    public long CustomerId { get; set; }
    public long LocationId { get; set; }
    public string? Description { get; set; }
    
    public class Handler : IRequestHandler<UpdateEntryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IApplicationContext applicationContext, ICurrentUserService currentUserService)
        {
            _context = applicationContext;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateEntryCommand request, CancellationToken cancellationToken)
        {
            var checkDepResult = await CreateEntryCommand.CheckDependencies(
                vehicleId: request.VehicleId, customerId: request.CustomerId,
                locationId:request.LocationId, userId:_currentUserService.UserId,
                context:_context, token:cancellationToken);

            if (checkDepResult.isError)
            {
                throw new Exception(checkDepResult.errorMessage);
            }

            Entry entry = checkDepResult.entry;

            entry.VehicleId = request.VehicleId;
            entry.CustomerId = request.CustomerId;
            entry.LocationId = request.LocationId;
            entry.Description = request.Description ?? "Yok";

            _context.Entries.Update(entry);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Faaliayet başarıyla güncellendi.");
        }
    }
}