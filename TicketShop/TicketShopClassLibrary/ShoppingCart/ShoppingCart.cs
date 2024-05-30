using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace TicketShopClassLibrary;

public class ShoppingCart : IShoppingCart
{
    public List<ITicket> ListOfReservedTickets { get; set; } = new();
    public IEvent EventToBook { get; set; }
    public int TicketLimit { get; set; } = 5;

    private bool _bookingComplete = false;
    private DateTime _timeLimit;

    public ShoppingCart(IEvent eventToBook)
    {
        EventToBook = eventToBook;
    }

    // Gör så att biljetter som varit reserverade släpps om bokningen inte är klar
    ~ShoppingCart()
    {
        Dispose();
    }
    
    // Lägger till en tidsbegränsning
    public DateTime StartTimeLimit()
    {
        _timeLimit = DateTime.Now.AddSeconds(10);
        
        return _timeLimit;
    }

    // Lägger till en biljett till kundvagnen om det det inte är fler än begränsningen
    public void AddToCart(int row, int seat, string section)
    {
        if(ListOfReservedTickets.Count >= TicketLimit)
        {
            return;
        }
        ListOfReservedTickets.Add(EventToBook.ReserveTicket(row, seat, section));
        
    }

    // Genomför betalningen om tiden inte är ute
    public bool HandlePayment(IPayment payment, Customer customer)
    {
        if(DateTime.Now > _timeLimit)
        {
            ReleaseTickets();
            return false;
        }

        _bookingComplete = payment.Pay(GetCost(), customer);
        return _bookingComplete;
    }

    // Skapar en kostnad för testning
    public double GetCost()
    {
        double cost = 0;
        foreach(ITicket ticket in ListOfReservedTickets)
        {
            cost+= ticket.Section.Price;
        }
        return cost;
    }

    // Släpper alla biljetter i kundkorgen som är reserverade om bokningen inte är klar
    public void ReleaseTickets()
    {
        if (ListOfReservedTickets.Count == 0)
        {
            return;
        }
        foreach (ITicket ticket in ListOfReservedTickets)
        {
            ticket.IsReserved = false;
        }
    }

    // Släpper biljetter om bokning inte är färdig
    public void Dispose()
    {
        if(_bookingComplete == false)
            ReleaseTickets();
    }

    public bool HandlePayment(PaymentMethods.InvoicePayment invoicePayment, Customer customer)
    {
        throw new NotImplementedException();
    }
}
