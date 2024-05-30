using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShopClassLibrary;
using Moq;

namespace TicketShopClassLibrary.Tests
{
    [TestClass]
    public class EventControllerTests
    {
        [TestMethod]
        public void CheckEventReleaseDate_ValidReleaseDate_ReturnsTrue()
        {
            // Arrange
            var eventController = new EventController();
            DateTime validReleaseDate = DateTime.Now.AddDays(1);

            // Act
            bool result = eventController.CheckEventReleaseDate(validReleaseDate);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckEventReleaseDate_PastReleaseDate_ReturnsFalse()
        {
            // Arrange
            var eventController = new EventController();
            DateTime pastReleaseDate = DateTime.Now.AddDays(-1);

            // Act
            bool result = eventController.CheckEventReleaseDate(pastReleaseDate);

            // Assert
            Assert.IsFalse(result);
        }

        
    }
}
