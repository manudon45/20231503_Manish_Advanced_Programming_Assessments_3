using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3
{
    public partial class CustomerManagementForm : BaseForm
    {
        private readonly CustomerController controller;

        public CustomerManagementForm(BankController bank)
        {
            InitializeComponent();
            controller = bank.Customers;
            RefreshCustomerList();
        }

        private void cmbCustRole_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool isStaff = cmbCustRole.SelectedIndex == 1;
            txtCustStaffId.Enabled = isStaff;
            if (isStaff && string.IsNullOrWhiteSpace(txtCustStaffId.Text))
            {
                txtCustStaffId.Text = "STF-0099";
            }
        }

        private void RefreshCustomerList()
        {
            lstCustomers.SelectedIndexChanged -= lstCustomers_SelectedIndexChanged;
            lstCustomers.Items.Clear();

            var list = controller.GetAllCustomers();
            foreach (var cust in list)
            {
                lstCustomers.Items.Add($"{cust.CustomerNumber} - {cust.Name} [{cust.GetRoleLabel()}]");
            }
            lstCustomers.SelectedIndexChanged += lstCustomers_SelectedIndexChanged;
        }

        private void lstCustomers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstCustomers.SelectedIndex < 0) return;

            var user = controller.GetCustomerByIndex(lstCustomers.SelectedIndex);
            if (user != null)
            {
                txtCustId.Text = user.CustomerNumber;
                txtCustName.Text = user.Name;
                txtCustContact.Text = user.ContactDetails;

                if (user is BankStaff staff)
                {
                    cmbCustRole.SelectedIndex = 1;
                    txtCustStaffId.Text = staff.StaffId;
                }
                else
                {
                    cmbCustRole.SelectedIndex = 0;
                    txtCustStaffId.Text = "";
                }

                if (user.Accounts.Count >= 3)
                {
                    txtEverydayBal.Text = user.Accounts[0].Balance.ToString("F2");
                    txtInvBal.Text = user.Accounts[1].Balance.ToString("F2");
                    txtOmniBal.Text = user.Accounts[2].Balance.ToString("F2");
                    if (user.Accounts[2] is OmniAccount omni)
                    {
                        txtOmniOverdraft.Text = omni.OverdraftLimit.ToString("F2");
                    }
                }
            }
        }

        private void btnAddCust_Click(object? sender, EventArgs e)
        {
            string name = txtCustName.Text;
            string contact = txtCustContact.Text;
            bool isStaff = cmbCustRole.SelectedIndex == 1;
            string staffId = txtCustStaffId.Text;

            decimal.TryParse(txtEverydayBal.Text, out decimal evBal);
            decimal.TryParse(txtInvBal.Text, out decimal invBal);
            decimal.TryParse(txtOmniBal.Text, out decimal omniBal);
            decimal.TryParse(txtOmniOverdraft.Text, out decimal omniOd);

            bool success = controller.AddCustomer(name, contact, isStaff, staffId, evBal, 0.05m, invBal, omniOd, omniBal);
            if (success)
            {
                RefreshCustomerList();
                btnClearCust_Click(null, EventArgs.Empty);
                MessageBox.Show("Customer successfully added via Controller.", "MVC Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter a valid Customer Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdateCust_Click(object? sender, EventArgs e)
        {
            string id = txtCustId.Text;
            string name = txtCustName.Text;
            string contact = txtCustContact.Text;

            bool success = controller.UpdateCustomer(id, name, contact);
            if (success)
            {
                RefreshCustomerList();
                MessageBox.Show("Customer details successfully updated via Controller.", "MVC Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Select a valid customer to update.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteCust_Click(object? sender, EventArgs e)
        {
            string id = txtCustId.Text;
            bool success = controller.DeleteCustomer(id);
            if (success)
            {
                RefreshCustomerList();
                btnClearCust_Click(null, EventArgs.Empty);
                MessageBox.Show("Customer deleted via Controller.", "MVC Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cannot delete customer. Ensure a customer is selected and at least one customer remains.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClearCust_Click(object? sender, EventArgs e)
        {
            txtCustId.Text = "(Auto Generated)";
            txtCustName.Clear();
            txtCustContact.Clear();
            cmbCustRole.SelectedIndex = 0;
            txtCustStaffId.Clear();
            txtEverydayBal.Text = "500.00";
            txtInvBal.Text = "1000.00";
            txtOmniBal.Text = "750.00";
            txtOmniOverdraft.Text = "500.00";
            lstCustomers.SelectedIndex = -1;
        }
    }
}
