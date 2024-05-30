using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;


    public interface IPersonalAccount : IAccount
    {
        Guid AccountId { get; set; }
        string AccountName { get; set; }
        double AccountBalance { get; set; }

        bool Withdraw(double amount);
        void Deposit(double amount);
    }
