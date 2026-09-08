using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Persistence
{
    // Root object written to bank_data.json. The schema version + save timestamp make the file
    // self-describing and let the loader reject a file from an incompatible schema.
    public class BankDataFile
    {
        public string SchemaVersion { get; set; } = "1.0";

        public DateTime SavedUtcTimestamp { get; set; }

        // Every account holder in the system. Serialized polymorphically (Customer / BankStaff).
        public List<User> Customers { get; set; } = new();
    }
}
