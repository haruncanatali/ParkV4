[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class PriceAttribute : Attribute
{
    public double OneHour { get; }
    public double TwoHour { get; }
    public double SixHour { get; }
    public double OneDay { get; }
    public double OneWeek { get; }
    public double OneMonth { get; }
    public double OneYear { get; }

    public PriceAttribute(double oneHour, double twoHour, double sixHour, double oneDay, 
                        double oneWeek, double oneMonth, double oneYear)
    {
        OneHour = oneHour;
        TwoHour = twoHour;
        SixHour = sixHour;
        OneDay = oneDay;
        OneWeek = oneWeek;
        OneMonth = oneMonth;
        OneYear = oneYear;
    }
}