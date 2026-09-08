namespace _20231503_ManishRay_Assignment3.Exceptions
{
    public class InsufficientFundsException : BankingException
    {
        public decimal CurrentBalance { get; }
        public decimal RequestedAmount { get; }

        public InsufficientFundsException(string message, string accountType, decimal currentBalance, decimal requestedAmount) 
            : base(message, accountType)
        {
            CurrentBalance = currentBalance;
            RequestedAmount = requestedAmount;
        }
    }
}
