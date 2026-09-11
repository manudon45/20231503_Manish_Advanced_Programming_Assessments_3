namespace _20231503_ManishRay_Assignment3.Exceptions
{
    public class BankingException : Exception
    {
        public string AccountType { get; }

        public BankingException(string message, string accountType = "General Account") : base(message)
        {
            AccountType = accountType;
        }

        public BankingException(string message, string accountType, Exception? innerException)
            : base(message, innerException)
        {
            AccountType = accountType;
        }
    }
}
