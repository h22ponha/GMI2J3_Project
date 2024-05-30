using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public class PersonalAccount : IPersonalAccount
{
    public Guid AccountId { get; set; }
    public string AccountName { get; set; }
    public double AccountBalance { get; set; }

    // Konstruktor som skapar ett nytt konto med ett namn och en balans och ett unikt id med Guid
    public PersonalAccount(string accountName, double balance)
    {
        AccountId = Guid.NewGuid();
        AccountName = accountName;
        AccountBalance = balance;
    }
    // Tar ut amount från kontot om det finns tillräckligt med pengar
    public bool Withdraw(double amount)
    {
        if (AccountBalance >= amount)
        {
            AccountBalance -= amount;
            return true;
        }
        return false;
    }
    // Fyller på kontot med amount
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            AccountBalance += amount;
        }
    }
}




