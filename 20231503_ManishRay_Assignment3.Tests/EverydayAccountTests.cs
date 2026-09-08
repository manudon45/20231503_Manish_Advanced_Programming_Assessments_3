using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Tests
{
    [TestClass]
    public class EverydayAccountTests
    {
        // Test successful deposit
        [TestMethod]
        public void Deposit_PositiveAmount_IncreasesBalance()
        {
            var acc = new EverydayAccount(500m);
            acc.Deposit(200m);
            Assert.AreEqual(700m, acc.Balance);
        }

        // Test invalid deposit amount
        [TestMethod]
        public void Deposit_ZeroOrNegativeAmount_ThrowsBankingException()
        {
            var acc = new EverydayAccount(500m);
            try
            {
                acc.Deposit(0m);
                Assert.Fail("Expected BankingException was not thrown.");
            }
            catch (BankingException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Deposit Failed"));
            }
        }

        // Test withdrawing exact balance amount (edge case)
        [TestMethod]
        public void Withdraw_ExactBalanceAmount_SetsBalanceToZero()
        {
            var acc = new EverydayAccount(500m);
            acc.Withdraw(500m);
            Assert.AreEqual(0m, acc.Balance);
        }

        // Test invalid withdrawal amount ($0)
        [TestMethod]
        public void Withdraw_ZeroAmount_ThrowsBankingException()
        {
            var acc = new EverydayAccount(500m);
            try
            {
                acc.Withdraw(0m);
                Assert.Fail("Expected BankingException was not thrown.");
            }
            catch (BankingException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Withdrawal Failed"));
            }
        }

        // Test overdraft attempt (exceeding balance)
        [TestMethod]
        public void Withdraw_ExceedingBalance_ThrowsInsufficientFundsException()
        {
            var acc = new EverydayAccount(500m);
            try
            {
                acc.Withdraw(500.01m);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Everyday Account withdrawal failed"));
            }
        }
    }
}
