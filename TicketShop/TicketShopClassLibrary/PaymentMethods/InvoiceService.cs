using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary.PaymentMethods
{
    public class InvoiceService 
    {
        private readonly IPayment _paymentProcessor;

        public InvoiceService(IPayment paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public void SendInvoice(PersonalAccount account, Invoice invoice)
        {
            //Skriver ut antingen om fakturan är betald eller inte beroende på om man har tillräkligt med saldo
            if (_paymentProcessor.Pay(invoice.Amount, account))
            {
                Console.WriteLine($"Invoice {invoice.InvoiceId} for {invoice.Amount} has been successfully sent to {account.AccountName}.");
            }
            else
            {
                Console.WriteLine($"Failed to send invoice {invoice.InvoiceId} for {invoice.Amount} to {account.AccountName}. Insufficient funds.");
            }
        }
    }
}
