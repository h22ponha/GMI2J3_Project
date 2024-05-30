using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public class Customer : ICustomer
{
    public string CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public string CustomerEmail { get; private set; }
    public string CustomerPhoneNumber { get; private set; }
    public IAccount CustomerAccount { get; set; }
    

    // Konstructor med initiering av CustomerId med Guid för att skapa ett unikt id
    public Customer(string name, string email, string phoneNumber, IAccount account)
    {
        CustomerId = Guid.NewGuid().ToString();
        CustomerName = name;
        CustomerEmail = email;
        CustomerPhoneNumber = phoneNumber;
        CustomerAccount = account;
        
    }
}
