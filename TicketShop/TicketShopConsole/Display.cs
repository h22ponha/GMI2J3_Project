using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TicketShopClassLibrary;

namespace TicketShopConsole;

internal static class Display
{

    public static void PrintAvailableEvents(List<IEvent> events)
    {
        int count = 1;
        foreach (IEvent eventItem in events)
        {
            
            Console.WriteLine($"{count}: {eventItem.EventName.PadRight(20)} {eventItem.EventDate}\t Biljetter släpps: {eventItem.EventReleaseDate}");
            count++;
        }
    }
    
    /// <summary>
    /// Skriver ut alla tillgängliga säten för valt event
    /// </summary>
    /// <param name="eventChoice"></param>
    public static void PrintAvailableSeats(IEvent eventChoice)
    {
        Console.WriteLine($"Lediga platser för {eventChoice.EventName}: \n");
        Dictionary<string, List<ITicket>> availableTickets = eventChoice.GetAvailableTickets();
        foreach (string section in availableTickets.Keys)
        {

            Console.WriteLine("Sektion " + section + ", Pris: " + availableTickets[section][0].Section.Price + "kr");
            
            int lastRow = -1;
            foreach (ITicket ticket in availableTickets[section])
            {
                lastRow = PrintRowNumber(lastRow, ticket);

                Console.Write("[" + (!ticket.IsReserved ? ticket.SeatNumber : "X") + "] ");


                if (ticket.SeatNumber % ticket.Section.Layout.GetLength(1) == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        
    }

    public static void PrintAvailableSeats(IEvent eventChoice, string section)
    {
        
        Dictionary<string, List<ITicket>> availableTickets = eventChoice.GetAvailableTickets();
        Console.WriteLine($"Lediga platser för {section}");
        Console.WriteLine("Sektion " + section + ":");
        int lastRow = -1;
        foreach (ITicket ticket in availableTickets[section])
        {
            lastRow = PrintRowNumber(lastRow, ticket);
            Console.Write("[" + (!ticket.IsReserved ? ticket.SeatNumber : "X") + "] ");
            if (ticket.SeatNumber % ticket.Section.Layout.GetLength(1) == 0)
            {
                Console.WriteLine();
            }
        }
        Console.WriteLine();
    }
    public static void PrintAvailableSeats(IEvent eventChoice, int row, string section)
    {
        Console.WriteLine("Sektion " + section + ":");
        foreach (ITicket ticket in eventChoice.GetAvailableTickets()[section])
        {
            if (ticket.SeatRow == row)
                Console.Write("[" + (!ticket.IsReserved ? ticket.SeatNumber : "X") + "] ");
        }
        Console.WriteLine();

        //Console.WriteLine($"Lediga platser för {eventChoice.EventName} och radnummer {row}: ");
        //Dictionary<string, List<ITicket>> availableTickets = eventChoice.GetAvailableTickets();
        //foreach (ITicket ticket in availableTickets[row])
        //{
        //    Console.Write("[" + (!ticket.IsReserved ? ticket.SeatNumber : "X") + "] ");
        //}

    }
    
    public static void PrintAvailableSeats(IEvent eventChoice, int row)
    {
        foreach (string section in eventChoice.GetAvailableTickets().Keys)
        {
            Console.WriteLine("Sektion " + section + ":");
            int lastRow = -1;
            foreach (ITicket ticket in eventChoice.GetAvailableTickets()[section])
            {
                lastRow = PrintRowNumber(lastRow, ticket);
                if (ticket.SeatRow == row)
                    Console.Write("[" + (!ticket.IsReserved ? ticket.SeatNumber : "X") + "] ");
            }
            Console.WriteLine();
        }

    }
    public static void PrintQuestion(string question)
    {
        Console.WriteLine("Välj " + question);
    }
    public static void PrintCart(IShoppingCart cart)
    {
        Console.WriteLine($"Din bokning för '{cart.ListOfReservedTickets.Count}' antal biljetter till eventet '{cart.EventToBook.EventName}': \n");
        foreach (ITicket ticket in cart.ListOfReservedTickets)
        {
            Console.WriteLine(ticket.ToString());
        }
        Console.WriteLine($"Total kostnad: {cart.GetCost()} kr");
    }
    public static void PrintChosenNumberOfTickets(int chosen, int limit)
    {
        Console.Clear();
        if (chosen > limit)
        {
            Console.WriteLine($"Du har försökt boka {chosen} biljetter. Max antal biljetter är {limit}. Vänligen välj platser för {limit} biljetter.");
        }
        else
        {
            Console.WriteLine($"Du har valt {chosen} biljetter. Vänligen välj önskad sittplats");
        }
    }
    static int PrintRowNumber(int lastRow, ITicket ticket)
    {
        if (ticket.SeatRow != lastRow)
        {
            Console.Write($"Rad {ticket.SeatRow}:\t");
            lastRow = ticket.SeatRow;
        }
        return lastRow;
    }
    public static void PrintTicketsLeftToReserve(int ticketsLeft)
    {
        Console.WriteLine($"Du har {ticketsLeft} biljetter kvar att boka");
    }
    public static void PrintPurchaseSuccess(bool success, Customer customer)
    {
        Console.WriteLine("Betalningen " + (success ? $"lyckades! Hurra! Biljetter skickas till {customer.CustomerEmail}" : "misslyckades. Försök igen"));
    }
    public static void PrintPaymentOptions()
    {
        Console.WriteLine("Välj betalningsmetod: ");
        Console.WriteLine("1: Swish");
        Console.WriteLine("2: Faktura");
    }
    public static void PrintAskForPhoneNumber()
    {
        Console.WriteLine("Skriv in ditt telefonnummer");
    }
    public static void PrintContinueToPayment()
    {
        Console.WriteLine("Fortsätt till betalning? Biljetterna är reserverade i 10 minuter. Tryck enter för att fortsätta. Skriv nej för att avbryta bokning");
    }
    public static void PrintTimeLeft(DateTime timeLimit)
    {
        Console.WriteLine($"Slutför betalning innan {timeLimit}");
    }
    public static void PrintError()
    {
        Console.WriteLine("Misslyckades. Vänligen försök igen");
    }

    internal static void PrintPurchase(bool success, IShoppingCart cart)
    {
        if (success)
        {
            Console.WriteLine("Din beställning: \n\n");
            Console.WriteLine(cart.EventToBook.EventName);
            foreach (ITicket ticket in cart.ListOfReservedTickets)
            {
                Console.WriteLine(ticket.ToString());
            }
        }
    }
}
