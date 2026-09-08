using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3
{
    public class AddAccountForm : BaseForm
    {
        private readonly CustomerController controller;
        private readonly User targetUser;

        private ComboBox cmbType = null!;
        private TextBox txtInitial = null!;
        private TextBox txtExtra = null!;
        private Label lblExtra = null!;

        public AddAccountForm(CustomerController customerController, User user)
        {
            controller = customerController;
            targetUser = user;
            BuildUI();
        }

        private void BuildUI()
        {
            Text = "Add New Account";
            ClientSize = new Size(440, 372);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Controls.Add(CreateBrandBar("Add a new account to a customer profile"));

            var lblHead = new Label
            {
                Text = $"NEW ACCOUNT FOR {targetUser.Name.ToUpperInvariant()}  (#{targetUser.CustomerNumber})",
                ForeColor = Gold,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(20, 80),
                Size = new Size(400, 22)
            };

            var lblType = CreateFieldLabel("Account Type:");
            lblType.Location = new Point(20, 120);
            cmbType = CreateStyledComboBox();
            cmbType.Location = new Point(170, 117);
            cmbType.Size = new Size(240, 24);
            cmbType.Items.AddRange(new object[] { "Everyday Account", "Investment Account", "Omni Account" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += (s, e) => UpdateExtraField();

            var lblInit = CreateFieldLabel("Initial Balance ($):");
            lblInit.Location = new Point(20, 160);
            txtInitial = CreateStyledTextBox();
            txtInitial.Location = new Point(170, 157);
            txtInitial.Size = new Size(240, 24);
            txtInitial.PlaceholderText = "0.00";

            lblExtra = CreateFieldLabel("Interest Rate:");
            lblExtra.Location = new Point(20, 200);
            txtExtra = CreateStyledTextBox();
            txtExtra.Location = new Point(170, 197);
            txtExtra.Size = new Size(240, 24);

            var btnCreate = CreatePrimaryButton("CREATE ACCOUNT");
            btnCreate.Location = new Point(170, 254);
            btnCreate.Size = new Size(160, 36);
            btnCreate.Click += btnCreate_Click;

            var btnCancel = CreateNeutralButton("CANCEL");
            btnCancel.Location = new Point(338, 254);
            btnCancel.Size = new Size(72, 36);
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            var lblHint = new Label
            {
                Text = "Everyday accounts carry no interest, fees or overdraft.\n"
                     + "Investment: enter an interest rate (e.g. 0.05).\n"
                     + "Omni: enter an overdraft limit (e.g. 500.00).",
                ForeColor = TextGray,
                Font = new Font("Segoe UI", 7.5f),
                Location = new Point(20, 304),
                Size = new Size(400, 52)
            };

            Controls.AddRange(new Control[]
            {
                lblHead, lblType, cmbType, lblInit, txtInitial, lblExtra, txtExtra, btnCreate, btnCancel, lblHint
            });

            AcceptButton = btnCreate;
            CancelButton = btnCancel;
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
    }
}
