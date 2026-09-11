namespace _20231503_ManishRay_Assignment3.Exceptions
{
    // Raised when the JSON data store cannot be read or written. Carries the file path that failed
    // so the UI can show bank staff exactly where the problem is.
    public class PersistenceException : BankingException
    {
        public string FilePath { get; }

        public PersistenceException(string message, string filePath, Exception? inner = null)
            : base(message, "Data Persistence", inner)
        {
            FilePath = filePath;
        }
    }
}
