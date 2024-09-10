using System.ComponentModel;

namespace ParkV4.Domain.Enums;

public enum UserStatus
{
    [Description("Aktif")]
    Active = 1,
    [Description("Pasif")]
    Passive
}