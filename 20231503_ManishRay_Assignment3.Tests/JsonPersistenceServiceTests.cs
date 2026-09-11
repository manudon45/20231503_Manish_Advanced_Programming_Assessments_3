using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;
using _20231503_ManishRay_Assignment3.Persistence;

namespace _20231503_ManishRay_Assignment3.Tests
{
    // Task 5 module tests: prove the system state round-trips through the JSON store with the
    // polymorphic account / user types intact and that a damaged file does not corrupt a session.
    [TestClass]
    public class JsonPersistenceServiceTests
    {
        private string tempDir = null!;
        private string tempFile = null!;

        [TestInitialize]
        public void Setup()
        {
            tempDir = Path.Combine(Path.GetTempPath(), $"bank_data_test_{Guid.NewGuid():N}");
            Directory.CreateDirectory(tempDir);
            tempFile = Path.Combine(tempDir, "bank_data.json");
        }

        [TestCleanup]
        public void Cleanup()
        {
            try { Directory.Delete(tempDir, recursive: true); } catch (IOException) { }
        }

        [TestMethod]
        public void Save_WritesIndentedJsonFileWithTypeDiscriminators()
        {
            var service = new JsonPersistenceService(tempFile);
            service.Save(new List<User> { new Customer("C-1", "Ada", "ada@bank.nz") });

            string json = File.ReadAllText(tempFile);

            Assert.IsTrue(File.Exists(tempFile));
            StringAssert.Contains(json, "\"$type\": \"customer\"");
            StringAssert.Contains(json, "\"$type\": \"everyday\"");
            StringAssert.Contains(json, "\"$type\": \"investment\"");
            StringAssert.Contains(json, "\"$type\": \"omni\"");
        }

        [TestMethod]
        public void SaveThenLoad_RestoresConcreteAccountTypesAndBalances()
        {
            var original = new Customer("C-7", "Grace", "grace@bank.nz");
            original.Accounts[0].Deposit(250m);          // Everyday 500 -> 750
            original.Accounts[2].Withdraw(1000m);        // Omni 750 -> -250 (into overdraft)

            var service = new JsonPersistenceService(tempFile);
            service.Save(new List<User> { original });

            List<User>? loaded = service.Load();

            Assert.IsNotNull(loaded);
            Assert.HasCount(1, loaded);

            User restored = loaded[0];
            Assert.IsInstanceOfType<Customer>(restored);
            Assert.AreEqual("C-7", restored.CustomerNumber);
            Assert.IsFalse(restored.IsStaff);

            Assert.IsInstanceOfType<EverydayAccount>(restored.Accounts[0]);
            Assert.IsInstanceOfType<InvestmentAccount>(restored.Accounts[1]);
            Assert.IsInstanceOfType<OmniAccount>(restored.Accounts[2]);

            Assert.AreEqual(750m, restored.Accounts[0].Balance);
            Assert.AreEqual(-250m, restored.Accounts[2].Balance);
            Assert.AreEqual(original.Accounts[0].AccountId, restored.Accounts[0].AccountId);
        }

        [TestMethod]
        public void SaveThenLoad_PreservesStaffRoleAndTypeSpecificFields()
        {
            var staff = new BankStaff("C-9", "Linus", "linus@bank.nz", "STF-9001");
            decimal rate = ((InvestmentAccount)staff.Accounts[1]).InterestRate;
            decimal overdraft = ((OmniAccount)staff.Accounts[2]).OverdraftLimit;

            var service = new JsonPersistenceService(tempFile);
            service.Save(new List<User> { staff });
            User restored = service.Load()![0];

            Assert.IsInstanceOfType<BankStaff>(restored);
            Assert.IsTrue(restored.IsStaff);
            Assert.AreEqual("STF-9001", ((BankStaff)restored).StaffId);
            Assert.AreEqual(rate, ((InvestmentAccount)restored.Accounts[1]).InterestRate);
            Assert.AreEqual(overdraft, ((OmniAccount)restored.Accounts[2]).OverdraftLimit);
        }

