using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        private BankController bank = null!;
        private AccountController accounts = null!;

        [TestInitialize]
        public void Setup()
        {
            bank = new BankController();
            accounts = bank.Accounts;
        }

        [TestMethod]
        public void AddAccountToCustomer_ValidType_GrowsAccountList()
        {
            User customer = bank.Customers.GetCustomerByNumber("C-2026-001")!;
            int before = customer.Accounts.Count;

            bool result = accounts.AddAccountToCustomer("C-2026-001", "investment", 2500m, 0.07m);

            Assert.IsTrue(result);
            Assert.AreEqual(before + 1, customer.Accounts.Count);
            Assert.IsInstanceOfType<InvestmentAccount>(customer.Accounts[^1]);
        }

        [TestMethod]
        public void AddAccountToCustomer_UnknownType_ReturnsFalse()
        {
            Assert.IsFalse(accounts.AddAccountToCustomer("C-2026-001", "crypto", 100m, 0m));
        }

        [TestMethod]
        public void AddAccountToCustomer_UnknownCustomer_ReturnsFalse()
        {
            Assert.IsFalse(accounts.AddAccountToCustomer("C-9999", "everyday", 100m, 0m));
        }

        [TestMethod]
        public void RemoveAccountFromCustomer_KeepsAtLeastOneAccount()
        {
            User customer = bank.Customers.GetCustomerByNumber("C-2026-001")!;
            while (customer.Accounts.Count > 1)
            {
                Assert.IsTrue(accounts.RemoveAccountFromCustomer("C-2026-001", 0));
            }

            Assert.IsFalse(accounts.RemoveAccountFromCustomer("C-2026-001", 0));
            Assert.AreEqual(1, customer.Accounts.Count);
        }

        [TestMethod]
        public void GetAccountsForCustomer_UnknownCustomer_ReturnsEmptyList()
        {
            Assert.IsEmpty(accounts.GetAccountsForCustomer("C-9999"));
        }
    }
}
