using System.Text.Json.Serialization;
using _20231503_ManishRay_Assignment3.Exceptions;

namespace _20231503_ManishRay_Assignment3.Models
{
    public class InvestmentAccount : Account
    {
        private decimal interestRate;
        public const decimal FailedFee = 2.50m;

        public decimal InterestRate
        {
            get { return interestRate; }
            set
            {
                if (value >= 0)
                    interestRate = value;
                else
                    interestRate = 0;
            }
        }

        public InvestmentAccount(decimal interestRate = 0.05m, decimal initialBalance = 1000m) : base("Investment Account", initialBalance)
        {
            InterestRate = interestRate;
        }

        // Restore constructor used by System.Text.Json when loading saved state
        [JsonConstructor]
        public InvestmentAccount(int accountId, string accountName, decimal balance, string lastTransactionStatus, decimal interestRate)
            : base(accountId, accountName, balance, lastTransactionStatus)
        {
            InterestRate = interestRate;
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
                decimal balanceBeforeFee = Balance;
                decimal fee = isStaff ? FailedFee * 0.5m : FailedFee;
                AdjustBalance(-fee);
                LastTransactionStatus = $"Withdrawal Failed: Insufficient Funds  |  Fee Charged: {fee:C2}  |  Balance: {Balance:C2}";
                string msg = $"Investment Account withdrawal failed - Insufficient Funds. Requested {amount:C2} exceeds the balance of {balanceBeforeFee:C2}. "
                           + $"A failed transaction fee of {fee:C2} was charged.";
                throw new InsufficientFundsException(msg, AccountName, Balance, amount);
            }

            AdjustBalance(-amount);
            LastTransactionStatus = $"Withdrawal Successful: -{amount:C2}  |  Balance: {Balance:C2}";
            return LastTransactionStatus;
        }

        public string CalculateInterest()
        {
            decimal interest = Balance * interestRate;
            AdjustBalance(interest);
            LastTransactionStatus = $"Interest Applied: +{interest:C2} ({interestRate:P1})  |  Balance: {Balance:C2}";
            return LastTransactionStatus;
        }

        public override string GetAccountInfo()
        {
            return base.GetAccountInfo() + $"  |  Rate: {interestRate:P1}  |  No Overdraft  |  Failed Fee: {FailedFee:C2}";
        }
    }
}
