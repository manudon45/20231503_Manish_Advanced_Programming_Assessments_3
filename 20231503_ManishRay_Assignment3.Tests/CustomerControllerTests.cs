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
            controller = new BankController().Customers;
        }

        [TestMethod]
        public void GetAllCustomers_ReturnsSeededCustomers()
        {
            Assert.IsGreaterThanOrEqualTo(2, controller.GetAllCustomers().Count);
        }

        [TestMethod]
        public void AddCustomer_ValidInput_IncreasesCount()
        {
            int initialCount = controller.GetCustomerCount();
            bool result = controller.AddCustomer("Test Customer", "021-111-222", false, "", 500m, 0.05m, 1000m, 500m, 750m);

            Assert.IsTrue(result);
            Assert.AreEqual(initialCount + 1, controller.GetCustomerCount());
        }

        [TestMethod]
        public void AddCustomer_StaffRole_CreatesBankStaffWithBenefit()
        {
            controller.AddCustomer("Staff Member", "021-999", true, "STF-1234", 500m, 0.05m, 1000m, 500m, 750m);
            User? added = controller.GetCustomerByNumber("C-2026-003");

            Assert.IsInstanceOfType<BankStaff>(added);
            Assert.IsTrue(added.IsStaff);
        }

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

        [TestMethod]
        public void DeleteCustomer_ValidId_RemovesCustomer()
        {
            int countBefore = controller.GetCustomerCount();
            bool result = controller.DeleteCustomer("C-2026-001");

            Assert.IsTrue(result);
            Assert.AreEqual(countBefore - 1, controller.GetCustomerCount());
            Assert.IsNull(controller.GetCustomerByNumber("C-2026-001"));
        }

        [TestMethod]
        public void DeleteCustomer_LastRemaining_IsRejected()
        {
            controller.DeleteCustomer("C-2026-001");
            bool result = controller.DeleteCustomer("C-2026-002");

            Assert.IsFalse(result);
            Assert.AreEqual(1, controller.GetCustomerCount());
        }
    }
}
