using System.ComponentModel;

namespace ParkV4.Domain.Enums;

public enum FuelType
{
    [Description("Benzin")]
    Gasoline,
    [Description("Dizel")]
    Diesel,
    [Description("Elektrikli")]
    Electric,
    [Description("Hibrit")]
    Hybrid,
    [Description("LPG + Benzin")]
    LPG,
    [Description("Hidrojen")]
    Hydrogen
}