using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3
{
    public class AddAccountForm : Form
    {
        private static readonly Color NavyDark = Color.FromArgb(15, 27, 53);
        private static readonly Color NavyLight = Color.FromArgb(40, 60, 100);
        private static readonly Color Gold = Color.FromArgb(201, 168, 76);
        private static readonly Color TextGray = Color.FromArgb(160, 180, 210);
        private static readonly Color SuccessGreen = Color.FromArgb(46, 204, 113);

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
            ClientSize = new Size(430, 300);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = NavyDark;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9f);

            var lblHead = new Label
            {
                Text = $"NEW ACCOUNT FOR {targetUser.Name.ToUpperInvariant()} (#{targetUser.CustomerNumber})",
                ForeColor = Gold,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(20, 18),
                Size = new Size(390, 22)
            };

            var lblType = MakeLabel("Account Type:", new Point(20, 58));
            cmbType = new ComboBox
            {
                Location = new Point(160, 55),
                Size = new Size(240, 24),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                BackColor = NavyLight,
                ForeColor = Color.White
            };
            cmbType.Items.AddRange(new object[] { "Everyday Account", "Investment Account", "Omni Account" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += (s, e) => UpdateExtraField();

            var lblInit = MakeLabel("Initial Balance ($):", new Point(20, 100));
            txtInitial = MakeTextBox(new Point(160, 97), "0.00");

            lblExtra = MakeLabel("Interest Rate:", new Point(20, 142));
            txtExtra = MakeTextBox(new Point(160, 139), "");

            var btnCreate = new Button
            {
                Text = "CREATE ACCOUNT",
                Location = new Point(160, 198),
                Size = new Size(160, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = SuccessGreen,
                ForeColor = NavyDark,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.Click += btnCreate_Click;

            var btnCancel = new Button
            {
                Text = "CANCEL",
                Location = new Point(330, 198),
                Size = new Size(80, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = NavyLight,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            var lblHint = new Label
            {
                Text = "Everyday accounts carry no interest, fees or overdraft.\n"
                     + "Investment: enter an interest rate (e.g. 0.05).\n"
                     + "Omni: enter an overdraft limit (e.g. 500.00).",
                ForeColor = TextGray,
                Font = new Font("Segoe UI", 7.5f),
                Location = new Point(20, 244),
                Size = new Size(390, 48)
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

        private static Label MakeLabel(string text, Point loc) => new Label
        {
            Text = text,
            ForeColor = TextGray,
            Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
            Location = loc,
            Size = new Size(135, 20)
        };

        private static TextBox MakeTextBox(Point loc, string placeholder) => new TextBox
        {
            Location = loc,
            Size = new Size(240, 24),
            BackColor = NavyLight,
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            PlaceholderText = placeholder
        };
    }
}
