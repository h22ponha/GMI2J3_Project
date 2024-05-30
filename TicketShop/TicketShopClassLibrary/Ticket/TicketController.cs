using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
internal class TicketController : ITicketController
{
    public Dictionary<string, List<ITicket>> DictOfTickets { get; set; } = new();

    // Generear en dictionary med alla tickets för eventet med TicketFactory
    public TicketController(ILocation location)
    {
        TicketFactory ticketFactory = new();
        DictOfTickets = ticketFactory.CreateTickets(location);
    }

    /// <summary>
    /// Returnerar en dictionary med alla tickets som inte blivit bokade än
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, List<ITicket>> GetAvailableTickets()
    {
        return DictOfTickets;
    }

    // Reserverar en biljett och returnerar den
    public ITicket ReserveTicket(int row, int seat, string section)
    {
        DictOfTickets[section].Find(x => x.SeatRow == row && x.SeatNumber == seat).IsReserved = true;
        return DictOfTickets[section].Find(x => x.SeatRow == row && x.SeatNumber == seat);
    }
}
