using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;

internal interface ICustomer
{
    public string CustomerId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string CustomerPhoneNumber { get; }
    public IAccount CustomerAccount { get; set; }
}
