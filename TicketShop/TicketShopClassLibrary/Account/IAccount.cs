using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public interface IAccount
{
    Guid AccountId { get; set; }
    string AccountName { get; set; }
    double AccountBalance { get; set; }

    public bool Withdraw(double amount);
    public void Deposit(double amount);
}
