using _20231503_ManishRay_Assignment3.Data;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    /// <summary>
    /// MVC controller for Sprint 3's headline feature: intra-account transfers. Moves money between
    /// two accounts owned by the same customer (cross-customer transfers are out of scope). All the
    /// transfer logic lives here, not in the form.
    /// </summary>
    public class TransferController
    {
        private readonly BankRepository repository;

        /// <summary>Creates the controller over the shared in-memory model + persistence layer.</summary>
        /// <param name="repository">The shared repository.</param>
        public TransferController(BankRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Transfers <paramref name="amount"/> between two of a customer's accounts. Validates the
        /// customer, both account indices, that the two accounts differ and that the amount is
        /// positive, then calls <c>source.Withdraw</c> followed by <c>destination.Deposit</c> - so the
        /// balance / overdraft checks and the staff-discounted failed-fee all apply, and a failed
        /// withdrawal aborts the transfer before any money reaches the destination.
        /// </summary>
        /// <param name="customerNumber">Id of the customer who owns both accounts.</param>
        /// <param name="sourceIndex">Index of the account to take funds from.</param>
        /// <param name="destinationIndex">Index of the account to move funds into.</param>
        /// <param name="amount">Positive amount to move.</param>
        /// <returns>A success message with the amount moved and both resulting balances.</returns>
        /// <exception cref="BankingException">Validation failure: customer not found, invalid selection, same account, or non-positive amount.</exception>
        /// <exception cref="InsufficientFundsException">The source account cannot cover the withdrawal; any failed-transfer fee has already been charged to it.</exception>
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
                throw new BankingException("Transfer failed - Source and destination must be different accounts.", "Transfer");
            }

            if (amount <= 0)
            {
                throw new BankingException("Transfer failed - Amount must be positive.", "Transfer");
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
