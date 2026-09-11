using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3
{
    // Modal dialog for attaching a new account to an existing customer at runtime. The layout lives
    // in AddAccountForm.Designer.cs; this file holds only the behaviour.
    public partial class AddAccountForm : BaseForm
    {
        private readonly AccountController controller;
        private readonly User targetUser;

        public AddAccountForm(BankController bank, User user)
        {
            InitializeComponent();

            controller = bank.Accounts;
            targetUser = user;

            lblHead.Text = $"NEW ACCOUNT FOR {targetUser.Name.ToUpperInvariant()}  (#{targetUser.CustomerNumber})";
            cmbType.SelectedIndex = 0;
            UpdateExtraField();
        }

        private void cmbType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateExtraField();
        }

        private void UpdateExtraField()
        {
            switch (cmbType.SelectedIndex)
            {
                case 1:
                    lblExtra.Text = "Interest Rate:";
                    lblExtra.Enabled = true;
                    txtExtra.Enabled = true;
                    if (string.IsNullOrWhiteSpace(txtExtra.Text) || txtExtra.Text == "500.00")
                    {
                        txtExtra.Text = "0.05";
                    }
                    break;
                case 2:
                    lblExtra.Text = "Overdraft Limit ($):";
                    lblExtra.Enabled = true;
                    txtExtra.Enabled = true;
                    if (string.IsNullOrWhiteSpace(txtExtra.Text) || txtExtra.Text == "0.05")
                    {
                        txtExtra.Text = "500.00";
                    }
                    break;
                default:
                    lblExtra.Text = "(not applicable):";
                    lblExtra.Enabled = false;
                    txtExtra.Enabled = false;
                    txtExtra.Text = "";
                    break;
            }
        }

        private void btnCreate_Click(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(txtInitial.Text, out decimal initial) || initial < 0)
            {
                MessageBox.Show("Enter a valid non-negative initial balance.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtExtra.Text, out decimal extra);

            string typeKey = cmbType.SelectedIndex switch
            {
                1 => "investment",
                2 => "omni",
                _ => "everyday"
            };

            bool ok = controller.AddAccountToCustomer(targetUser.CustomerNumber, typeKey, initial, extra);
            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Unable to add the account. Please check your inputs.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lblBrandLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
