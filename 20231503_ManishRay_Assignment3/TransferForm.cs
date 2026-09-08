using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3
{
    public class TransferForm : BaseForm
    {
        private readonly CustomerController controller;
        private User customer = null!;

        private ComboBox cmbCustomer = null!;
        private ComboBox cmbSource = null!;
        private ComboBox cmbDestination = null!;
        private TextBox txtAmount = null!;
        private Label lblSourceBal = null!;
        private Label lblDestBal = null!;
        private Label lblStaff = null!;
        private Label lblResult = null!;
        private Button btnExecute = null!;

        public bool TransferPerformed { get; private set; }

        public TransferForm(CustomerController customerController, User? initialCustomer = null)
        {
            controller = customerController;
            BuildUI();
            PopulateCustomers(initialCustomer);
        }

        private void BuildUI()
        {
            Text = "Intra-Account Transfer";
            ClientSize = new Size(480, 486);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Controls.Add(CreateBrandBar("Intra-Account Transfer  -  move funds between your own accounts"));

            var lblCustHead = CreateFieldLabel("CUSTOMER PROFILE");
            lblCustHead.Location = new Point(24, 78);
            lblCustHead.Size = new Size(432, 16);

            cmbCustomer = CreateStyledComboBox();
            cmbCustomer.Location = new Point(24, 96);
            cmbCustomer.Size = new Size(432, 26);
            cmbCustomer.SelectedIndexChanged += cmbCustomer_SelectedIndexChanged;

            var divider = new Panel();
            divider.Location = new Point(24, 136);
            divider.Size = new Size(432, 1);
            divider.BackColor = Color.FromArgb(35, 52, 88);

            var lblFrom = CreateFieldLabel("TRANSFER FROM  (SOURCE)");
            lblFrom.Location = new Point(24, 150);
            lblFrom.Size = new Size(432, 16);

            cmbSource = CreateStyledComboBox();
            cmbSource.Location = new Point(24, 168);
            cmbSource.Size = new Size(432, 26);
            cmbSource.SelectedIndexChanged += (s, e) => UpdateBalances();

            lblSourceBal = new Label();
            lblSourceBal.Location = new Point(24, 198);
            lblSourceBal.Size = new Size(432, 16);
            lblSourceBal.ForeColor = Gold;
            lblSourceBal.Font = new Font("Segoe UI", 8f);

            var lblTo = CreateFieldLabel("TRANSFER TO  (DESTINATION)");
            lblTo.Location = new Point(24, 226);
            lblTo.Size = new Size(432, 16);

            cmbDestination = CreateStyledComboBox();
            cmbDestination.Location = new Point(24, 244);
            cmbDestination.Size = new Size(432, 26);
            cmbDestination.SelectedIndexChanged += (s, e) => UpdateBalances();

            lblDestBal = new Label();
            lblDestBal.Location = new Point(24, 274);
            lblDestBal.Size = new Size(432, 16);
            lblDestBal.ForeColor = Gold;
            lblDestBal.Font = new Font("Segoe UI", 8f);

            var lblAmtHead = CreateFieldLabel("AMOUNT  ($NZD)");
            lblAmtHead.Location = new Point(24, 302);
            lblAmtHead.Size = new Size(432, 16);

            txtAmount = CreateStyledTextBox();
            txtAmount.Location = new Point(24, 320);
            txtAmount.Size = new Size(200, 28);
            txtAmount.PlaceholderText = "0.00";
            txtAmount.KeyPress += txtAmount_KeyPress;

            lblStaff = new Label();
            lblStaff.Location = new Point(24, 356);
            lblStaff.Size = new Size(432, 16);
            lblStaff.ForeColor = Color.FromArgb(110, 130, 170);
            lblStaff.Font = new Font("Segoe UI", 7.5f, FontStyle.Italic);

            btnExecute = CreatePrimaryButton("EXECUTE TRANSFER");
            btnExecute.Location = new Point(24, 384);
            btnExecute.Size = new Size(200, 38);
            btnExecute.Click += btnExecute_Click;

            var btnClose = CreateNeutralButton("CLOSE");
            btnClose.Location = new Point(236, 384);
            btnClose.Size = new Size(90, 38);
            btnClose.Click += (s, e) => Close();

            lblResult = new Label();
            lblResult.Location = new Point(24, 432);
            lblResult.Size = new Size(432, 44);
            lblResult.ForeColor = TextGray;
            lblResult.Font = new Font("Segoe UI", 8f);

            Controls.AddRange(new Control[]
            {
                lblCustHead, cmbCustomer, divider,
                lblFrom, cmbSource, lblSourceBal,
                lblTo, cmbDestination, lblDestBal,
                lblAmtHead, txtAmount, lblStaff,
                btnExecute, btnClose, lblResult
            });

            AcceptButton = btnExecute;
            CancelButton = btnClose;
        }

        private void PopulateCustomers(User? initialCustomer)
        {
            cmbCustomer.SelectedIndexChanged -= cmbCustomer_SelectedIndexChanged;
            cmbCustomer.Items.Clear();

            var customers = controller.GetAllCustomers();
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
            var customers = controller.GetAllCustomers();
            if (cmbCustomer.SelectedIndex < 0 || cmbCustomer.SelectedIndex >= customers.Count)
            {
                return;
            }

            customer = customers[cmbCustomer.SelectedIndex];
            PopulateAccountCombos();
        }

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
                string result = controller.TransferFunds(
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
