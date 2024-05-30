using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TicketShopClassLibrary;

namespace TicketShopTest
{
    [TestClass]
    public class ShoppingCartTests
    {
        private Mock<IEvent> _mockEvent;
        private Mock<IPayment> _mockPayment;
        private Mock<IAccount> _mockAccount;
        private ShoppingCart _shoppingCart;
        private const double TicketPrice = 50.0;

        [TestInitialize]
        public void Setup()
        {
            _mockEvent = new Mock<IEvent>();
            _mockPayment = new Mock<IPayment>();
            _mockAccount = new Mock<IAccount>();

            // Setting up the event mock to return a concrete Ticket
            _mockEvent.Setup(eventToBook => eventToBook.ReserveTicket(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                      .Returns((int row, int seat, string sectionName) => new Ticket(row, seat)
                      {
                          IsReserved = true,
                          Section = new LocationSection(sectionName, new int[10, 10], TicketPrice)
                      });

            _shoppingCart = new ShoppingCart(_mockEvent.Object);
        }

        [TestMethod]  
        public void Test_AddToCart_WithinLimit() //Testar om shoppingcarten kan hålla en biljett
        {
            _shoppingCart.AddToCart(1, 1, "A");
            Assert.AreEqual(1, _shoppingCart.ListOfReservedTickets.Count);
        }

        [TestMethod]
        public void Test_AddToCart_BeyondLimit() //Testar att lägga till för många biljetter
        {
            for (int i = 0; i < _shoppingCart.TicketLimit; i++)
            {
                _shoppingCart.AddToCart(i, i, "A");
            }
            _shoppingCart.AddToCart(6, 6, "A");
            Assert.AreEqual(_shoppingCart.TicketLimit, _shoppingCart.ListOfReservedTickets.Count);
        }

        [TestMethod]
        public void Test_TimeLimit_Exceeded()  //Testar om det går att boka efter att tidsgränsen gått ut
        {
            _shoppingCart.StartTimeLimit();
            Thread.Sleep(11000); // Sleep for 11 seconds to exceed the time limit
            var customer = new Customer("Test Name", "test@example.com", "1234567890", _mockAccount.Object);
            Assert.IsFalse(_shoppingCart.HandlePayment(_mockPayment.Object, customer));
        }

        [TestMethod]
        public void Test_Payment_WithinTimeLimit() //Testar att lägga till inom tidsramen
        {
            _shoppingCart.StartTimeLimit();
            _shoppingCart.AddToCart(1, 1, "A");
            _mockPayment.Setup(payment => payment.Pay(It.IsAny<double>(), It.IsAny<Customer>())).Returns(true);
            var customer = new Customer("Test Name", "test@example.com", "1234567890", _mockAccount.Object);

            Assert.IsTrue(_shoppingCart.HandlePayment(_mockPayment.Object, customer));
        }

        [TestMethod]
        public void Test_Payment_AfterTimeLimit() //testar betalning efter tidsgränsen
        {
            _shoppingCart.StartTimeLimit();
            _shoppingCart.AddToCart(1, 1, "A");
            Thread.Sleep(11000); // Sleep for 11 seconds to exceed the time limit
            var customer = new Customer("Test Name", "test@example.com", "1234567890", _mockAccount.Object);

            Assert.IsFalse(_shoppingCart.HandlePayment(_mockPayment.Object, customer));
        }

        [TestMethod]
        public void Test_GetCost_Calculation() //Räknar ut totalt pris för biljetterna
        {
            _shoppingCart.AddToCart(1, 1, "A");
            _shoppingCart.AddToCart(2, 2, "A");
            Assert.AreEqual(2 * TicketPrice, _shoppingCart.GetCost());
        }

        [TestMethod]
        public void Test_ReleaseTickets_OnDispose() //Testar att släppa tillbaka biljetter
        {
            _shoppingCart.AddToCart(1, 1, "A");
            _shoppingCart.Dispose();

            foreach (var ticket in _shoppingCart.ListOfReservedTickets)
            {
                Assert.IsFalse(ticket.IsReserved);
            }
        }
    }
}

