using _20231503_ManishRay_Assignment3.Data;
using _20231503_ManishRay_Assignment3.Persistence;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    // Facade the UI talks to. Owns the shared BankRepository and the segregated controllers, and
    // exposes the whole-system load / save that Form1 wires to its Load / FormClosing events.
    public class BankController
    {
        private readonly BankRepository repository;

        public CustomerController Customers { get; }
        public AccountController Accounts { get; }
        public TransferController Transfers { get; }

        public BankController() : this(new JsonPersistenceService())
        {
        }

        public BankController(JsonPersistenceService persistenceService)
        {
            repository = new BankRepository(persistenceService);
            Customers = new CustomerController(repository);
            Accounts = new AccountController(repository);
            Transfers = new TransferController(repository);
        }

        // Absolute path of the JSON data store (used by the troubleshooting guide)
        public string StoragePath => repository.StoragePath;

        // Restores the whole system state from the JSON file on start-up.
        // Returns true when saved data was loaded from disk.
        public bool LoadData() => repository.Load();

        // Persists the whole system state to the JSON file.
        public void SaveData() => repository.Save();
    }
}
