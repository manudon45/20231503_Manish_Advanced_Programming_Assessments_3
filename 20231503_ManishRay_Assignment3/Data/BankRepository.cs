using _20231503_ManishRay_Assignment3.Models;
using _20231503_ManishRay_Assignment3.Persistence;

namespace _20231503_ManishRay_Assignment3.Data
{
    // Single shared data store. Every controller works against this one instance, so the customer
    // list and the JSON persistence layer are not duplicated across controllers.
    public class BankRepository
    {
        private readonly JsonPersistenceService persistence;

        public List<User> Customers { get; private set; }

        public BankRepository() : this(new JsonPersistenceService())
        {
        }

        public BankRepository(JsonPersistenceService persistenceService)
        {
            persistence = persistenceService;
            Customers = new List<User>();
            SeedDefaults();
        }

        // Absolute path of the JSON data store (used by the troubleshooting guide)
        public string StoragePath => persistence.FilePath;

        // Two demo account holders used before any file has been saved
        public void SeedDefaults()
        {
            Customers.Clear();
            Customers.Add(new Customer("C-2026-001", "Manish Regular", "021-555-0123 | mr@mbk.nz"));
            Customers.Add(new BankStaff("C-2026-002", "Manish BankStaff", "021-555-0199 | mbs@mbk.nz", "STF-0042"));
        }

        // Restores the whole system state from the JSON file. Returns true when saved data replaced
        // the seed data; false when the seed data is kept (no file / empty / corrupt).
        public bool Load()
        {
            List<User>? restored = persistence.Load();
            if (restored == null || restored.Count == 0)
            {
                return false;
            }

            Customers = restored;
            return true;
        }

        // Serializes the whole system state (all customers and their polymorphic accounts)
        public void Save()
        {
            persistence.Save(Customers);
        }

        public User? FindByNumber(string customerNumber)
        {
            if (string.IsNullOrWhiteSpace(customerNumber))
            {
                return null;
            }

            foreach (var cust in Customers)
            {
                if (string.Equals(cust.CustomerNumber?.Trim(), customerNumber.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return cust;
                }
            }
            return null;
        }

        public User? FindByIndex(int index)
        {
            return index >= 0 && index < Customers.Count ? Customers[index] : null;
        }
    }
}
