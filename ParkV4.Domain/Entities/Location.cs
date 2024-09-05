public class Location : BaseEntity
{
    public string Name { get; set; }
    public long CompanyId { get; set; }

    public Company Company { get; set; }
    public List<Entry> Entries { get; set; }
}