        [TestMethod]
        public void RestoredAccount_StillEnforcesDomainRulesAfterLoad()
        {
            var staff = new BankStaff("C-3", "Edsger", "e@bank.nz", "STF-3");
            var service = new JsonPersistenceService(tempFile);
            service.Save(new List<User> { staff });

            User restored = service.Load()![0];
            var investment = (InvestmentAccount)restored.Accounts[1];
            decimal before = investment.Balance;

            // Overdrawn investment withdrawal still charges the staff-discounted failed fee.
            Assert.ThrowsExactly<InsufficientFundsException>(() => investment.Withdraw(9_999_999m, isStaff: true));
            Assert.AreEqual(before - (InvestmentAccount.FailedFee * 0.5m), investment.Balance);
        }

        [TestMethod]
        public void Load_NoFile_ReturnsNull()
        {
            var service = new JsonPersistenceService(tempFile);
            Assert.IsNull(service.Load());
        }

        [TestMethod]
        public void Load_CorruptFile_ReturnsNullAndQuarantinesFile()
        {
            File.WriteAllText(tempFile, "{ this is not valid json ]");
            var service = new JsonPersistenceService(tempFile);

            Assert.IsNull(service.Load());
            Assert.IsFalse(File.Exists(tempFile), "corrupt file should be moved aside");
            Assert.IsNotEmpty(Directory.GetFiles(tempDir, "bank_data.json.corrupt-*"));
        }

        [TestMethod]
        public void Load_UnknownSchemaVersion_IsRejectedAndQuarantined()
        {
            // A populated file whose only problem is an unrecognised schema version.
            new JsonPersistenceService(tempFile).Save(new List<User> { new Customer("C-1", "Ada", "a@bank.nz") });
            string good = File.ReadAllText(tempFile);
            File.WriteAllText(tempFile, good.Replace("\"SchemaVersion\": \"1.0\"", "\"SchemaVersion\": \"9.9\""));

            var service = new JsonPersistenceService(tempFile);

            Assert.IsNull(service.Load());
            Assert.IsFalse(File.Exists(tempFile));
            Assert.IsNotEmpty(Directory.GetFiles(tempDir, "bank_data.json.corrupt-*"));
        }

        [TestMethod]
        public void BankController_SaveThenLoad_RoundTripsRuntimeChanges()
        {
            var writer = new BankController(new JsonPersistenceService(tempFile));
            writer.Customers.AddCustomer("Katherine Johnson", "kj@bank.nz", false, "", 400m, 0.05m, 900m, 300m, 600m);
            writer.Accounts.AddAccountToCustomer("C-2026-003", "everyday", 123.45m, 0m);
            writer.SaveData();

            var reader = new BankController(new JsonPersistenceService(tempFile));
            Assert.IsTrue(reader.LoadData());

            User? katherine = reader.Customers.GetCustomerByNumber("C-2026-003");
            Assert.IsNotNull(katherine);
            Assert.HasCount(4, katherine.Accounts);
            Assert.AreEqual(123.45m, katherine.Accounts[3].Balance);
        }

        // T1-S11: transferred balances are still correct after "restarting" the application.
        [TestMethod]
        public void T1_S11_TransferredBalancesSurviveARestart()
        {
            var session1 = new BankController(new JsonPersistenceService(tempFile));
            session1.Transfers.TransferFunds("C-2026-001", 0, 1, 250m); // Everyday -> Investment
            session1.SaveData();                                        // FormClosing

            var session2 = new BankController(new JsonPersistenceService(tempFile));
            Assert.IsTrue(session2.LoadData());                         // Form_Load

            User customer = session2.Customers.GetCustomerByNumber("C-2026-001")!;
            Assert.AreEqual(250m, customer.Accounts[0].Balance);        // Everyday 500 - 250
            Assert.AreEqual(1250m, customer.Accounts[1].Balance);       // Investment 1000 + 250
            Assert.AreEqual(500m, ((OmniAccount)customer.Accounts[2]).OverdraftLimit);
        }
    }
}
