using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    public class CustomerController
    {
        private List<User> customerList;

        public CustomerController()
        {
            customerList = new List<User>();
            // Add default sample users
            customerList.Add(new Customer("C-2026-001", "Manish Regular", "021-555-0123 | mr@mbk.nz"));
            customerList.Add(new BankStaff("C-2026-002", "Manish BankStaff", "021-555-0199 | mbs@mbk.nz", "STF-0042"));
        }

        // Returns all customers
        public List<User> GetAllCustomers()
        {
            return customerList;
        }

        // Find customer by customer ID string
        public User? GetCustomerByNumber(string customerNumber)
        {
            if (string.IsNullOrWhiteSpace(customerNumber))
            {
                return null;
            }

            foreach (var cust in customerList)
            {
                if (string.Equals(cust.CustomerNumber?.Trim(), customerNumber.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return cust;
                }
            }
            return null;
        }

        // Get customer by list index
        public User? GetCustomerByIndex(int index)
        {
            if (index >= 0 && index < customerList.Count)
            {
                return customerList[index];
            }
            return null;
        }

        // Add a new customer to list
        public bool AddCustomer(string name, string contactDetails, bool isStaff, string staffId, decimal everydayBal, decimal invRate, decimal invBal, decimal omniOverdraft, decimal omniBal)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            string newId = $"C-2026-{customerList.Count + 1:D3}";
            User newCustomer;

            if (isStaff)
            {
                string sId = string.IsNullOrWhiteSpace(staffId) ? "STF-00" + (customerList.Count + 1) : staffId;
                newCustomer = new BankStaff(newId, name, contactDetails, sId, everydayBal, invRate, invBal, omniOverdraft, omniBal);
            }
            else
            {
                newCustomer = new Customer(newId, name, contactDetails, everydayBal, invRate, invBal, omniOverdraft, omniBal);
            }

            customerList.Add(newCustomer);
            return true;
        }

        // Add a new account to an existing customer at runtime (One-to-Many expansion)
        public bool AddAccountToCustomer(string customerNumber, string accountType, decimal initialBalance, decimal extraParameter)
        {
            User? cust = GetCustomerByNumber(customerNumber);
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
            User? cust = GetCustomerByNumber(customerNumber);
            if (cust == null || accountIndex < 0 || accountIndex >= cust.Accounts.Count)
            {
                return false;
            }

            return cust.RemoveAccount(cust.Accounts[accountIndex]);
        }

        // Return the account list for a given customer, or an empty list if not found
        public List<Account> GetAccountsForCustomer(string customerNumber)
        {
            User? cust = GetCustomerByNumber(customerNumber);
            return cust?.Accounts ?? new List<Account>();
        }

        // Intra-account transfer between two accounts belonging to the same customer
        public string TransferFunds(string customerNumber, int sourceIndex, int destinationIndex, decimal amount)
        {
            User? customer = GetCustomerByNumber(customerNumber);
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

        // Update existing customer details
        public bool UpdateCustomer(string customerNumber, string newName, string newContactDetails)
        {
            User? cust = GetCustomerByNumber(customerNumber);
            if (cust == null || string.IsNullOrWhiteSpace(newName))
            {
                return false;
            }

            cust.UpdateDetails(newName, newContactDetails);
            return true;
        }

        // Delete a customer by ID
        public bool DeleteCustomer(string customerNumber)
        {
            User? cust = GetCustomerByNumber(customerNumber);
            if (cust == null || customerList.Count <= 1)
            {
                return false;
            }

            return customerList.Remove(cust);
        }

        // Returns count of customers
        public int GetCustomerCount()
        {
            return customerList.Count;
        }
    }
}
