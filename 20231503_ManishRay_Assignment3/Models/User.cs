namespace _20231503_ManishRay_Assignment3.Models
{
    public abstract class User
    {
        public string CustomerNumber { get; protected set; }
        public string Name { get; protected set; }
        public string ContactDetails { get; protected set; }
        public List<Account> Accounts { get; protected set; }

        public abstract bool IsStaff { get; }

        protected User(string customerNumber, string name, string contactDetails)
        {
            CustomerNumber = customerNumber;
            Name = name;
            ContactDetails = contactDetails;
            Accounts = new List<Account>();
        }

        public abstract string GetRoleLabel();

        public void UpdateDetails(string name, string contactDetails)
        {
            Name = name;
            ContactDetails = contactDetails;
        }

        public void AddAccount(Account account)
        {
            if (account != null)
            {
                Accounts.Add(account);
            }
        }

        public bool RemoveAccount(Account account)
        {
            if (account == null || Accounts.Count <= 1)
            {
                return false;
            }

            return Accounts.Remove(account);
        }
    }
}
