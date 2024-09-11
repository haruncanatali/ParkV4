namespace ParkV4.Application.Common.Helpers;

public static class DateExtensions
{
    public static string ToCustomString(this DateTime dateTime)
    {
        return dateTime.ToString("dd.MM.yyyy HH:mm");
    }
    
    public static string GetTimeDifferenceString(this DateTime startDate, DateTime endDate)
    {
        TimeSpan timeDifference = endDate - startDate;

        int years = endDate.Year - startDate.Year;
        int months = endDate.Month - startDate.Month;
        int days = endDate.Day - startDate.Day;

        if (months < 0)
        {
            years--;
            months += 12;
        }

        if (days < 0)
        {
            months--;
            DateTime previousMonth = endDate.AddMonths(-1);
            days += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);
        }

        int weeks = days / 7;
        days %= 7;

        int hours = timeDifference.Hours;
        int minutes = timeDifference.Minutes;
        int seconds = timeDifference.Seconds;

        return $"{years} yıl {months} ay {weeks} hafta {days} gün {hours} saat {minutes} dakika {seconds} saniye";
    }
}