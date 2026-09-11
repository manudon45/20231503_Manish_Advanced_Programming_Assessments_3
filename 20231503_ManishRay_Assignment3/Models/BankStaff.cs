using System.Text.Json.Serialization;

namespace _20231503_ManishRay_Assignment3.Models
{
    public class BankStaff : User
    {
        public string StaffId { get; private set; }

        [JsonIgnore]
        public override bool IsStaff
        {
            get { return true; }
        }

        // Convenience constructor: opens the three standard staff accounts
        public BankStaff(
            string customerNumber,
            string name,
            string contactDetails,
            string staffId,
            decimal everydayBalance = 1200m,
            decimal investmentRate = 0.06m,
            decimal investmentBalance = 5000m,
            decimal omniOverdraft = 1000m,
            decimal omniBalance = 2500m
        ) : base(customerNumber, name, contactDetails)
        {
            StaffId = staffId;
            Accounts = new List<Account>
            {
                new EverydayAccount(everydayBalance),
                new InvestmentAccount(investmentRate, investmentBalance),
                new OmniAccount(omniOverdraft, omniBalance)
            };
        }

        // Restore constructor used by System.Text.Json: rebuilds the staff member with the
        // exact staff id and account list that were read back from the JSON store
        [JsonConstructor]
        public BankStaff(string customerNumber, string name, string contactDetails, string staffId, List<Account> accounts)
            : base(customerNumber, name, contactDetails)
        {
            StaffId = staffId;
            if (accounts is { Count: > 0 })
            {
                Accounts = accounts;
            }
        }

        public override string GetRoleLabel()
        {
            return "Bank Staff";
        }
    }
}
