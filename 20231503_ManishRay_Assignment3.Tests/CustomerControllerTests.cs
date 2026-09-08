using _20231503_ManishRay_Assignment3.Controllers;
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
