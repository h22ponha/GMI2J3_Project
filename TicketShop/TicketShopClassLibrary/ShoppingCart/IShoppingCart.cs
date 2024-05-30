using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;

public interface IShoppingCart
{
    public List<ITicket> ListOfReservedTickets { get; set; }
    public IEvent EventToBook { get; set; }
    int TicketLimit { get; set; }

    void AddToCart(int row, int seat, string section);
    void Dispose();
    double GetCost();
    bool HandlePayment(IPayment payment, Customer customer);
    bool HandlePayment(PaymentMethods.InvoicePayment invoicePayment, Customer customer);
    void ReleaseTickets();
    DateTime StartTimeLimit();
}

