using _20231503_ManishRay_Assignment3.Data;
using _20231503_ManishRay_Assignment3.Persistence;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    /// <summary>
    /// Top-level MVC controller the WinForms layer talks to. Owns the shared <see cref="BankRepository"/>
    /// and the three segregated sub-controllers, and exposes the whole-system load / save that
    /// <c>Form1</c> wires to its <c>Load</c> and <c>FormClosing</c> events.
    /// </summary>
    public class BankController
    {
        private readonly BankRepository repository;

        /// <summary>Customer CRUD (create, read, update, delete account holders).</summary>
        public CustomerController Customers { get; }

        /// <summary>Add, remove and list a customer's accounts (the one-to-many side).</summary>
        public AccountController Accounts { get; }

        /// <summary>Intra-account transfers between two accounts owned by the same customer.</summary>
        public TransferController Transfers { get; }

        /// <summary>Creates the controller with the default JSON store (a file beside the executable).</summary>
        public BankController() : this(new JsonPersistenceService())
        {
        }

        /// <summary>Creates the controller with an explicit persistence service (used by the tests).</summary>
        /// <param name="persistenceService">The JSON serializer layer the shared repository saves to and loads from.</param>
        public BankController(JsonPersistenceService persistenceService)
        {
            repository = new BankRepository(persistenceService);
            Customers = new CustomerController(repository);
            Accounts = new AccountController(repository);
            Transfers = new TransferController(repository);
        }

        /// <summary>Absolute path of the JSON data store on disk (shown in the user guide's troubleshooting section).</summary>
        public string StoragePath => repository.StoragePath;

        /// <summary>
        /// Restores the entire system state from the JSON file. Called from <c>Form1_Load</c> at
        /// start-up; delegates to <see cref="BankRepository.Load"/>.
        /// </summary>
        /// <returns><c>true</c> when a valid saved file was loaded; <c>false</c> when the seeded demo data is still in use.</returns>
        public bool LoadData() => repository.Load();

        /// <summary>
        /// Serializes the entire system state to the JSON file. Called from <c>Form1_FormClosing</c>
        /// so the next session opens where this one ended.
        /// </summary>
        public void SaveData() => repository.Save();
    }
}
