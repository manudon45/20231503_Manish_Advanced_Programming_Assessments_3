using System.Text.Json.Serialization;

namespace _20231503_ManishRay_Assignment3.Models
{
    // Polymorphic base: System.Text.Json writes a "$type" discriminator
    // ("everyday" | "investment" | "omni") so the correct subclass is rebuilt on load.
    [JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
    [JsonDerivedType(typeof(EverydayAccount), "everyday")]
    [JsonDerivedType(typeof(InvestmentAccount), "investment")]
    [JsonDerivedType(typeof(OmniAccount), "omni")]
    public abstract class Account
    {
        private static int _idSeed = 1000;

        public int AccountId { get; private set; }
        public string AccountName { get; protected set; }
        public decimal Balance { get; private set; }
        public string LastTransactionStatus { get; protected set; }

        // Used when a brand-new account is opened at runtime
        protected Account(string accountName, decimal initialBalance)
        {
            AccountId = _idSeed++;
            AccountName = accountName;
            Balance = initialBalance >= 0 ? initialBalance : 0;
            LastTransactionStatus = "No transactions yet.";
        }

        // Used by the JSON deserializer to restore a saved account as-is
        protected Account(int accountId, string accountName, decimal balance, string lastTransactionStatus)
        {
            AccountId = accountId;
            AccountName = accountName;
            Balance = balance;
            LastTransactionStatus = lastTransactionStatus;
            SyncIdSeed(accountId);
        }

        // Adjust balance safely through base method
        protected void AdjustBalance(decimal delta)
        {
            Balance += delta;
        }

        // Keep the id counter ahead of every id read back from disk (collision guard)
        public static void SyncIdSeed(int existingId)
        {
            if (existingId >= _idSeed)
            {
                _idSeed = existingId + 1;
            }
        }

        public abstract string Deposit(decimal amount, bool isStaff = false);
        public abstract string Withdraw(decimal amount, bool isStaff = false);

        public virtual string GetAccountInfo()
        {
            return $"[{AccountName}]  ID: {AccountId}  |  Balance: {Balance:C2}";
        }
    }
}
