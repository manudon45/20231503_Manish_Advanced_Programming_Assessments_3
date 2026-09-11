using _20231503_ManishRay_Assignment3.Data;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    /// <summary>
    /// MVC controller for the one-to-many side of the model: adding, removing and listing the
    /// accounts a customer holds. Works against the shared <see cref="BankRepository"/>.
    /// </summary>
    public class AccountController
    {
        private readonly BankRepository repository;

        /// <summary>Creates the controller over the shared in-memory model + persistence layer.</summary>
        /// <param name="repository">The shared repository.</param>
        public AccountController(BankRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Opens a new account for an existing customer at runtime (proving the <c>List&lt;Account&gt;</c>
        /// is a true 1:N relationship). Switches on <paramref name="accountType"/> to build the right
        /// concrete <see cref="Account"/> subclass, then calls <c>User.AddAccount</c>.
        /// </summary>
        /// <param name="customerNumber">Id of the customer to add the account to.</param>
        /// <param name="accountType"><c>"everyday"</c>, <c>"investment"</c> or <c>"omni"</c> (case-insensitive).</param>
        /// <param name="initialBalance">Opening balance for the new account.</param>
        /// <param name="extraParameter">Interest rate (Investment) or overdraft limit (Omni); a default is used when zero; ignored for Everyday.</param>
        /// <returns><c>true</c> when created and attached; <c>false</c> when the customer is not found or the type is blank / unknown.</returns>
        public bool AddAccountToCustomer(string customerNumber, string accountType, decimal initialBalance, decimal extraParameter)
        {
            User? cust = repository.FindByNumber(customerNumber);
            if (cust == null || string.IsNullOrWhiteSpace(accountType))
            {
                return false;
            }

            Account newAccount;
            switch (accountType.Trim().ToLowerInvariant())
            {
                case "everyday":
                    newAccount = new EverydayAccount(initialBalance);
                    break;
                case "investment":
                    decimal rate = extraParameter > 0 ? extraParameter : 0.05m;
                    newAccount = new InvestmentAccount(rate, initialBalance);
                    break;
                case "omni":
                    decimal overdraft = extraParameter > 0 ? extraParameter : 500m;
                    newAccount = new OmniAccount(overdraft, initialBalance);
                    break;
                default:
                    return false;
            }

            cust.AddAccount(newAccount);
            return true;
        }

        /// <summary>
        /// Removes one account from a customer by list position. <c>User.RemoveAccount</c> keeps at
        /// least one account, so a call that would empty the list is rejected.
        /// </summary>
        /// <param name="customerNumber">Id of the customer.</param>
        /// <param name="accountIndex">Zero-based index of the account to remove.</param>
        /// <returns><c>true</c> when removed; <c>false</c> when not found, out of range, or it is the last account.</returns>
        public bool RemoveAccountFromCustomer(string customerNumber, int accountIndex)
        {
            User? cust = repository.FindByNumber(customerNumber);
            if (cust == null || accountIndex < 0 || accountIndex >= cust.Accounts.Count)
            {
                return false;
            }

            return cust.RemoveAccount(cust.Accounts[accountIndex]);
        }

        /// <summary>Returns a customer's account list, for populating combo boxes and the tab strip.</summary>
        /// <param name="customerNumber">Id of the customer.</param>
        /// <returns>The customer's <c>List&lt;Account&gt;</c>, or a new empty list when they are not found.</returns>
        public List<Account> GetAccountsForCustomer(string customerNumber)
        {
            User? cust = repository.FindByNumber(customerNumber);
            return cust?.Accounts ?? new List<Account>();
        }
    }
}
