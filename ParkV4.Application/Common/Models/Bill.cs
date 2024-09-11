namespace ParkV4.Application.Common.Models;

public class Bill
{
    public long Id { get; set; }
    public Guid ReceiptId { get; set; }
    public string CompanyName { get; set; }
    public string CustomerFullName { get; set; }
    public string UserFullName { get; set; }
    public string LocationName { get; set; }
    public string VehicleBrandModel { get; set; }
    public string VehiclePlate { get; set; }
    public string FirstDuration { get; set; }
    public string LastDuration { get; set; }
    public string FirstDate { get; set; }
    public string LastDate { get; set; }
    public string TotalDate { get; set; }
    public double TotalPrice  { get; set; }
}