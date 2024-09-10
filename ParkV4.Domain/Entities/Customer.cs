public class Customer : BaseEntity
{
    public string IdentityNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string TelephoneNumber { get; set; }
    public string Photo { get; set; }

    public List<Entry> Entries { get; set; }
}