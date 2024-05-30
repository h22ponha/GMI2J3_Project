using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public class Event : IEvent
{
    public DateTime EventDate { get; set; }
    public string EventId { get; set; }
    public string EventName { get; set; }
    public ILocation EventLocation { get; set; }
    public DateTime EventReleaseDate { get; set; }

    // Hanterar biljetter för eventet
    ITicketController _ticketController;

    // Konstructor som skapar ett unikt ID för eventet och skapar en ticketcontroller för eventet
    public Event(ILocation eventLocation, string eventName, DateTime eventReleaseDate, DateTime eventDate)
    {
        EventReleaseDate = eventReleaseDate;
        EventName = eventName;
        EventDate = eventDate;
        EventLocation = eventLocation;
        EventId = Guid.NewGuid().ToString();
        _ticketController = new TicketController(EventLocation);
    }

    // Returnerar en dictionary med alla tillgängliga biljetter för eventet
    public Dictionary<string, List<ITicket>> GetAvailableTickets()
    {
        return _ticketController.GetAvailableTickets();
    }

    // Bokar en biljett för eventet och returnerar den
    public ITicket ReserveTicket(int row, int seat, string section)
    {
        return _ticketController.ReserveTicket(row, seat, section);
    }

    // Returnerar en string med formaterad information om eventet
    public override string ToString()
    {
        return $"Event: {EventName}\n" +
            $"Ticket Release: {EventReleaseDate.ToString("yyyy-MM-dd HH:mm:ss")}\n" +
            $"Event Location: {EventLocation.LocationName}\n" +
            $"Event Date: {EventDate.ToString("yyyy-MM-dd")}\n" +
            $"Event Time: {EventDate.ToString("HH:mm")}\n" +
            $"EventID: {EventId}\n";
    }
}


