using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Tests
{
    [TestClass]
    public class TransferControllerTests
    {
        private BankController bank = null!;
        private TransferController transfers = null!;

        [TestInitialize]
        public void Setup()
        {
            bank = new BankController();
            transfers = bank.Transfers;
        }

        [TestMethod]
        public void TransferFunds_ValidRequest_MovesFundsBetweenOwnAccounts()
        {
            User customer = bank.Customers.GetCustomerByNumber("C-2026-001")!;
            decimal sourceBefore = customer.Accounts[0].Balance;
            decimal destBefore = customer.Accounts[1].Balance;

            transfers.TransferFunds("C-2026-001", 0, 1, 100m);

            Assert.AreEqual(sourceBefore - 100m, customer.Accounts[0].Balance);
            Assert.AreEqual(destBefore + 100m, customer.Accounts[1].Balance);
        }

        [TestMethod]
        public void TransferFunds_SameSourceAndDestination_Throws()
        {
            Assert.ThrowsExactly<BankingException>(() => transfers.TransferFunds("C-2026-001", 0, 0, 50m));
        }

        [TestMethod]
        public void TransferFunds_InsufficientEverydayFunds_ThrowsAndLeavesBalancesUntouched()
        {
            User customer = bank.Customers.GetCustomerByNumber("C-2026-001")!;
            decimal sourceBefore = customer.Accounts[0].Balance;
            decimal destBefore = customer.Accounts[1].Balance;

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds("C-2026-001", 0, 1, 999_999m));

            Assert.AreEqual(sourceBefore, customer.Accounts[0].Balance);
            Assert.AreEqual(destBefore, customer.Accounts[1].Balance);
        }

        [TestMethod]
        public void TransferFunds_StaffFailedTransfer_ChargesHalfFee()
        {
            User staff = bank.Customers.GetCustomerByNumber("C-2026-002")!;
            decimal investmentBefore = staff.Accounts[1].Balance;

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds("C-2026-002", 1, 0, 999_999m));

            Assert.AreEqual(investmentBefore - (InvestmentAccount.FailedFee * 0.5m), staff.Accounts[1].Balance);
        }

        [TestMethod]
        public void TransferFunds_UnknownCustomer_Throws()
        {
            Assert.ThrowsExactly<BankingException>(() => transfers.TransferFunds("C-9999", 0, 1, 10m));
        }
    }
}
