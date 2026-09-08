using System.Text.Json.Serialization;
using _20231503_ManishRay_Assignment3.Exceptions;

namespace _20231503_ManishRay_Assignment3.Models
{
    public class OmniAccount : Account
    {
        public const decimal InterestRate = 0.04m;
        public const decimal InterestThreshold = 1000m;
        public const decimal FailedFee = 5.00m;

        public decimal OverdraftLimit { get; private set; }

        public OmniAccount(decimal overdraftLimit = 500m, decimal initialBalance = 750m) : base("Omni Account", initialBalance)
        {
            OverdraftLimit = overdraftLimit >= 0 ? overdraftLimit : 0;
        }

        // Restore constructor used by System.Text.Json when loading saved state
        [JsonConstructor]
        public OmniAccount(int accountId, string accountName, decimal balance, string lastTransactionStatus, decimal overdraftLimit)
            : base(accountId, accountName, balance, lastTransactionStatus)
        {
            OverdraftLimit = overdraftLimit >= 0 ? overdraftLimit : 0;
        }

        public override string Deposit(decimal amount, bool isStaff = false)
        {
            if (amount <= 0)
            {
                LastTransactionStatus = "Deposit Failed: Amount must be positive.";
                throw new BankingException("Deposit Failed: Amount must be positive.", AccountName);
            }

            AdjustBalance(amount);
            LastTransactionStatus = $"Deposit Successful: +{amount:C2}  |  Balance: {Balance:C2}";
            return LastTransactionStatus;
        }

        public override string Withdraw(decimal amount, bool isStaff = false)
        {
            if (amount <= 0)
            {
                LastTransactionStatus = "Withdrawal Failed: Amount must be positive.";
                throw new BankingException("Withdrawal Failed: Amount must be positive.", AccountName);
            }

            if (amount > Balance + OverdraftLimit)
            {
                decimal fee = isStaff ? FailedFee * 0.5m : FailedFee;
                AdjustBalance(-fee);
                LastTransactionStatus = $"Withdrawal Failed: Exceeds Overdraft Limit  |  Fee Charged: {fee:C2}  |  Balance: {Balance:C2}";
                string msg = $"Omni Account withdrawal failed: Requested amount {amount:C2} exceeds total limit of {(Balance + fee + OverdraftLimit):C2} (Overdraft: {OverdraftLimit:C2}). Failed fee of {fee:C2} charged.";
                throw new InsufficientFundsException(msg, AccountName, Balance, amount);
            }

            AdjustBalance(-amount);
            LastTransactionStatus = $"Withdrawal Successful: -{amount:C2}  |  Balance: {Balance:C2}";
            return LastTransactionStatus;
        }

        public string CalculateInterest()
        {
            if (Balance > InterestThreshold)
            {
                decimal interest = Balance * InterestRate;
                AdjustBalance(interest);
                LastTransactionStatus = $"Interest Applied: +{interest:C2} ({InterestRate:P1})  |  Balance: {Balance:C2}";
            }
            else
            {
                LastTransactionStatus = $"No Interest: Balance {Balance:C2} must exceed {InterestThreshold:C0}.";
            }

            return LastTransactionStatus;
        }

        public override string GetAccountInfo()
        {
            return base.GetAccountInfo() + $"  |  Rate: {InterestRate:P1} on >{InterestThreshold:C0}  |  Overdraft: {OverdraftLimit:C2}  |  Failed Fee: {FailedFee:C2}";
        }
    }
}
