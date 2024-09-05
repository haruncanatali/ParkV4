using System.ComponentModel;

public enum Duration
{
    [Description("1 Saat")]
    OneHour,

    [Description("2 Saat")]
    TwoHour,

    [Description("6 Saat")]
    SixHour,

    [Description("1 Gün")]
    OneDay,

    [Description("1 Hafta")]
    OneWeek,

    [Description("1 Ay")]
    OneMonth
}