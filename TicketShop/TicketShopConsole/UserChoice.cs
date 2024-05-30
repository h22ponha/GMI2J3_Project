using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopConsole;

internal static class UserChoice
{
    public static int Event()
    {
        Display.PrintQuestion("ett event");
        return int.Parse(Console.ReadLine());
    }
    public static int NumberOfTickets()
    {
        Display.PrintQuestion("hur många biljetter du vill boka");
        return int.Parse(Console.ReadLine());
    }

    public static int Row()
    {
        Display.PrintQuestion("en rad");
        return int.Parse(Console.ReadLine());
    }
    public static int Seat()
    {
        Display.PrintQuestion("en stol");
        return int.Parse(Console.ReadLine());
    }
    public static string PaymentMethod()
    {
        Display.PrintQuestion("ett betalningsalternativ");
        return Console.ReadLine();
    }

    public static string PhoneNumber()
    {
        return Console.ReadLine();
    }
}
