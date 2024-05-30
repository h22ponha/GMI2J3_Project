namespace TicketShopClassLibrary;

public interface ILocation
{
    public string LocationName { get; set; }
    public int Capacity { get; set; }
    public string Address { get; set; }
    public List<LocationSection> Sections { get; set; }
}