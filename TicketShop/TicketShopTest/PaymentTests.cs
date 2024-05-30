using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TicketShopClassLibrary;
using TicketShopClassLibrary.PaymentMethods;

namespace PaymentTests
{
    [TestClass]
    public class PersonalAccountTests
    {
        //Test som visar att man kan dra ut mer eller lika med 100
        [TestMethod]
        public void Test_Withdraw_BalanceMoreOrEqualTo100()
        {
            var account = new PersonalAccount("John Doe", 150);
            double withdrawalAmount = 100; // Set the withdrawal amount
            bool result = account.Withdraw(withdrawalAmount);

            Assert.IsTrue(result, "Withdrawal should succeed when balance is greater than or equal to 100.");
        }

        //Test som visar att man inta ska kunna ta ut 100 om man har en för lite saldo
        [TestMethod]
        public void Test_Withdraw_BalanceLessThan100()
        {
            var account = new PersonalAccount("Jane Smith", 50);
            double withdrawalAmount = 100; // Set the withdrawal amount
            bool result = account.Withdraw(withdrawalAmount);

            Assert.IsFalse(result, "Withdrawal should fail when balance is less than 100.");
        }
    }
        
    [TestClass]
    public class InvoiceServiceTests
    {
        //Test där man skickar en faktura tii ett personkontot då man har tillräcklig med saldo för att betala fakturan
        [TestMethod]
        public void SendInvoice_PaymentSucceeds()
        {
            var paymentMock = new Mock<IPayment>();
            paymentMock.Setup(p => p.Pay(It.IsAny<double>(), It.IsAny<PersonalAccount>())).Returns(true);

            var invoiceService = new InvoiceService(paymentMock.Object);
            var account = new PersonalAccount("Test Account", 1000);
            var invoice = new Invoice("Test Description", 400);

            using (var consoleOutput = new ConsoleOutput())
            {
                invoiceService.SendInvoice(account, invoice);

                var expectedMessage = $"Invoice {invoice.InvoiceId} for {invoice.Amount} has been successfully sent to {account.AccountName}.";
                Assert.IsTrue(consoleOutput.GetOuput().Contains(expectedMessage));
            }
        }

        //Test där man skickar en faktura till ett personkontot då man inte har tillräcklig med saldo för att betala fakturan
        [TestMethod]
        public void SendInvoice_PaymentFails()
        {
            var paymentMock = new Mock<IPayment>();
            paymentMock.Setup(p => p.Pay(It.IsAny<double>(), It.IsAny<PersonalAccount>())).Returns(false);

            var invoiceService = new InvoiceService(paymentMock.Object);

            var account = new PersonalAccount("Test Account", 400);
            var invoice = new Invoice("Test Description", 800);

            using (var consoleOutput = new ConsoleOutput())
            {
                invoiceService.SendInvoice(account, invoice);

                var expectedMessage = $"Failed to send invoice {invoice.InvoiceId} for {invoice.Amount} to {account.AccountName}. Insufficient funds.";
                Assert.IsTrue(consoleOutput.GetOuput().Contains(expectedMessage));
            }
        }

        //Testar när man betalar med ett ogiltigt saldo
        [TestClass]
        public class InvoicePaymentTests
        {
            [TestMethod]
            public void TestPayWithInvalidAmount()
            {
                var invoicePayment = new InvoicePayment();
                var account = new PersonalAccount("TestAccount", 100.0);

                Assert.ThrowsException<FormatException>(() =>
                {
                    double invalidAmount = Convert.ToDouble("A");
                    invoicePayment.Pay(invalidAmount, account);
                });
            }
        }
    }

    //Hjälper klassen att hämta outputen ur konsolen 
    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
        
    
}