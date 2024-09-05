public class Entry : BaseEntity
{
    public Guid ReceiptId { get; set; }

    public long VehicleId { get; set; }
    public long CustomerId { get; set; }
    public long LocationId { get; set; }

    public double FirstPrice { get; set; }
    public double LastPrice { get; set; }
    public double PriceDiffrence { get; set; }

    public Duration FirstDuration { get; set; }
    public Duration LastDuration { get; set; }

    public DateTime FirstDate { get; set; }
    public DateTime LastDate { get; set; }

    public EntryStatus EntryStatus { get; set; }

    public Vehicle Vehicle { get; set; }
    public Customer Customer { get; set; }
    public Location Location { get; set; }

    public Entry()
    {
        ReceiptId = Guid.NewGuid();
    }
}