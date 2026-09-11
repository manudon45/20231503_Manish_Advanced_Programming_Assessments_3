using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Tests
{
    // Task 6 evidence: these tests follow the Task 1 Gherkin scenarios one-for-one.
    // Account indices follow the constructor order: 0 = Everyday, 1 = Investment, 2 = Omni.
    [TestClass]
    public class TransferControllerTests
    {
        private BankController bank = null!;
        private TransferController transfers = null!;

        private const int Everyday = 0, Investment = 1, Omni = 2;

        [TestInitialize]
        public void Setup()
        {
            bank = new BankController();
            transfers = bank.Transfers;
        }

        // Adds a regular customer with the exact opening balances a scenario needs and returns their id.
        private string AddRegular(decimal everyday, decimal investment, decimal omniBalance, decimal omniOverdraft = 500m)
        {
            bank.Customers.AddCustomer("Scenario Customer", "sc@bank.nz", false, "",
                everyday, 0.05m, investment, omniOverdraft, omniBalance);
            return bank.Customers.GetAllCustomers()[^1].CustomerNumber;
        }

        private List<Account> AccountsOf(string customerNumber) =>
            bank.Customers.GetCustomerByNumber(customerNumber)!.Accounts;

        // ---- T1-SO1: successful transfers between a customer's own accounts --------------------
        [TestMethod]
        [DataRow(Everyday, 500, Investment, 1000, 200, 300, 1200)]
        [DataRow(Investment, 1000, Omni, 750, 400, 600, 1150)]
        [DataRow(Omni, 750, Everyday, 500, 300, 450, 800)]
        [DataRow(Everyday, 500, Omni, 750, 500, 0, 1250)]
        [DataRow(Investment, 1000, Everyday, 500, 1000, 0, 1500)]
        public void T1_SO1_SuccessfulTransfer(int src, int srcOpen, int dst, int dstOpen,
            int amount, int srcClose, int dstClose)
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m);
            var acc = AccountsOf(id);

            string result = transfers.TransferFunds(id, src, dst, amount);

            Assert.AreEqual((decimal)srcClose, acc[src].Balance);
            Assert.AreEqual((decimal)dstClose, acc[dst].Balance);
            StringAssert.Contains(result, "Transfer Successful");
        }

        // ---- T1-S2 / T1-S3: transfers that use the Omni overdraft ------------------------------
        [TestMethod]
        public void T1_S2_TransferUsingOmniOverdraft()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m, omniOverdraft: 500m);
            var acc = AccountsOf(id);

            transfers.TransferFunds(id, Omni, Everyday, 1000m);

            Assert.AreEqual(-250m, acc[Omni].Balance);
            Assert.AreEqual(1500m, acc[Everyday].Balance);
        }

        [TestMethod]
        public void T1_S3_TransferExactlyTheAvailableLimit()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m, omniOverdraft: 500m);
            var acc = AccountsOf(id);

            transfers.TransferFunds(id, Omni, Investment, 1250m);

            Assert.AreEqual(-500m, acc[Omni].Balance);
            Assert.AreEqual(2250m, acc[Investment].Balance);
        }

        // ---- T1-SO4: transfer fails when the source cannot cover it ---------------------------
        [TestMethod]
        public void T1_SO4a_EverydayInsufficientFunds_NoFee_NothingMoves()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m);
            var acc = AccountsOf(id);

            var ex = Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds(id, Everyday, Investment, 600m));

            StringAssert.Contains(ex.Message, "Insufficient Funds");
            Assert.AreEqual(500m, acc[Everyday].Balance);   // no fee on Everyday
            Assert.AreEqual(1000m, acc[Investment].Balance); // destination untouched
        }

        [TestMethod]
        public void T1_SO4b_InvestmentInsufficientFunds_Charges250()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m);
            var acc = AccountsOf(id);

            var ex = Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds(id, Investment, Omni, 1500m));

            StringAssert.Contains(ex.Message, "Insufficient Funds");
            Assert.AreEqual(1000m - 2.50m, acc[Investment].Balance);
            Assert.AreEqual(750m, acc[Omni].Balance);
        }

        [TestMethod]
        public void T1_SO4c_OmniExceedsOverdraft_Charges500()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m, omniOverdraft: 500m);
            var acc = AccountsOf(id);

            var ex = Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds(id, Omni, Everyday, 1300m));

            StringAssert.Contains(ex.Message, "Exceeds Overdraft Limit");
            Assert.AreEqual(750m - 5.00m, acc[Omni].Balance);
            Assert.AreEqual(500m, acc[Everyday].Balance);
        }

        // ---- T1-S5: one cent over the overdraft limit -----------------------------------------
        [TestMethod]
        public void T1_S5_OneCentOverTheOverdraftLimit_Fails()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m, omniOverdraft: 500m);
            var acc = AccountsOf(id);

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds(id, Omni, Everyday, 1250.01m));

            Assert.AreEqual(750m - 5.00m, acc[Omni].Balance);
            Assert.AreEqual(500m, acc[Everyday].Balance);
        }

        // ---- T1-SO6: transfer fails when the amount is not positive --------------------------
        [TestMethod]
        [DataRow(0.0)]
        [DataRow(-50.0)]
        public void T1_SO6_NonPositiveAmount_Fails(double amount)
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m);
            var acc = AccountsOf(id);

            var ex = Assert.ThrowsExactly<BankingException>(
                () => transfers.TransferFunds(id, Everyday, Investment, (decimal)amount));

            StringAssert.Contains(ex.Message, "Amount must be positive");
            Assert.AreEqual(500m, acc[Everyday].Balance);
            Assert.AreEqual(1000m, acc[Investment].Balance);
        }

        // ---- T1-S7: source and destination are the same account -----------------------------
        [TestMethod]
        public void T1_S7_SameSourceAndDestination_Fails()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m);

            var ex = Assert.ThrowsExactly<BankingException>(
                () => transfers.TransferFunds(id, Everyday, Everyday, 100m));

            StringAssert.Contains(ex.Message, "Source and destination must be different accounts");
            Assert.AreEqual(500m, AccountsOf(id)[Everyday].Balance);
        }

        // ---- T1-S8: transfers only touch the selected customer's own accounts ---------------
        [TestMethod]
        public void T1_S8_TransferIsScopedToTheSelectedCustomer()
        {
            User other = bank.Customers.GetCustomerByNumber("C-2026-002")!;
            decimal[] otherBefore = other.Accounts.Select(a => a.Balance).ToArray();

            transfers.TransferFunds("C-2026-001", Everyday, Investment, 100m);

            CollectionAssert.AreEqual(otherBefore, other.Accounts.Select(a => a.Balance).ToArray());
            // the account list offered for a customer is only ever their own
            Assert.AreNotSame(bank.Accounts.GetAccountsForCustomer("C-2026-001"),
                              bank.Accounts.GetAccountsForCustomer("C-2026-002"));
        }

        // ---- T1-SO9: failed-transfer fee is halved for a Bank Staff account holder ----------
        [TestMethod]
        public void T1_SO9_RegularCustomer_InvestmentFailedFee_IsFull250()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m);
            decimal before = AccountsOf(id)[Investment].Balance;

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds(id, Investment, Everyday, 1500m));

            Assert.AreEqual(before - 2.50m, AccountsOf(id)[Investment].Balance);
        }

        [TestMethod]
        public void T1_SO9_BankStaff_InvestmentFailedFee_IsHalved125()
        {
            User staff = bank.Customers.GetCustomerByNumber("C-2026-002")!;  // Investment opens at 5000
            decimal before = staff.Accounts[Investment].Balance;

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds("C-2026-002", Investment, Everyday, 6000m));

            Assert.AreEqual(before - 1.25m, staff.Accounts[Investment].Balance);
        }

        [TestMethod]
        public void T1_SO9_RegularCustomer_OmniFailedFee_IsFull500()
        {
            string id = AddRegular(everyday: 500m, investment: 1000m, omniBalance: 750m, omniOverdraft: 500m);
            decimal before = AccountsOf(id)[Omni].Balance;

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds(id, Omni, Everyday, 1300m));

            Assert.AreEqual(before - 5.00m, AccountsOf(id)[Omni].Balance);
        }

        [TestMethod]
        public void T1_SO9_BankStaff_OmniFailedFee_IsHalved250()
        {
            User staff = bank.Customers.GetCustomerByNumber("C-2026-002")!;  // Omni: 2500 balance + 1000 overdraft
            decimal before = staff.Accounts[Omni].Balance;

            Assert.ThrowsExactly<InsufficientFundsException>(
                () => transfers.TransferFunds("C-2026-002", Omni, Everyday, 4000m));

            Assert.AreEqual(before - 2.50m, staff.Accounts[Omni].Balance);
        }

        // ---- T1-S10: staff pays no fee when the transfer succeeds ---------------------------
        [TestMethod]
        public void T1_S10_StaffSuccessfulTransfer_ChargesNoFee()
        {
            User staff = bank.Customers.GetCustomerByNumber("C-2026-002")!;

            transfers.TransferFunds("C-2026-002", Investment, Omni, 1000m);

            Assert.AreEqual(4000m, staff.Accounts[Investment].Balance);
            Assert.AreEqual(3500m, staff.Accounts[Omni].Balance);
        }

        // ---- supporting: unknown customer -------------------------------------------------
        [TestMethod]
        public void TransferFunds_UnknownCustomer_Throws()
        {
            Assert.ThrowsExactly<BankingException>(() => transfers.TransferFunds("C-9999", 0, 1, 10m));
        }
    }
}
