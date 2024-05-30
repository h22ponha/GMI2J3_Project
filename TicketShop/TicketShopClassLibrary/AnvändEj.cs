//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TicketShopClassLibrary;
//internal class Misslyckat
//{
//    
//    public bool CheckNumberOfTickets()
//    {
//        if (ListOfReservedTickets.Count > TicketLimit)
//        {
//            return false;
//        }
//        return true;
//    }

//public void TimeCountDown()
//{
//    if (ListOfReservedTickets != null)
//    {
//        System.Timers.Timer timer = new System.Timers.Timer(1000);
//        timer.Start();
//        Console.WriteLine(timer);

//    }
//}
//}

//public void HejHans(int row, int seat, string section)
//{
//    if (CheckNumberOfTickets())
//    {
//        Hej hans * 2;
//    }

// Returnerar en sträng med all info om alla events
//public string GetAllEventInfo()
//{
//    string toReturn = "";

//    foreach(IEvent eventItem in ListOfEvents)
//    {
//        toReturn += eventItem.ToString();
//    }
//    return toReturn;
//}

//// Retrunerar en 
//public Dictionary<int, List<ITicket>>? GetAvailableTickets(string eventName)
//{
//    foreach(IEvent e in ListOfEvents)
//    {
//        if (e.EventName == eventName)
//        {
//            return e.GetAvailableTickets();
//        }
//    }
//    return null;
//}

//public IEvent? GetEvent(string eventName)
//{
//    foreach (IEvent e in ListOfEvents)
//    {
//        if (e.EventName == eventName)
//        {
//            return e;
//        }
//    }
//    return null;
//}


//Dictionary<int, List<ITicket>> availableTickets = new Dictionary<int, List<ITicket>>();
//foreach (int row in DictOfTickets.Keys)
//{
//    availableTickets[row] = new List<ITicket>();
//    foreach (ITicket ticket in DictOfTickets[row])
//    {
//        if (ticket.IsReserved == false)
//        {
//            availableTickets[row].Add(ticket);

//        }
//    }
//}

//foreach (int row in availableTickets.Keys)
//{
//    Console.Write("Rad " + row + ":\t");
//    foreach (ITicket ticket in availableTickets[row])
//    {
//        Console.Write("[" + (!ticket.IsReserved ? ticket.SeatNumber : "X") + "] ");
//    }
//    Console.WriteLine();

//}

//Console.WriteLine(count + ": " + eventItem.EventName + ", " + eventItem.EventDate +"\t\t\tBiljetter släpps: " + eventItem.EventReleaseDate);

