using _20231503_ManishRay_Assignment3.Data;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    // Handles intra-account transfers between two accounts owned by the same customer.
    public class TransferController
    {
        private readonly BankRepository repository;

        public TransferController(BankRepository repository)
        {
            this.repository = repository;
        }

        // Move funds between two accounts belonging to the same customer. Reuses Account.Withdraw /
        // Account.Deposit so overdraft limits, balance checks and the staff-discounted failed fee
        // all apply; a failed withdrawal aborts the transfer before any money reaches the destination.
        public string TransferFunds(string customerNumber, int sourceIndex, int destinationIndex, decimal amount)
        {
            User? customer = repository.FindByNumber(customerNumber);
            if (customer == null)
            {
                throw new BankingException("Transfer failed: customer not found.", "Transfer");
            }

            if (sourceIndex < 0 || sourceIndex >= customer.Accounts.Count ||
                destinationIndex < 0 || destinationIndex >= customer.Accounts.Count)
            {
                throw new BankingException("Transfer failed: invalid account selection.", "Transfer");
            }

            if (sourceIndex == destinationIndex)
            {
                throw new BankingException("Transfer failed: source and destination must be different accounts.", "Transfer");
            }

            if (amount <= 0)
            {
                throw new BankingException("Transfer failed: amount must be a positive value.", "Transfer");
            }

            Account source = customer.Accounts[sourceIndex];
            Account destination = customer.Accounts[destinationIndex];
            bool isStaff = customer.IsStaff;

            source.Withdraw(amount, isStaff);
            destination.Deposit(amount, isStaff);

            return $"Transfer Successful: {amount:C2} moved from {source.AccountName} to {destination.AccountName}."
                 + $"  |  {source.AccountName}: {source.Balance:C2}  |  {destination.AccountName}: {destination.Balance:C2}";
        }
    }
}
