using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Helpers;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Entries.Commands.Create;

public class CreateEntryCommand : IRequest<BaseResponseModel<Guid>>
{
    public long VehicleId { get; set; }
    public long CustomerId { get; set; }
    public long LocationId { get; set; }
    public Duration FirstDuration { get; set; }
    public string? Description { get; set; }
    
    public class Handler : IRequestHandler<CreateEntryCommand, BaseResponseModel<Guid>>
    {
        private readonly IApplicationContext _context;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IApplicationContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponseModel<Guid>> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var checkDepResult = await CheckDependencies(
                vehicleId: request.VehicleId, customerId: request.CustomerId,
                locationId:request.LocationId, userId:_currentUserService.UserId,
                context:_context, token:cancellationToken);

            if (checkDepResult.isError)
            {
                throw new Exception(checkDepResult.errorMessage);
            }

            Guid receiptId = Guid.NewGuid();
            
            await _context.Entries.AddAsync(new Entry
            {
                ReceiptId = receiptId,
                VehicleId = request.VehicleId,
                CustomerId = request.CustomerId,
                LocationId = request.LocationId,
                UserId = _currentUserService.UserId,
                FirstPrice = request.FirstDuration.GetVehiclePriceForDuration(checkDepResult.vehicle.VehicleType),
                FirstDuration = request.FirstDuration,
                Description = request.Description ?? "-",
                FirstDate = DateTime.Now,
                EntryStatus = EntryStatus.Active
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Guid>.Success(receiptId, "Faaliyet başarıyla oluşturuldu.");
        }
    }

    public static async Task<(bool isError, string errorMessage, Entry entry, Vehicle vehicle)> CheckDependencies(
        long vehicleId, long customerId, 
        long locationId, long userId,
        IApplicationContext context, CancellationToken token,
        long entryId = -1)
    {
        Entry? entry = null;
        Vehicle? vehicle = null;
        if (entryId != -1)
        {
            Entry? _entry = await context.Entries
                .FirstOrDefaultAsync(c => c.Id == entryId, token);
            if (_entry == null)
            {
                return (true, "Faaliyet bulunamadı.", entry, vehicle);
            }
            else
            {
                entry = _entry;
            }
        }
        
        Vehicle? _vehicle = await context.Vehicles
            .FirstOrDefaultAsync(c => c.Id == vehicleId, token);
        if (_vehicle == null)
        {
            return (true, "Araç bulunamadı.", entry,vehicle);
        }
        else
        {
            vehicle = _vehicle;
        }
        
        bool isCustomerExists = await context.Customers
            .AnyAsync(c => c.Id == customerId, token);
        if (!isCustomerExists)
        {
            return (true, "Müşteri bulunamadı.", entry, vehicle);
        }
        
        bool isLocationExists = await context.Locations
            .AnyAsync(c => c.Id == locationId, token);
        if (!isLocationExists)
        {
            return (true, "Konum bulunamadı.", entry, vehicle);
        }
        
        bool isUserExists = await context.Users
            .AnyAsync(c => c.Id == userId, token);
        if (!isUserExists)
        {
            return (true, "Kullanıcı bulunamadı.", entry, vehicle);
        }

        return (false, "", entry, vehicle);
    }
}