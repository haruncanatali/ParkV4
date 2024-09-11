using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Helpers;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Entries.Commands.CreateBill;

public class CreateBillCommand : IRequest<BaseResponseModel<Bill>>
{
    public Guid ReceiptId { get; set; }
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<CreateBillCommand, BaseResponseModel<Bill>>
    {
        private readonly IApplicationContext _context;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IApplicationContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponseModel<Bill>> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            Entry? entry = await _context.Entries
                .Include(c=>c.Vehicle)
                .ThenInclude(c=>c.Model)
                .ThenInclude(c=>c.Brand)
                .Include(c=>c.Customer)
                .Include(c=>c.Location)
                .Include(c=>c.User)
                .ThenInclude(c=>c.Company)
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.ReceiptId == request.ReceiptId, cancellationToken);

            if (entry == null)
            {
                throw new Exception("Faaliyet sistemde bulunamadı.");
            }

            if (entry.EntryStatus == EntryStatus.Passive)
            {
                throw new Exception("Faaliyetin faturası oluşturulmuş.");
            }

            entry.EntryStatus = EntryStatus.Passive;
            entry.LastDate = DateTime.Now;
            entry.LastDuration = GetDurationFromDates(entry.FirstDate, entry.LastDate);
            entry.LastPrice = entry.LastDuration.GetVehiclePriceForDuration(entry.Vehicle.VehicleType);
            entry.PriceDiffrence = entry.LastPrice - entry.FirstPrice;

            _context.Entries.Update(entry);
            await _context.SaveChangesAsync(cancellationToken);

            Bill bill = new()
            {
                Id = entry.Id,
                ReceiptId = entry.ReceiptId,
                CompanyName = entry.User.Company.Name,
                CustomerFullName = entry.Customer.FullName,
                UserFullName = entry.User.FullName,
                LocationName = entry.Location.Name,
                VehicleBrandModel = $"{entry.Vehicle.Brand.Name} {entry.Vehicle.Model.Name}", 
                VehiclePlate = entry.Vehicle.Plate,
                FirstDuration = entry.FirstDuration.GetDescription(),
                LastDuration = entry.LastDuration.GetDescription(),
                FirstDate = entry.FirstDate.ToCustomString(),
                LastDate = entry.LastDate.ToCustomString(),
                TotalDate = entry.LastDate.GetTimeDifferenceString(entry.FirstDate),
                TotalPrice = entry.LastPrice
            };
            
            return BaseResponseModel<Bill>.Success(bill, "Fatura başarıyla oluşturuldu.");
        }
        
        public Duration GetDurationFromDates(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeDifference = endDate - startDate;

            if (timeDifference.TotalHours <= 1)
            {
                return Duration.OneHour;
            }
            else if (timeDifference.TotalHours <= 2)
            {
                return Duration.TwoHour;
            }
            else if (timeDifference.TotalHours <= 6)
            {
                return Duration.SixHour;
            }
            else if (timeDifference.TotalDays <= 1)
            {
                return Duration.OneDay;
            }
            else if (timeDifference.TotalDays <= 7)
            {
                return Duration.OneWeek;
            }
            else
            {
                return Duration.OneMonth;
            }
        }
    }
}