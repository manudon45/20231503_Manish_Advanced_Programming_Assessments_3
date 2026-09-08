namespace _20231503_ManishRay_Assignment3.Models
{
    public class BankStaff : User
    {
        public string StaffId { get; private set; }

        public override bool IsStaff
        {
            get { return true; }
        }

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

        public override string GetRoleLabel()
        {
            return "Bank Staff";
        }
    }
}
