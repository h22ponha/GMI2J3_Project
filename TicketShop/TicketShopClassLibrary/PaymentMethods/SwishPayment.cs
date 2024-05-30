using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public class SwishPayment : IPayment
{
    // Returnerar true om betalningen lyckades, annars false
    public bool Pay(double amount, Customer customer)
    {
        return customer.CustomerAccount.Withdraw(amount);
    }

    public bool Pay(double amount, PersonalAccount account)
    {
        throw new NotImplementedException();
    }
}
