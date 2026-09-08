using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Tests
{
    [TestClass]
    public class CustomerControllerTests
    {
        private CustomerController controller = null!;

        [TestInitialize]
        public void Setup()
        {
            controller = new CustomerController();
        }

        // Test initial seeded customers load
        [TestMethod]
        public void GetAllCustomers_ReturnsSeededCustomers()
        {
            var customers = controller.GetAllCustomers();
            Assert.IsTrue(customers.Count >= 2);
        }

        // Test adding a new customer
        [TestMethod]
        public void AddCustomer_ValidInput_IncreasesCount()
        {
            int initialCount = controller.GetCustomerCount();
            bool result = controller.AddCustomer("Test Customer", "021-111-222", false, "", 500m, 0.05m, 1000m, 500m, 750m);

            Assert.IsTrue(result);
            Assert.AreEqual(initialCount + 1, controller.GetCustomerCount());
        }

        // Test updating customer details
        [TestMethod]
        public void UpdateCustomer_ValidId_UpdatesDetails()
        {
            bool result = controller.UpdateCustomer("C-2026-001", "Manish Updated", "newemail@test.com");
            Assert.IsTrue(result);

            User? updated = controller.GetCustomerByNumber("C-2026-001");
            Assert.IsNotNull(updated);
            Assert.AreEqual("Manish Updated", updated.Name);
            Assert.AreEqual("newemail@test.com", updated.ContactDetails);
        }

        // Test adding a new account to an existing customer at runtime
        [TestMethod]
        public void AddAccountToCustomer_ValidType_GrowsAccountList()
        {
            User? customer = controller.GetCustomerByNumber("C-2026-001");
            Assert.IsNotNull(customer);
            int before = customer.Accounts.Count;

            bool result = controller.AddAccountToCustomer("C-2026-001", "investment", 2500m, 0.07m);

            Assert.IsTrue(result);
            Assert.AreEqual(before + 1, customer.Accounts.Count);
            Assert.IsInstanceOfType(customer.Accounts[^1], typeof(InvestmentAccount));
        }

        // Test adding an account with an unknown type is rejected
        [TestMethod]
        public void AddAccountToCustomer_UnknownType_ReturnsFalse()
        {
            bool result = controller.AddAccountToCustomer("C-2026-001", "crypto", 100m, 0m);
            Assert.IsFalse(result);
        }

        // Test a valid intra-account transfer moves funds between the customer's own accounts
        [TestMethod]
        public void TransferFunds_ValidRequest_MovesFundsBetweenOwnAccounts()
        {
            User customer = controller.GetCustomerByNumber("C-2026-001")!;
            decimal sourceBefore = customer.Accounts[0].Balance;
            decimal destBefore = customer.Accounts[1].Balance;

            controller.TransferFunds("C-2026-001", 0, 1, 100m);

            Assert.AreEqual(sourceBefore - 100m, customer.Accounts[0].Balance);
            Assert.AreEqual(destBefore + 100m, customer.Accounts[1].Balance);
        }

        // Test transferring to the same account is rejected
        [TestMethod]
        public void TransferFunds_SameSourceAndDestination_Throws()
        {
            Assert.ThrowsExactly<BankingException>(
                () => controller.TransferFunds("C-2026-001", 0, 0, 50m));
        }

        // Test an Everyday transfer over balance fails without moving any money
        [TestMethod]
        public void TransferFunds_InsufficientEverydayFunds_ThrowsAndLeavesBalancesUntouched()
        {
            User customer = controller.GetCustomerByNumber("C-2026-001")!;
            decimal sourceBefore = customer.Accounts[0].Balance;
            decimal destBefore = customer.Accounts[1].Balance;

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => controller.TransferFunds("C-2026-001", 0, 1, 999_999m));

            Assert.AreEqual(sourceBefore, customer.Accounts[0].Balance);
            Assert.AreEqual(destBefore, customer.Accounts[1].Balance);
        }

        // Test a staff member's failed transfer is charged only 50% of the failed-transaction fee
        [TestMethod]
        public void TransferFunds_StaffFailedTransfer_ChargesHalfFee()
        {
            User staff = controller.GetCustomerByNumber("C-2026-002")!;
            decimal investmentBefore = staff.Accounts[1].Balance;

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => controller.TransferFunds("C-2026-002", 1, 0, 999_999m));

            Assert.AreEqual(investmentBefore - (InvestmentAccount.FailedFee * 0.5m), staff.Accounts[1].Balance);
        }

        // Test deleting a customer
        [TestMethod]
        public void DeleteCustomer_ValidId_RemovesCustomer()
        {
            int countBefore = controller.GetCustomerCount();
            bool result = controller.DeleteCustomer("C-2026-001");

            Assert.IsTrue(result);
            Assert.AreEqual(countBefore - 1, controller.GetCustomerCount());
            Assert.IsNull(controller.GetCustomerByNumber("C-2026-001"));
        }
    }
}
