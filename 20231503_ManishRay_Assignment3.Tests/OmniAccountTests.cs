using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Tests
{
    [TestClass]
    public class OmniAccountTests
    {
        // Test withdrawal into overdraft limit
        [TestMethod]
        public void Withdraw_WithinOverdraftLimit_Succeeds()
        {
            var acc = new OmniAccount(overdraftLimit: 500m, initialBalance: 750m);
            // Available total = $750 + $500 = $1250. Withdraw $1000 -> Balance = -$250
            acc.Withdraw(1000m);
            Assert.AreEqual(-250m, acc.Balance);
        }

        // Edge case test: Exceeding overdraft limit by exactly $0.01
        [TestMethod]
        public void Withdraw_ExceedingOverdraftLimitByOneCent_ThrowsException()
        {
            var acc = new OmniAccount(overdraftLimit: 500m, initialBalance: 750m);
            // Available = $1250. Attempt $1250.01
            try
            {
                acc.Withdraw(1250.01m, isStaff: false);
                Assert.Fail("Should throw InsufficientFundsException");
            }
            catch (InsufficientFundsException ex)
            {
                // Balance decreases by $5.00 failed fee -> $750 - $5 = $745
                Assert.AreEqual(745m, acc.Balance);
                Assert.IsTrue(ex.Message.Contains("Omni Account withdrawal failed"));
            }
        }

        // Test interest calculation above $1000 threshold
        [TestMethod]
        public void CalculateInterest_AboveThreshold_AppliesInterest()
        {
            var acc = new OmniAccount(overdraftLimit: 500m, initialBalance: 1500m);
            acc.CalculateInterest();
            // $1500 * 4% = $60 -> $1560
            Assert.AreEqual(1560m, acc.Balance);
        }

        // Test interest calculation below $1000 threshold
        [TestMethod]
        public void CalculateInterest_BelowThreshold_NoInterestApplied()
        {
            var acc = new OmniAccount(overdraftLimit: 500m, initialBalance: 750m);
            acc.CalculateInterest();
            Assert.AreEqual(750m, acc.Balance);
        }
    }
}
