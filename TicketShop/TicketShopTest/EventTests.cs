using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShopClassLibrary;
using Moq;
using static System.Collections.Specialized.BitVector32;

namespace TicketShopTest
{
    [TestClass]
    public class EventTests
    {
        private Mock<ILocation> _mockLocation;
        private Mock<ITicketController> _mockTicketController;
        private Event _event;
        private string _eventName = "Sample Event";
        private DateTime _eventReleaseDate = new DateTime(2024, 5, 1);
        private DateTime _eventDate = new DateTime(2024, 6, 1);

        [TestInitialize]
        public void Setup()
        {
            _mockLocation = new Mock<ILocation>();
            _mockLocation.Setup(l => l.LocationName).Returns("Sample Location");

            // Setup mock sections and layouts
            var mockSections = new List<LocationSection>
            {
                new LocationSection("Section A", new int[5, 10], 50.0),
                new LocationSection("Section B", new int[3, 15], 35.0)
            };

            _mockLocation.Setup(l => l.Sections).Returns(mockSections);

            _mockTicketController = new Mock<ITicketController>();

            // Setup the event with mocked location
            _event = new Event(_mockLocation.Object, _eventName, _eventReleaseDate, _eventDate);

            // Replace the ticket controller with a mock
            _event.GetType().GetField("_ticketController", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                  .SetValue(_event, _mockTicketController.Object);
        }
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            Assert.AreEqual(_eventName, _event.EventName);
            Assert.AreEqual(_eventReleaseDate, _event.EventReleaseDate);
            Assert.AreEqual(_eventDate, _event.EventDate);
            Assert.AreEqual(_mockLocation.Object, _event.EventLocation);
            Assert.IsNotNull(Guid.Parse(_event.EventId)); // Ensure it's a valid GUID
        }
        [TestMethod]
        public void GetAvailableTickets_ReturnsCorrectTickets()
        {
            var expectedTickets = new Dictionary<string, List<ITicket>>();
            _mockTicketController.Setup(tc => tc.GetAvailableTickets()).Returns(expectedTickets);

            var actualTickets = _event.GetAvailableTickets();

            Assert.AreEqual(expectedTickets, actualTickets);
        }
        [TestMethod]
        public void ReserveTicket_ReservesTicketCorrectly()
        {
            var mockTicket = new Mock<ITicket>();
            int row = 1, seat = 1;
            string section = "A";

            _mockTicketController.Setup(tc => tc.ReserveTicket(row, seat, section)).Returns(mockTicket.Object);

            var reservedTicket = _event.ReserveTicket(row, seat, section);

            Assert.AreEqual(mockTicket.Object, reservedTicket);
        }

        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            string expectedString = $"Event: {_eventName}\n" +
                                    $"Ticket Release: {_eventReleaseDate.ToString("yyyy-MM-dd HH:mm:ss")}\n" +
                                    $"Event Location: {_mockLocation.Object.LocationName}\n" +
                                    $"Event Date: {_eventDate.ToString("yyyy-MM-dd")}\n" +
                                    $"Event Time: {_eventDate.ToString("HH:mm")}\n" +
                                    $"EventID: {_event.EventId}\n";

            Assert.AreEqual(expectedString, _event.ToString());
        }

        [TestMethod]
        public void ReserveTicket_InvalidInput_ThrowsException()
        {
            int row = -1, seat = -1;
            string section = "NonExistentSection";

            _mockTicketController.Setup(tc => tc.ReserveTicket(row, seat, section)).Throws<ArgumentException>();

            Assert.ThrowsException<ArgumentException>(() => _event.ReserveTicket(row, seat, section));
        }
        [TestMethod]
        public void ReserveTicket_ReservingNonExistentSeat_ReturnsNull()
        {
            int row = 10, seat = 10; // Out of bounds
            string section = "A";

            _mockTicketController.Setup(tc => tc.ReserveTicket(row, seat, section)).Returns((ITicket)null);

            var reservedTicket = _event.ReserveTicket(row, seat, section);

            Assert.IsNull(reservedTicket);
        }
    }
}
