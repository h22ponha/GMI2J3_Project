using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary.Factories;
internal interface ITicketFactory
{
    ITicket CreateTicket(int row, int seat);
}
