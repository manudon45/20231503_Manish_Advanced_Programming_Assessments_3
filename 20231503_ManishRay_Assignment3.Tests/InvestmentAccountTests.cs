using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Tests
{
    [TestClass]
    public class InvestmentAccountTests
    {
        // Test successful withdrawal
        [TestMethod]
        public void Withdraw_ValidAmount_DecreasesBalance()
        {
            var acc = new InvestmentAccount(0.05m, 1000m);
            acc.Withdraw(400m);
            Assert.AreEqual(600m, acc.Balance);
        }

        // Test failed withdrawal applies fee and throws exception
        [TestMethod]
        public void Withdraw_ExceedingBalance_DeductsFeeAndThrowsException()
        {
            var acc = new InvestmentAccount(0.05m, 1000m);
            try
            {
                acc.Withdraw(1200m, isStaff: false);
                Assert.Fail("Should have thrown InsufficientFundsException");
            }
            catch (InsufficientFundsException ex)
            {
                // Balance should decrease by $2.50 fee
                Assert.AreEqual(997.50m, acc.Balance);
                Assert.IsTrue(ex.Message.Contains("Investment Account withdrawal failed"));
            }
        }

        // Test 50% fee discount for bank staff on failed withdrawal
        [TestMethod]
        public void Withdraw_ExceedingBalance_StaffUser_DeductsHalfFee()
        {
            var acc = new InvestmentAccount(0.05m, 1000m);
            try
            {
                acc.Withdraw(1200m, isStaff: true);
                Assert.Fail("Should have thrown InsufficientFundsException");
            }
            catch (InsufficientFundsException)
            {
                // Staff fee is 50% of $2.50 = $1.25
                Assert.AreEqual(998.75m, acc.Balance);
            }
        }

        // Test interest calculation
        [TestMethod]
        public void CalculateInterest_AddsCorrectInterest()
        {
            var acc = new InvestmentAccount(0.05m, 1000m);
            acc.CalculateInterest();
            // $1000 * 5% = $50 interest -> Total $1050
            Assert.AreEqual(1050m, acc.Balance);
        }
    }
}
