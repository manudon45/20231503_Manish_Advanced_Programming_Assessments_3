using _20231503_ManishRay_Assignment3.Data;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3.Controllers
{
    // Customer CRUD only. Account operations live in AccountController; transfers in TransferController.
    public class CustomerController
    {
        private readonly BankRepository repository;

        public CustomerController(BankRepository repository)
        {
            this.repository = repository;
        }

        // Returns all customers
        public List<User> GetAllCustomers()
        {
            return repository.Customers;
        }

        // Find customer by customer ID string
        public User? GetCustomerByNumber(string customerNumber)
        {
            return repository.FindByNumber(customerNumber);
        }

        // Get customer by list index
        public User? GetCustomerByIndex(int index)
        {
            return repository.FindByIndex(index);
        }

        // Add a new customer to the list
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

        // Update existing customer details
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

        // Delete a customer by ID (always keeps at least one customer)
        public bool DeleteCustomer(string customerNumber)
        {
            User? cust = repository.FindByNumber(customerNumber);
            if (cust == null || repository.Customers.Count <= 1)
            {
                return false;
            }

            return repository.Customers.Remove(cust);
        }

        // Returns count of customers
        public int GetCustomerCount()
        {
            return repository.Customers.Count;
        }
    }
}
