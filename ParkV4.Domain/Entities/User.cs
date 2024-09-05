public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Photo { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public long CompanyId { get; set; }

    public Company Company { get; set; }
}