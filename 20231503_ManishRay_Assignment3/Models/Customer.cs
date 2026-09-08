using System.Text.Json.Serialization;

namespace _20231503_ManishRay_Assignment3.Models
{
    public class Customer : User
    {
        [JsonIgnore]
        public override bool IsStaff
        {
            get { return false; }
        }

        // Convenience constructor: opens the three standard accounts for a new customer
        public Customer(
            string customerNumber,
            string name,
            string contactDetails,
            decimal everydayBalance = 500m,
            decimal investmentRate = 0.05m,
            decimal investmentBalance = 1000m,
            decimal omniOverdraft = 500m,
            decimal omniBalance = 750m
        ) : base(customerNumber, name, contactDetails)
        {
            Accounts = new List<Account>
            {
                new EverydayAccount(everydayBalance),
                new InvestmentAccount(investmentRate, investmentBalance),
                new OmniAccount(omniOverdraft, omniBalance)
            };
        }

        // Restore constructor used by System.Text.Json: rebuilds the customer with the
        // exact account list that was read back from the JSON store
        [JsonConstructor]
        public Customer(string customerNumber, string name, string contactDetails, List<Account> accounts)
            : base(customerNumber, name, contactDetails)
        {
            if (accounts is { Count: > 0 })
            {
                Accounts = accounts;
            }
        }

        public override string GetRoleLabel()
        {
            return "Regular Customer";
        }
    }
}
