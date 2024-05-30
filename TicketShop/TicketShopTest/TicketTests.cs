using System;
using TicketShopClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicketShopTest
{
    [TestClass]
    public class TicketTests
    {
        // Testar att alla grundläggande egenskaper hos Ticket-instansen initieras korrekt
        [TestMethod]
        public void CheckProperties()
        {
            int seatRow = 5;
            int seatNumber = 10;
            Ticket ticket = new Ticket(seatRow, seatNumber); 

            Assert.AreEqual(seatRow, ticket.SeatRow); 
            Assert.AreEqual(seatNumber, ticket.SeatNumber); 
            Assert.IsNotNull(ticket.TicketId); 
            Assert.IsFalse(ticket.IsReserved); 
        }

        // Testar att metod för att omvandla biljettens information till en sträng fungerar 
        [TestMethod]
        public void TicketFormat()
        {
            int[,] seats = new int[5, 10];
            double price = 150;
            Ticket ticket = new Ticket(5, 10);
            ticket.Section = new LocationSection("A", seats, price); 
            string expected = "Sektion: A\nRad: 5\nStol 10\nPris 150kr\n"; 

            string result = ticket.ToString(); 

            Assert.AreEqual(expected, result); 
        }

        // Testar att växla biljettens reservationsstatus
        [TestMethod]
        public void ToggleReservation()
        {
            Ticket ticket = new Ticket(1, 1);
            Assert.IsFalse(ticket.IsReserved);

            ticket.IsReserved = true;
            Assert.IsTrue(ticket.IsReserved); 

            ticket.IsReserved = false;
            Assert.IsFalse(ticket.IsReserved); 
        }

        // Testar konstruktören för att se om den skapar en giltig guid för TicketId
        [TestMethod]
        public void Ticket_ValidGuid()
        {
            Ticket ticket = new Ticket(2, 5);
            Guid result;
            bool isValidGuid = Guid.TryParse(ticket.TicketId, out result);

            Assert.IsTrue(isValidGuid);
        }

        // Testar att default-värdet för IsReserved är false vid initialisering
        [TestMethod]
        public void Ticket_DefaultIsReserved()
        {
            Ticket ticket = new Ticket(3, 7);
            Assert.IsFalse(ticket.IsReserved);
        }

        // Testar om korrekt sektionsdata initialiseras
        [TestMethod]
        public void Ticket_SectionInitialization()
        {
            int[,] seats = new int[10, 10];
            double price = 200;
            LocationSection section = new LocationSection("B", seats, price);
            Ticket ticket = new Ticket(1, 1);
            ticket.Section = section;

            Assert.AreEqual("B", ticket.Section.SectionName);
            Assert.AreEqual(price, ticket.Section.Price);
        }
    }
}
