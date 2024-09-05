using System.ComponentModel;

public enum VehicleType
{
    [Description("Otobüs"), Price(20.0, 36.0, 60.0, 100.0, 600.0, 2000.0, 20000.0)]
    Bus,
    [Description("Minibüs"), Price(15.0, 28.0, 50.0, 80.0, 500.0, 1500.0, 15000.0)]
    Minibus,
    [Description("Otomobil"), Price(7.0, 14.0, 25.0, 40.0, 250.0, 750.0, 7500.0)]
    Automobile,
    [Description("Kamyonet"), Price(25.0, 45.0, 75.0, 120.0, 700.0, 2500.0, 25000.0)]
    Pickup,
    [Description("Kamyon"), Price(30.0, 55.0, 90.0, 150.0, 900.0, 3000.0, 30000.0)]
    Truck,
    [Description("Motosiklet"), Price(5.0, 9.0, 15.0, 25.0, 150.0, 500.0, 5000.0)]
    Motorcycle,
    [Description("Bisiklet"), Price(2.0, 3.6, 6.0, 10.0, 60.0, 200.0, 2000.0)]
    Bicycle
}