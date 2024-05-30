using TicketShopClassLibrary;
using TicketShopClassLibrary.PaymentMethods;


namespace TicketShopConsole;

internal class Program
{
    static void Main(string[] args)
    {
        //
        IEventController eventController = new EventController();
        LocationSection seatSection = new LocationSection("A", new int[10, 20], 100);
        LocationSection benchSection = new LocationSection("B", new int[5, 10], 200);
        LocationSection vipSection = new LocationSection("VIP", new int[10, 10], 300);

        Location woodenHouse = new Location("Trähuset", "Trädvägen 1", new List<LocationSection> { seatSection, benchSection });
        Location brickHouse = new Location("Tegelhuset", "Tegelvägen 42", new List<LocationSection> { seatSection, benchSection, vipSection});
        // (EventDate, Name, location, releasedate)
        // I Trähuset
        eventController.CreateEvent(new DateTime(2023, 12, 13, 1, 0, 0), "Luciashow", woodenHouse, new DateTime(2023, 11, 1, 13, 0, 0));
        eventController.CreateEvent(new DateTime(2023, 10, 10, 20, 0, 0), "Julshow", woodenHouse, new DateTime(2023, 11, 1, 15, 0, 0));

        // I tegelhuset
        eventController.CreateEvent(new DateTime(2024, 01, 07, 2, 0, 0), "Pettson och Findus", brickHouse, new DateTime(2023, 10, 11, 7, 0, 0));
        eventController.CreateEvent(new DateTime(2023, 11, 30, 10, 0, 0), "Andens värld", brickHouse, new DateTime(2023, 10, 30, 8, 45, 0));



        

    }
}

