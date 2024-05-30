using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public class Ticket : ITicket
{
    public string TicketId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public bool IsReserved { get; set; } = false;
    public int SeatRow { get; set; }
    public int SeatNumber { get; set; }
    public LocationSection Section { get; set; }


    // Skapar ett unikt id för varje biljett och ger den en rad och en plats
    public Ticket(int seatRow, int seatColumn)
    {
        TicketId = Guid.NewGuid().ToString();
        SeatNumber = seatColumn;
        SeatRow = seatRow;
    }

    // Används för att skriva ut information om biljetten
    public override string ToString()
    {
        return "Sektion: " + Section.SectionName +
            "\nRad: " + SeatRow.ToString() +
            "\nStol " + SeatNumber.ToString() +
            "\nPris " + Section.Price + "kr" +
            "\n";
    }
}

