namespace TicketShopClassLibrary;

public interface IEvent
{
    DateTime EventDate { get; set; }
    DateTime EventReleaseDate { get; set; }
    string EventId { get; set; }
    ILocation EventLocation { get; set; }
    string EventName { get; set; }
    public Dictionary<string, List<ITicket>> GetAvailableTickets();
    public ITicket ReserveTicket(int row, int seat, string section);
}