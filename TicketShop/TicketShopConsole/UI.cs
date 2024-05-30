using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShopClassLibrary;
using TicketShopClassLibrary.PaymentMethods;

namespace TicketShopConsole;

internal static class UI
{
    // Metod för att köpa biljetter
    public static void BuyTicket(IEventController eventController)
    {
        // Skapar en kund i testsyfte
        Customer customer = new Customer("Exempel", "exempel@exempel.com", "1234987650", new PersonalAccount("Exempel", 900));

        // Loop för att köpa biljetter
        while(true)
        {
            IEvent chosenEvent = null;
            // Skriver ut alla events och låter användaren välja ett event och antal biljetter
            bool hej = false;
            Display.PrintAvailableEvents(eventController.ListOfEvents);
            while (hej == false)
            {
                
                chosenEvent = eventController.ListOfEvents[UserChoice.Event() - 1];

                if (eventController.CheckEventReleaseDate(chosenEvent.EventReleaseDate) != true)
                {
                    Console.WriteLine("Biljetterna till detta event släpps " + chosenEvent.EventReleaseDate);
                    Console.WriteLine("Vänligen välj ett annat event");
                } else
                {
                    hej = true;
                }
            }

            int numberOfTickets = UserChoice.NumberOfTickets();
            IShoppingCart cart = new ShoppingCart(chosenEvent);
            
            Display.PrintChosenNumberOfTickets(numberOfTickets, cart.TicketLimit);
            numberOfTickets = numberOfTickets > cart.TicketLimit ? 5 : numberOfTickets;
            
            // Loop för att välja platser som även skriver ut lediga platser
            for (int i = numberOfTickets; i > 0; i--)
            {
                Display.PrintTicketsLeftToReserve(i);
                Display.PrintAvailableSeats(chosenEvent);

                Console.WriteLine("Skriv in sektionsnamn");
                string section = Console.ReadLine();

                Display.PrintAvailableSeats(chosenEvent, section);

                int row = UserChoice.Row();

                Display.PrintAvailableSeats(chosenEvent, row, section);
                
                int seat = UserChoice.Seat();

                cart.AddToCart(row, seat, section);
                Console.Clear();
            }

            // Skriver ut kundvagnen och frågar om användare önskar fortsätta till betalning
            Display.PrintCart(cart);
            Display.PrintContinueToPayment();
            
            // Hanterar val och skickar vidare till betalning
            if (Console.ReadLine().ToLower() == "")
            {
                HandlePayment(cart, customer);

            }
            else
            {
                Console.WriteLine("Bokning avbruten");
                cart.ReleaseTickets();
            }
            Thread.Sleep(4000);
            Console.Clear();
        }
    }

    // Hanterar betalning
    public static void HandlePayment(IShoppingCart cart, Customer customer)
    {
        // Skriver ut tidsbegränsning och betalningsalternativ
        Display.PrintTimeLeft(cart.StartTimeLimit());
        Display.PrintPaymentOptions();

        bool success = false;
        
        // Hanterar val av betalningsalternativ och anropar betalningsmetod
        switch (UserChoice.PaymentMethod())
        {
            case "1":
                Display.PrintAskForPhoneNumber();
                string phoneNumber = UserChoice.PhoneNumber();
                success = cart.HandlePayment(new SwishPayment(), customer);
                break;
            case "2":
                success = cart.HandlePayment(new InvoicePayment(), customer);
                break;
            default:
                Display.PrintError();
                break;
        }

        Display.PrintPurchaseSuccess(success, customer);
        Display.PrintPurchase(success, cart);
        cart.Dispose();
    }
}
