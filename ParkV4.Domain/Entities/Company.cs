public class Company : BaseEntity
{
    public string Name { get; set; }
    public string Photo { get; set; }

    public List<User> Users { get; set; }
    public List<Location> Locations { get; set; }
}