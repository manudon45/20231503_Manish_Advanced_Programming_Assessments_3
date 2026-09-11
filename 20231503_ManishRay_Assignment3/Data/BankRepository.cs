using _20231503_ManishRay_Assignment3.Models;
using _20231503_ManishRay_Assignment3.Persistence;

namespace _20231503_ManishRay_Assignment3.Data
{
    /// <summary>
    /// The single shared data store. Every controller reads and writes this one instance, so the
    /// customer list and the JSON persistence layer are not duplicated. Also holds the two lookup
    /// helpers the controllers use.
    /// </summary>
    public class BankRepository
    {
        private readonly JsonPersistenceService persistence;

        /// <summary>The live in-memory model: every account holder (customers and staff).</summary>
        public List<User> Customers { get; private set; }

        /// <summary>Creates the repository with the default JSON store (a file beside the executable).</summary>
        public BankRepository() : this(new JsonPersistenceService())
        {
        }

        /// <summary>Creates the repository with an explicit persistence service (used by the tests).</summary>
        /// <param name="persistenceService">The JSON serializer layer to save to and load from.</param>
        public BankRepository(JsonPersistenceService persistenceService)
        {
            persistence = persistenceService;
            Customers = new List<User>();
            SeedDefaults();
        }

        /// <summary>Absolute path of the JSON data store on disk.</summary>
        public string StoragePath => persistence.FilePath;

        /// <summary>
        /// Replaces the model with the two demo account holders (one regular customer, one Bank
        /// Staff). Used before any file exists and as the fallback when a saved file cannot be read.
        /// </summary>
        public void SeedDefaults()
        {
            Customers.Clear();
            Customers.Add(new Customer("C-2026-001", "Manish Regular", "021-555-0123 | mr@mbk.nz"));
            Customers.Add(new BankStaff("C-2026-002", "Manish BankStaff", "021-555-0199 | mbs@mbk.nz", "STF-0042"));
        }

        /// <summary>
        /// Restores the model from the JSON file. Replaces <see cref="Customers"/> when a valid file
        /// is returned; keeps the seed data otherwise.
        /// </summary>
        /// <returns><c>true</c> when saved data replaced the seed data; <c>false</c> when the seed data is kept.</returns>
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

        /// <summary>Serializes the whole model (all customers and their polymorphic accounts) to the JSON file.</summary>
        public void Save()
        {
            persistence.Save(Customers);
        }

        /// <summary>Finds an account holder by customer number (trimmed, case-insensitive).</summary>
        /// <param name="customerNumber">The customer id to search for.</param>
        /// <returns>The matching <see cref="User"/>, or <c>null</c> when blank or not found.</returns>
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

        /// <summary>Gets the account holder at a list position.</summary>
        /// <param name="index">Zero-based index into <see cref="Customers"/>.</param>
        /// <returns>The <see cref="User"/> at that index, or <c>null</c> when out of range.</returns>
        public User? FindByIndex(int index)
        {
            return index >= 0 && index < Customers.Count ? Customers[index] : null;
        }
    }
}
