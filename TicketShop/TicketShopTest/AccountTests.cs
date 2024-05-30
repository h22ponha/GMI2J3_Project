using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TicketShopClassLibrary;
namespace TicketShopTest

{
    [TestClass]
    public class AccountTests
    {
        private PersonalAccount _account;

        [TestInitialize]
        //Definerar Personal account med "Test account" Med en summa p� 1000.
        public void Setup()
        {
            _account = new PersonalAccount("Test Account", 1000);
        }

        [TestMethod]
        //Kollar om det g�r att s�tta in pengar p� ett konto och se att summan �kar.
        public void Deposit_IncreasesBalance()
        {
            _account.Deposit(500);
            Assert.AreEqual(1500, _account.AccountBalance);
        }

        [TestMethod]
        //Kollar om summan p� account minskar vid uttag.
        public void Withdraw_DecreasesBalance()
        {
            bool result = _account.Withdraw(500);
            Assert.IsTrue(result);
            Assert.AreEqual(500, _account.AccountBalance);
        }

        [TestMethod]
        //Kollar om det g�r att ta ut mer pengar �n det som finns p� account.
        public void Withdraw_ReturnsFalse_WhenAmountIsGreaterThanBalance()
        {
            bool result = _account.Withdraw(1500);
            Assert.IsFalse(result);
            Assert.AreEqual(1000, _account.AccountBalance);
        }

        [TestMethod]
        //Kollar om det g�r att skapa ett nytt account med ett nytt v�rde.
        public void NewAccount_SetsInitialValues()
        {
            var newAccount = new PersonalAccount("New Account", 2000);
            Assert.AreEqual("New Account", newAccount.AccountName);
            Assert.AreEqual(2000, newAccount.AccountBalance);
            Assert.AreNotEqual(Guid.Empty, newAccount.AccountId);
        }

        [TestMethod]
        //Kollar om programmet inte tar emot en negativ summa.
        public void Deposit_DoesNotAcceptNegativeAmount()
        {
            _account.Deposit(-500);
            Assert.AreEqual(1000, _account.AccountBalance);
        }

        [TestMethod]
        //Kollar om summan p� kontot �r r�tt n�r den skapas.
        public void AccountBalance_IsCorrect()
        {
            Assert.AreEqual(1000, _account.AccountBalance);
        }  
    }
}