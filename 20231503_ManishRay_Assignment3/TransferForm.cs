using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3
{
    // Modal dialog for an intra-account transfer: pick a customer, then a Source and a Destination
    // account from that customer's own profile. The layout lives in TransferForm.Designer.cs.
    public partial class TransferForm : BaseForm
    {
        private readonly CustomerController customersController;
        private readonly TransferController transfersController;
        private User customer = null!;

        public bool TransferPerformed { get; private set; }

        public TransferForm(BankController bank, User? initialCustomer = null)
        {
            InitializeComponent();
            customersController = bank.Customers;
            transfersController = bank.Transfers;
            PopulateCustomers(initialCustomer);
        }

        private void PopulateCustomers(User? initialCustomer)
        {
            cmbCustomer.SelectedIndexChanged -= cmbCustomer_SelectedIndexChanged;
            cmbCustomer.Items.Clear();

            var customers = customersController.GetAllCustomers();
            foreach (var c in customers)
            {
                cmbCustomer.Items.Add($"{c.Name}  ({c.GetRoleLabel()})  -  #{c.CustomerNumber}");
            }

            int startIndex = 0;
            if (initialCustomer != null)
            {
                int found = customers.IndexOf(initialCustomer);
                if (found >= 0)
                {
                    startIndex = found;
                }
            }

            if (cmbCustomer.Items.Count > 0)
            {
                cmbCustomer.SelectedIndex = startIndex;
                customer = customers[startIndex];
                PopulateAccountCombos();
            }

            cmbCustomer.SelectedIndexChanged += cmbCustomer_SelectedIndexChanged;
        }

        private void cmbCustomer_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var customers = customersController.GetAllCustomers();
            if (cmbCustomer.SelectedIndex < 0 || cmbCustomer.SelectedIndex >= customers.Count)
            {
                return;
            }

            customer = customers[cmbCustomer.SelectedIndex];
            PopulateAccountCombos();
        }

        private void cmbSource_SelectedIndexChanged(object? sender, EventArgs e) => UpdateBalances();

        private void cmbDestination_SelectedIndexChanged(object? sender, EventArgs e) => UpdateBalances();

        private void PopulateAccountCombos()
        {
            cmbSource.Items.Clear();
            cmbDestination.Items.Clear();

            foreach (var account in customer.Accounts)
            {
                string label = AccountLabel(account);
                cmbSource.Items.Add(label);
                cmbDestination.Items.Add(label);
            }

            bool canTransfer = customer.Accounts.Count >= 2;
            cmbSource.Enabled = canTransfer;
            cmbDestination.Enabled = canTransfer;
            txtAmount.Enabled = canTransfer;
            btnExecute.Enabled = canTransfer;

            if (canTransfer)
            {
                cmbSource.SelectedIndex = 0;
                cmbDestination.SelectedIndex = 1;
                lblResult.ForeColor = TextGray;
                lblResult.Text = "";
            }
            else
            {
                lblResult.ForeColor = ErrorRed;
                lblResult.Text = "This customer needs at least two accounts before a transfer can be made.";
                lblSourceBal.Text = "";
                lblDestBal.Text = "";
            }

            lblStaff.Text = customer.IsStaff
                ? "Bank Staff benefit active: failed-transfer fees are charged at 50%."
                : "";

            UpdateBalances();
        }

        private void UpdateBalances()
        {
            if (cmbSource.SelectedIndex >= 0 && cmbSource.SelectedIndex < customer.Accounts.Count)
            {
                var src = customer.Accounts[cmbSource.SelectedIndex];
                lblSourceBal.Text = $"Available: {src.Balance:C2}";
            }

            if (cmbDestination.SelectedIndex >= 0 && cmbDestination.SelectedIndex < customer.Accounts.Count)
            {
                var dst = customer.Accounts[cmbDestination.SelectedIndex];
                lblDestBal.Text = $"Current: {dst.Balance:C2}";
            }
        }

        private void btnExecute_Click(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                lblResult.ForeColor = ErrorRed;
                lblResult.Text = "Enter a valid positive amount to transfer.";
                return;
            }

            if (cmbSource.SelectedIndex == cmbDestination.SelectedIndex)
            {
                lblResult.ForeColor = ErrorRed;
                lblResult.Text = "Source and destination must be different accounts.";
                return;
            }

            try
            {
                string result = transfersController.TransferFunds(
                    customer.CustomerNumber,
                    cmbSource.SelectedIndex,
                    cmbDestination.SelectedIndex,
                    amount);

                TransferPerformed = true;
                lblResult.ForeColor = SuccessGreen;
                lblResult.Text = result;
                txtAmount.Clear();
            }
            catch (BankingException ex)
            {
                TransferPerformed = true;
                lblResult.ForeColor = ErrorRed;
                lblResult.Text = ex.Message;
            }

            RefreshAccountCombos();
        }

        private void btnClose_Click(object? sender, EventArgs e) => Close();

        private void RefreshAccountCombos()
        {
            int src = cmbSource.SelectedIndex;
            int dst = cmbDestination.SelectedIndex;

            cmbSource.Items.Clear();
            cmbDestination.Items.Clear();

            foreach (var account in customer.Accounts)
            {
                string label = AccountLabel(account);
                cmbSource.Items.Add(label);
                cmbDestination.Items.Add(label);
            }

            if (src >= 0 && src < cmbSource.Items.Count)
            {
                cmbSource.SelectedIndex = src;
            }
            if (dst >= 0 && dst < cmbDestination.Items.Count)
            {
                cmbDestination.SelectedIndex = dst;
            }

            UpdateBalances();
        }

        private static string AccountLabel(Account account)
        {
            return $"{account.AccountName}   -   ID {account.AccountId}   -   {account.Balance:C2}";
        }

        private void txtAmount_KeyPress(object? sender, KeyPressEventArgs e)
        {
            bool isDigit = char.IsDigit(e.KeyChar);
            bool isDecimal = (e.KeyChar == '.') && !txtAmount.Text.Contains('.');
            bool isBackspace = (e.KeyChar == '\b');

            if (!isDigit && !isDecimal && !isBackspace)
            {
                e.Handled = true;
            }
        }
    }
}
