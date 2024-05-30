using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShopClassLibrary.Factories;

namespace TicketShopClassLibrary;

public class TicketFactory : ITicketFactory
{
    /// <summary>
    /// Returnerar en dictionary med alla tickets till eventet. Matar in layout från location som styr hur dictionaryn ser ut.
    /// </summary>
    /// <param name="layout"></param>
    /// <returns></returns>
    public ITicket CreateTicket(int row, int seat)
    {
        return new Ticket(row, seat);
    }

    public Dictionary<string, List<ITicket>> CreateTickets(ILocation location)
    {
        Dictionary<string, List<ITicket>> map = new Dictionary<string, List<ITicket>>();

        foreach (var section in location.Sections)
        {
            map[section.SectionName] = new List<ITicket>();
            for (int intRow = 0; intRow < section.Layout.GetLength(0); intRow++)
            {
                for (int seatRow = 0; seatRow < section.Layout.GetLength(1); seatRow++)
                {
                    ITicket ticket = CreateTicket(intRow + 1, seatRow + 1);
                    ticket.Section = section;
                    map[section.SectionName].Add(ticket);
                }
            }
        }
        return map;

    }
}
