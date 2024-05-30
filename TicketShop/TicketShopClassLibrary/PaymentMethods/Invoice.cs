using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace TicketShopClassLibrary;
public class InvoicePayment : IPayment
{
    // Returnerar alltid true för att simulera att betalningen lyckades eftersom det är en faktura
    public bool Pay(double amount, Customer customer)
    {
        return true;
    }
}
*/
using System;

namespace TicketShopClassLibrary
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime InvoiceDate { get; set; }

        public Invoice(string description, double amount)
        {
            InvoiceId = Guid.NewGuid();
            Description = description;
            Amount = amount;
            InvoiceDate = DateTime.Now;
        }
    }
}
