namespace _20231503_ManishRay_Assignment3.Models
{
    public abstract class Account
    {
        private static int _idSeed = 1000;

        public int AccountId { get; private set; }
        public string AccountName { get; protected set; }
        private decimal balance;
        public string LastTransactionStatus { get; protected set; }

        public decimal Balance
        {
            get { return balance; }
        }

        protected Account(string accountName, decimal initialBalance)
        {
            AccountId = _idSeed++;
            AccountName = accountName;
            balance = initialBalance >= 0 ? initialBalance : 0;
            LastTransactionStatus = "No transactions yet.";
        }

        // Adjust balance safely through base method
        protected void AdjustBalance(decimal delta)
        {
            balance += delta;
        }

        public abstract string Deposit(decimal amount, bool isStaff = false);
        public abstract string Withdraw(decimal amount, bool isStaff = false);

        public virtual string GetAccountInfo()
        {
            return $"[{AccountName}]  ID: {AccountId}  |  Balance: {balance:C2}";
        }
    }
}
