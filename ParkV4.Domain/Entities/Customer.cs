using System.Runtime.Serialization;

public class Customer : BaseEntity
{
    public string IdentityNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string TelephoneNumber { get; set; }
    public string Photo { get; set; }
    
    [IgnoreDataMember]
    public string FullName
    {
        get { return $"{Name} {Surname}"; }
    }

    public List<Entry> Entries { get; set; }
}