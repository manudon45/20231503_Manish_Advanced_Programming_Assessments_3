using _20231503_ManishRay_Assignment3.Data;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    // Owns the one-to-many side of the model: adding, removing and listing a customer's accounts.
    public class AccountController
    {
        private readonly BankRepository repository;

        public AccountController(BankRepository repository)
        {
            this.repository = repository;
        }

        // Add a new account to an existing customer at runtime (one-to-many expansion)
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

        // Remove one account from a customer (keeps a minimum of one account)
        public bool RemoveAccountFromCustomer(string customerNumber, int accountIndex)
        {
            User? cust = repository.FindByNumber(customerNumber);
            if (cust == null || accountIndex < 0 || accountIndex >= cust.Accounts.Count)
            {
                return false;
            }

            return cust.RemoveAccount(cust.Accounts[accountIndex]);
        }

        // Return the account list for a given customer, or an empty list if not found
        public List<Account> GetAccountsForCustomer(string customerNumber)
        {
            User? cust = repository.FindByNumber(customerNumber);
            return cust?.Accounts ?? new List<Account>();
        }
    }
}
