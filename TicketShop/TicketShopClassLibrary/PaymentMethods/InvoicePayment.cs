using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary.PaymentMethods
{
    public class InvoicePayment : IPayment
    {
        // Always returns true to simulate that the payment succeeded since it is an invoice
        public bool Pay(double amount, PersonalAccount account)
        {
            if (account.Withdraw(amount))
            {
                Console.WriteLine($"Payment of {amount} from account {account.AccountName} succeeded.");
                return true;
            }
            else
            {
                Console.WriteLine($"Payment of {amount} from account {account.AccountName} failed. Insufficient funds.");
                return false;
            }
        }

        public bool Pay(double v, Customer customer)
        {
            throw new NotImplementedException();
        }
    }

   
}
