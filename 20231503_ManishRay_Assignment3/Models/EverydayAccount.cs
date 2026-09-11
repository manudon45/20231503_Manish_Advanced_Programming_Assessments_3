using System.Text.Json.Serialization;
using _20231503_ManishRay_Assignment3.Exceptions;

namespace _20231503_ManishRay_Assignment3.Models
{
    public class EverydayAccount : Account
    {
        public EverydayAccount(decimal initialBalance = 500m) : base("Everyday Account", initialBalance)
        {
        }

        // Restore constructor used by System.Text.Json when loading saved state
        [JsonConstructor]
        public EverydayAccount(int accountId, string accountName, decimal balance, string lastTransactionStatus)
            : base(accountId, accountName, balance, lastTransactionStatus)
        {
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

            if (amount > Balance)
            {
                LastTransactionStatus = $"Withdrawal Failed: Insufficient Funds  |  Balance: {Balance:C2}";
                string msg = $"Everyday Account withdrawal failed - Insufficient Funds. Requested {amount:C2} exceeds the available balance of {Balance:C2}, and Everyday accounts have no overdraft.";
                throw new InsufficientFundsException(msg, AccountName, Balance, amount);
            }

            AdjustBalance(-amount);
            LastTransactionStatus = $"Withdrawal Successful: -{amount:C2}  |  Balance: {Balance:C2}";
            return LastTransactionStatus;
        }

        public override string GetAccountInfo()
        {
            return base.GetAccountInfo() + "  |  No Interest  |  No Overdraft  |  No Fees";
        }
    }
}
