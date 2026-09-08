namespace _20231503_ManishRay_Assignment3.Models
{
    public class Customer : User
    {
        public override bool IsStaff
        {
            get { return false; }
        }

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

        public override string GetRoleLabel()
        {
            return "Regular Customer";
        }
    }
}
