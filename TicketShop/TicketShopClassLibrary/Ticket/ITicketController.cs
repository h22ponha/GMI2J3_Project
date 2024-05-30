using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public interface ITicketController
{
    Dictionary<string, List<ITicket>> DictOfTickets { get; set; }
    Dictionary<string, List<ITicket>> GetAvailableTickets();
    public ITicket ReserveTicket(int row, int seat, string section);
}
