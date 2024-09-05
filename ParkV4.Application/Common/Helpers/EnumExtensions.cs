using System.ComponentModel;
using System.Reflection;

namespace ParkV4.Application.Common.Helpers
{
    public static class EnumExtentions
{
    public static string GetDescription<T>(this T source)
    { 
        try
        {
            if(source == null) return string.Empty;
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }

    public static T GetValueFromDescription<T>(string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }
        throw new ArgumentException("Not found.", nameof(description));
    }

    public static double GetVehiclePriceForDuration<T>(this T source, VehicleType vehicleType)
    {
        double price = 0.0; 

        try
        {
            FieldInfo vehicleFieldInfo = vehicleType.GetType().GetField(vehicleType.ToString());
            PriceAttribute priceAttribute = (PriceAttribute)Attribute.GetCustomAttribute(vehicleFieldInfo, typeof(PriceAttribute));


            if(priceAttribute != null)
            {
                Duration duration = (Duration)Enum.Parse(typeof(Duration), source.ToString());
                switch(duration)
                {
                    case Duration.OneHour:
                        price = priceAttribute.OneHour;
                        break;
                    case Duration.TwoHour:
                        price = priceAttribute.TwoHour;
                        break;
                    case Duration.SixHour:
                        price = priceAttribute.SixHour;
                        break;
                    case Duration.OneDay:
                        price = priceAttribute.OneDay;
                        break;
                    case Duration.OneWeek:
                        price = priceAttribute.OneWeek;
                        break;
                    case Duration.OneMonth:
                        price = priceAttribute.OneMonth;
                        break;
                }
            }
        }
        catch
        {
            return price;
        }

        return price;
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    
}
}