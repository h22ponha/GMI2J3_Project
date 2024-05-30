namespace TicketShopClassLibrary;

public interface ITicket
{
    string TicketId { get; set; }
    DateTime PurchaseDate { get; set; }
    bool IsReserved { get; set; }
    int SeatRow { get; set; }
    int SeatNumber { get; set; }
    public LocationSection Section { get; set; }
}