using System.Runtime.Serialization;
using ParkV4.Domain.Enums;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Photo { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string TelephoneNumber { get; set; }

    public UserStatus UserStatus { get; set; }

    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
    public DateTime TokenExpireTime { get; set; }


    public long CompanyId { get; set; }

    public Company Company { get; set; }
    public List<Entry> Entries { get; set; }

    [IgnoreDataMember]
    public string FullName
    {
        get { return $"{Name} {Surname}"; }
    }
}