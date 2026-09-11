using _20231503_ManishRay_Assignment3.Data;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    /// <summary>
    /// MVC controller for customer CRUD. Account operations live in <see cref="AccountController"/>
    /// and transfers in <see cref="TransferController"/>; all three share the one
    /// <see cref="BankRepository"/> passed in here.
    /// </summary>
    public class CustomerController
    {
        private readonly BankRepository repository;

        /// <summary>Creates the controller over the shared in-memory model + persistence layer.</summary>
        /// <param name="repository">The shared repository.</param>
        public CustomerController(BankRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>Returns the live list of every account holder in the system (customers and staff).</summary>
        /// <returns>The repository's <c>List&lt;User&gt;</c> - read it, do not replace it.</returns>
        public List<User> GetAllCustomers()
        {
            return repository.Customers;
        }

        /// <summary>Finds one account holder by customer number (trimmed, case-insensitive).</summary>
        /// <param name="customerNumber">The customer id, e.g. <c>"C-2026-001"</c>.</param>
        /// <returns>The matching <see cref="User"/>, or <c>null</c> when blank or not found.</returns>
        public User? GetCustomerByNumber(string customerNumber)
        {
            return repository.FindByNumber(customerNumber);
        }

        /// <summary>Gets the account holder at a list position (used by the management form's list box).</summary>
        /// <param name="index">Zero-based position in <see cref="GetAllCustomers"/>.</param>
        /// <returns>The <see cref="User"/> at that index, or <c>null</c> when out of range.</returns>
        public User? GetCustomerByIndex(int index)
        {
            return repository.FindByIndex(index);
        }

        /// <summary>
        /// Creates a new account holder and appends them to the model: generates the next
        /// <c>C-2026-NNN</c> id, then builds a <see cref="BankStaff"/> when <paramref name="isStaff"/>
        /// is set or a <see cref="Customer"/> otherwise, opening the three standard accounts with the
        /// supplied balances.
        /// </summary>
        /// <param name="name">Full name; must not be blank.</param>
        /// <param name="contactDetails">Free-text contact string (phone / email).</param>
        /// <param name="isStaff"><c>true</c> to create a Bank Staff holder (eligible for the 50% fee benefit).</param>
        /// <param name="staffId">Staff id; ignored when not staff, auto-filled when blank.</param>
        /// <param name="everydayBal">Opening balance of the Everyday account.</param>
        /// <param name="invRate">Interest rate of the Investment account (e.g. <c>0.05</c>).</param>
        /// <param name="invBal">Opening balance of the Investment account.</param>
        /// <param name="omniOverdraft">Overdraft limit of the Omni account.</param>
        /// <param name="omniBal">Opening balance of the Omni account.</param>
        /// <returns><c>true</c> when added; <c>false</c> when <paramref name="name"/> is blank.</returns>
        public bool AddCustomer(string name, string contactDetails, bool isStaff, string staffId, decimal everydayBal, decimal invRate, decimal invBal, decimal omniOverdraft, decimal omniBal)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            var customers = repository.Customers;
            string newId = $"C-2026-{customers.Count + 1:D3}";
            User newCustomer;

            if (isStaff)
            {
                string sId = string.IsNullOrWhiteSpace(staffId) ? "STF-00" + (customers.Count + 1) : staffId;
                newCustomer = new BankStaff(newId, name, contactDetails, sId, everydayBal, invRate, invBal, omniOverdraft, omniBal);
            }
            else
            {
                newCustomer = new Customer(newId, name, contactDetails, everydayBal, invRate, invBal, omniOverdraft, omniBal);
            }

            customers.Add(newCustomer);
            return true;
        }

        /// <summary>
        /// Updates an existing account holder's name and contact details in place; their accounts
        /// and customer number are unchanged.
        /// </summary>
        /// <param name="customerNumber">Id of the customer to update.</param>
        /// <param name="newName">Replacement name; must not be blank.</param>
        /// <param name="newContactDetails">Replacement contact string.</param>
        /// <returns><c>true</c> on success; <c>false</c> when not found or the new name is blank.</returns>
        public bool UpdateCustomer(string customerNumber, string newName, string newContactDetails)
        {
            User? cust = repository.FindByNumber(customerNumber);
            if (cust == null || string.IsNullOrWhiteSpace(newName))
            {
                return false;
            }

            cust.UpdateDetails(newName, newContactDetails);
            return true;
        }

        /// <summary>
        /// Deletes an account holder, but refuses to remove the last remaining customer so the model
        /// is never left empty.
        /// </summary>
        /// <param name="customerNumber">Id of the customer to delete.</param>
        /// <returns><c>true</c> when removed; <c>false</c> when not found or they are the only customer left.</returns>
        public bool DeleteCustomer(string customerNumber)
        {
            User? cust = repository.FindByNumber(customerNumber);
            if (cust == null || repository.Customers.Count <= 1)
            {
                return false;
            }

            return repository.Customers.Remove(cust);
        }

        /// <summary>Number of account holders currently in the model.</summary>
        /// <returns>The customer count (always at least 1).</returns>
        public int GetCustomerCount()
        {
            return repository.Customers.Count;
        }
    }
}
