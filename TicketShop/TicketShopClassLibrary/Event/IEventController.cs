namespace TicketShopClassLibrary;

public interface IEventController
{
    List<IEvent> ListOfEvents { get; set; }

    bool CheckEventReleaseDate(DateTime eventReleaseDate);
    void CreateEvent(DateTime eventDate, string eventName, ILocation eventLocation, DateTime eventReleaseDate);

}