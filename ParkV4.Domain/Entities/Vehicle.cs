public class Vehicle : BaseEntity
{
    public VehicleType VehicleType { get; set; }
    public string Plate { get; set; }
    public string Color { get; set; }

    public long BrandId { get; set; }
    public long ModelId { get; set; }

    public Brand Brand { get; set; }
    public Model Model { get; set; }

    public List<Entry> Entries { get; set; }
}