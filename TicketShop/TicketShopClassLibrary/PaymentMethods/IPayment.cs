namespace TicketShopClassLibrary;

public interface IPayment
{
    bool Pay(double amount, PersonalAccount account);
    bool Pay(double v, Customer customer);
}