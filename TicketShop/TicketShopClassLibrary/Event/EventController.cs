using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public class EventController : IEventController
{
    public List<IEvent> ListOfEvents { get; set; } = new();

    // Skapar ett event  och lägger till det i listan av events
    public void CreateEvent(DateTime eventDate, string eventName, ILocation eventLocation, DateTime eventReleaseDate)
    {
        IEvent newEvent = new Event(eventLocation, eventName, eventReleaseDate, eventDate);

        ListOfEvents.Add(newEvent);
    }

    public bool CheckEventReleaseDate(DateTime eventReleaseDate)
    {
        if (DateTime.Now > eventReleaseDate)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
