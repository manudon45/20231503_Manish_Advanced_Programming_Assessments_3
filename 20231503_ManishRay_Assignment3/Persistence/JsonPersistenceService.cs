using System.Text.Json;
using System.Text.Json.Serialization;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Persistence
{
    // The JSON serializer layer. The Controller hands this service the in-memory model and it
    // converts it to / from the local bank_data.json file using System.Text.Json. Polymorphic
    // account and user types are preserved through the [JsonDerivedType] attributes on Account
    // and User.
    public class JsonPersistenceService
    {
        // Current on-disk schema. A file that does not match is treated as unreadable.
        public const string CurrentSchemaVersion = "1.0";

        private const string DefaultFileName = "bank_data.json";

        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        private readonly string _filePath;

        // filePath: optional explicit path. When omitted the store sits next to the executable
        // (AppContext.BaseDirectory) so it travels with the app and survives between sessions.
        public JsonPersistenceService(string? filePath = null)
        {
            _filePath = string.IsNullOrWhiteSpace(filePath)
                ? Path.Combine(AppContext.BaseDirectory, DefaultFileName)
                : filePath!;
        }

        // Absolute path of the JSON data store (shown in the troubleshooting guide)
        public string FilePath => _filePath;

        // True when a data file already exists on disk
        public bool StoreExists => File.Exists(_filePath);

        // Serializes the whole system state to the JSON file. Writes a temp file first and then
        // atomically renames it over the real file, so a crash mid-write cannot corrupt the store.
        public void Save(IEnumerable<User> customers)
        {
            var snapshot = new BankDataFile
            {
                SchemaVersion = CurrentSchemaVersion,
                SavedUtcTimestamp = DateTime.UtcNow,
                Customers = customers.ToList()
            };

            try
            {
                string? directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonSerializer.Serialize(snapshot, SerializerOptions);

                // Write to a sibling temp file first, then swap it in. A crash mid-write can only
                // ever damage the .tmp file, never the live store.
                string tempPath = _filePath + ".tmp";
                File.WriteAllText(tempPath, json);
                File.Move(tempPath, _filePath, overwrite: true);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
            {
                throw new PersistenceException("Unable to save the banking data file.", _filePath, ex);
            }
        }

        // Reads the JSON file back into a list of User objects with their concrete account types
        // restored. Returns null (so the caller falls back to seed data) when there is no file, the
        // file is empty, the schema version is unknown, or the JSON is corrupt - a corrupt file is
        // renamed to *.corrupt-* rather than deleted so no data is silently lost.
        public List<User>? Load()
        {
            if (!File.Exists(_filePath))
            {
                return null;
            }

            string json;
            try
            {
                json = File.ReadAllText(_filePath);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                throw new PersistenceException("Unable to read the banking data file.", _filePath, ex);
            }

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            try
            {
                BankDataFile? snapshot = JsonSerializer.Deserialize<BankDataFile>(json, SerializerOptions);

                if (snapshot == null || snapshot.Customers.Count == 0)
                {
                    return null;
                }

                if (!string.Equals(snapshot.SchemaVersion, CurrentSchemaVersion, StringComparison.Ordinal))
                {
                    QuarantineCorruptFile();
                    return null;
                }

                // Guard: keep the account id counter ahead of every restored id.
                foreach (User user in snapshot.Customers)
                {
                    foreach (Account account in user.Accounts)
                    {
                        Account.SyncIdSeed(account.AccountId);
                    }
                }

                return snapshot.Customers;
            }
            catch (JsonException)
            {
                QuarantineCorruptFile();
                return null;
            }
            catch (NotSupportedException)
            {
                // Unknown "$type" discriminator in the file.
                QuarantineCorruptFile();
                return null;
            }
        }

        private void QuarantineCorruptFile()
        {
            try
            {
                string quarantinePath = $"{_filePath}.corrupt-{DateTime.Now:yyyyMMdd-HHmmss}";
                File.Move(_filePath, quarantinePath, overwrite: true);
            }
            catch (IOException)
            {
                // If we cannot move it we simply leave it; the next Save() will overwrite it.
            }
        }
    }
}
