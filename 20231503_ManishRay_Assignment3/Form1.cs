using _20231503_ManishRay_Assignment3.Controllers;
using _20231503_ManishRay_Assignment3.Exceptions;
using _20231503_ManishRay_Assignment3.Models;

namespace _20231503_ManishRay_Assignment3
{
    public partial class Form1 : Form
    {
        // Colour palette
        private static readonly Color NavyDark = Color.FromArgb(15, 27, 53);
        private static readonly Color NavyMid = Color.FromArgb(27, 42, 74);
        private static readonly Color NavyLight = Color.FromArgb(40, 60, 100);
        private static readonly Color NavStrip = Color.FromArgb(10, 20, 42);
        private static readonly Color Gold = Color.FromArgb(201, 168, 76);
        private static readonly Color TextGray = Color.FromArgb(160, 180, 210);
        private static readonly Color SuccessGreen = Color.FromArgb(46, 204, 113);
        private static readonly Color ErrorRed = Color.FromArgb(231, 76, 60);
        private static readonly Color CardBg = Color.FromArgb(22, 38, 68);

        // Data: customer controller instance
        private readonly CustomerController customerController;
        private User currentUser = null!;
        private Account currentAccount = null!;

        // Controls we need to read or update at runtime
        private ComboBox cmbUser = null!;
        private Label lblUserInfo = null!;
        private FlowLayoutPanel flowTabs = null!;
        private readonly List<Button> navButtons = new();
        private readonly List<Panel> navIndicators = new();
        private Button btnManageCustomers = null!;
        private Button btnAddAccount = null!;
        private Label lblAccTitle = null!;
        private Label lblBalance = null!;
        private Label lblAccDetails = null!;
        private TextBox txtAmount = null!;
        private Button btnDeposit = null!;
        private Button btnWithdraw = null!;
        private Button btnCalcInterest = null!;
        private ListBox lstHistory = null!;

        public Form1()
        {
            InitializeComponent();
            customerController = new CustomerController();
            BuildUI();
            PopulateUserDropdown();
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
        }

        private void PopulateUserDropdown()
        {
            cmbUser.SelectedIndexChanged -= cmbUser_SelectedIndexChanged;
            cmbUser.Items.Clear();
            foreach (var user in customerController.GetAllCustomers())
            {
                cmbUser.Items.Add($"{user.Name} ({user.GetRoleLabel()})");
            }
            if (cmbUser.Items.Count > 0)
            {
                cmbUser.SelectedIndex = 0;
                SwitchUser(0);
            }
            cmbUser.SelectedIndexChanged += cmbUser_SelectedIndexChanged;
        }

        private void BuildUI()
        {
            this.BackColor = NavyDark;
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9f);

            // 4-row vertical layout for the whole form
            var layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.RowCount = 4;
            layout.ColumnCount = 1;
            layout.BackColor = NavyDark;
            layout.Padding = Padding.Empty;
            layout.Margin = Padding.Empty;
            layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 132f));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140f));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90f));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            layout.Controls.Add(BuildHeader(), 0, 0);
            layout.Controls.Add(BuildAccountCard(), 0, 1);
            layout.Controls.Add(BuildTransaction(), 0, 2);
            layout.Controls.Add(BuildHistory(), 0, 3);

            this.Controls.Add(layout);
        }

        // Logo, bank name, user dropdown, and nav tabs
        private Panel BuildHeader()
        {
            var panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = NavyDark;

            // Gold "MB" logo box
            var logo = new Label();
            logo.Text = "MB";
            logo.Size = new Size(50, 50);
            logo.Location = new Point(20, 13);
            logo.BackColor = Gold;
            logo.ForeColor = NavyDark;
            logo.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            logo.TextAlign = ContentAlignment.MiddleCenter;

            // Bank name
            var lblName = new Label();
            lblName.Text = "ManishRayyy Bank";
            lblName.AutoSize = false;
            lblName.Location = new Point(82, 10);
            lblName.Size = new Size(300, 36);
            lblName.ForeColor = Color.White;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Segoe UI", 21f, FontStyle.Bold);

            // Tagline
            var lblTag = new Label();
            lblTag.Text = "Smart Banking. Real Returns.";
            lblTag.AutoSize = false;
            lblTag.Location = new Point(84, 48);
            lblTag.Size = new Size(280, 16);
            lblTag.ForeColor = Gold;
            lblTag.BackColor = Color.Transparent;
            lblTag.Font = new Font("Segoe UI", 8.5f, FontStyle.Italic);

            // "ACTIVE USER" caption above the dropdown
            var lblUserHead = new Label();
            lblUserHead.Text = "ACTIVE USER";
            lblUserHead.AutoSize = false;
            lblUserHead.Location = new Point(700, 12);
            lblUserHead.Size = new Size(110, 14);
            lblUserHead.ForeColor = TextGray;
            lblUserHead.BackColor = Color.Transparent;
            lblUserHead.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);

            // Dropdown to switch between users
            cmbUser = new ComboBox();
            cmbUser.Location = new Point(700, 30);
            cmbUser.Size = new Size(240, 26);
            cmbUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUser.FlatStyle = FlatStyle.Flat;
            cmbUser.BackColor = NavyLight;
            cmbUser.ForeColor = Color.White;
            cmbUser.Font = new Font("Segoe UI", 9.5f);
            cmbUser.SelectedIndexChanged += cmbUser_SelectedIndexChanged;

            lblUserInfo = new Label();
            lblUserInfo.AutoSize = false;
            lblUserInfo.Size = new Size(350, 18);
            lblUserInfo.Location = new Point(590, 58);
            lblUserInfo.ForeColor = TextGray;
            lblUserInfo.BackColor = Color.Transparent;
            lblUserInfo.Font = new Font("Segoe UI", 8f);
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;

            // Dark strip at the bottom of the header that holds the nav tabs
            var pnlNav = new Panel();
            pnlNav.Dock = DockStyle.Bottom;
            pnlNav.Height = 56;
            pnlNav.BackColor = NavStrip;

            // Thin gold line along the very bottom of the nav strip
            var navBorder = new Panel();
            navBorder.Dock = DockStyle.Bottom;
            navBorder.Height = 2;
            navBorder.BackColor = Gold;

            // Horizontally scrolling container for the dynamic account tabs
            flowTabs = new FlowLayoutPanel();
            flowTabs.Dock = DockStyle.Fill;
            flowTabs.BackColor = NavStrip;
            flowTabs.WrapContents = false;
            flowTabs.AutoScroll = true;
            flowTabs.Padding = new Padding(8, 4, 8, 0);

            var pnlNavRight = new Panel();
            pnlNavRight.Dock = DockStyle.Right;
            pnlNavRight.Width = 330;
            pnlNavRight.BackColor = NavStrip;

            btnAddAccount = new Button();
            btnAddAccount.Text = "+ ADD ACCOUNT";
            btnAddAccount.Location = new Point(8, 11);
            btnAddAccount.Size = new Size(150, 32);
            btnAddAccount.FlatStyle = FlatStyle.Flat;
            btnAddAccount.BackColor = SuccessGreen;
            btnAddAccount.ForeColor = NavyDark;
            btnAddAccount.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            btnAddAccount.Cursor = Cursors.Hand;
            btnAddAccount.FlatAppearance.BorderSize = 0;
            btnAddAccount.Click += btnAddAccount_Click;

            btnManageCustomers = new Button();
            btnManageCustomers.Text = "MANAGE CUSTOMERS";
            btnManageCustomers.Location = new Point(164, 11);
            btnManageCustomers.Size = new Size(158, 32);
            btnManageCustomers.FlatStyle = FlatStyle.Flat;
            btnManageCustomers.BackColor = Gold;
            btnManageCustomers.ForeColor = NavyDark;
            btnManageCustomers.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            btnManageCustomers.Cursor = Cursors.Hand;
            btnManageCustomers.FlatAppearance.BorderSize = 0;
            btnManageCustomers.Click += btnManageCustomers_Click;

            pnlNavRight.Controls.Add(btnAddAccount);
            pnlNavRight.Controls.Add(btnManageCustomers);

            pnlNav.Controls.Add(flowTabs);
            pnlNav.Controls.Add(pnlNavRight);
            pnlNav.Controls.Add(navBorder);

            panel.Controls.Add(logo);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblTag);
            panel.Controls.Add(lblUserHead);
            panel.Controls.Add(cmbUser);
            panel.Controls.Add(lblUserInfo);
            panel.Controls.Add(pnlNav);
            return panel;
        }

        private void btnAddAccount_Click(object? sender, EventArgs e)
        {
            if (currentUser == null)
            {
                return;
            }

            using var dialog = new AddAccountForm(customerController, currentUser);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                RebuildAccountTabs();
                SelectAccount(currentUser.Accounts.Count - 1);
                LogTransaction("New account added to profile.");
            }
        }

        private void RebuildAccountTabs()
        {
            flowTabs.SuspendLayout();
            flowTabs.Controls.Clear();
            navButtons.Clear();
            navIndicators.Clear();

            var accounts = currentUser.Accounts;
            for (int i = 0; i < accounts.Count; i++)
            {
                int index = i;
                var item = CreateNavItem(accounts[i].AccountName);
                item.button.Click += (s, e) => SelectAccount(index);
                navButtons.Add(item.button);
                navIndicators.Add(item.indicator);
                flowTabs.Controls.Add(item.container);
            }

            flowTabs.ResumeLayout();
        }

        private void btnManageCustomers_Click(object? sender, EventArgs e)
        {
            var form = new CustomerManagementForm(customerController);
            form.ShowDialog();
            PopulateUserDropdown();
        }

        // Creates a single nav tab: a button with a gold underline indicator below it.
        // Returns a tuple containing the container, button, and indicator.
        private (Panel container, Button button, Panel indicator) CreateNavItem(string label)
        {
            var container = new Panel();
            container.Size = new Size(178, 32);
            container.Margin = new Padding(3, 0, 3, 0);
            container.BackColor = NavStrip;

            // Gold underline - only visible when this tab is the active one
            var indicator = new Panel();
            indicator.Dock = DockStyle.Bottom;
            indicator.Height = 3;
            indicator.BackColor = Gold;
            indicator.Visible = false;

            // The clickable button that fills the container
            var button = new Button();
            button.Dock = DockStyle.Fill;
            button.Text = label;
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = NavStrip;
            button.ForeColor = TextGray;
            button.Font = new Font("Segoe UI", 9.5f);
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.BorderSize = 0;

            container.Controls.Add(button);
            container.Controls.Add(indicator);
            return (container, button, indicator);
        }

        // Account name, balance, and detail line
        private Panel BuildAccountCard()
        {
            var panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = NavyDark;

            // Account name + ID (e.g. "Investment Account - Account ID: 1001")
            lblAccTitle = new Label();
            lblAccTitle.AutoSize = false;
            lblAccTitle.Location = new Point(28, 14);
            lblAccTitle.Size = new Size(700, 20);
            lblAccTitle.ForeColor = TextGray;
            lblAccTitle.BackColor = Color.Transparent;
            lblAccTitle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

            // Small "CURRENT BALANCE" heading above the amount
            var lblBalHead = new Label();
            lblBalHead.Text = "CURRENT BALANCE";
            lblBalHead.AutoSize = false;
            lblBalHead.Location = new Point(28, 38);
            lblBalHead.Size = new Size(200, 15);
            lblBalHead.ForeColor = TextGray;
            lblBalHead.BackColor = Color.Transparent;
            lblBalHead.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);

            // Large balance amount — turns red when balance is negative (Omni overdraft)
            lblBalance = new Label();
            lblBalance.AutoSize = false;
            lblBalance.Location = new Point(24, 54);
            lblBalance.Size = new Size(460, 54);
            lblBalance.ForeColor = Gold;
            lblBalance.BackColor = Color.Transparent;
            lblBalance.Font = new Font("Segoe UI", 32f, FontStyle.Bold);

            // Extra account info (interest rate, fees, overdraft limit)
            lblAccDetails = new Label();
            lblAccDetails.AutoSize = false;
            lblAccDetails.Location = new Point(28, 112);
            lblAccDetails.Size = new Size(900, 18);
            lblAccDetails.ForeColor = TextGray;
            lblAccDetails.BackColor = Color.Transparent;
            lblAccDetails.Font = new Font("Segoe UI", 8.5f);

            // Thin separator between the card and the transaction row
            var divider = new Panel();
            divider.Dock = DockStyle.Bottom;
            divider.Height = 1;
            divider.BackColor = Color.FromArgb(35, 52, 88);

            panel.Controls.Add(lblAccTitle);
            panel.Controls.Add(lblBalHead);
            panel.Controls.Add(lblBalance);
            panel.Controls.Add(lblAccDetails);
            panel.Controls.Add(divider);
            return panel;
        }

        // Amount input and action buttons
        private Panel BuildTransaction()
        {
            var panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = NavyMid;

            var lblAmtHead = new Label();
            lblAmtHead.Text = "TRANSACTION AMOUNT ($NZD)";
            lblAmtHead.AutoSize = false;
            lblAmtHead.Location = new Point(28, 12);
            lblAmtHead.Size = new Size(260, 15);
            lblAmtHead.ForeColor = TextGray;
            lblAmtHead.BackColor = Color.Transparent;
            lblAmtHead.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);

            // Amount input — KeyPress handler restricts input to numbers and one decimal point
            txtAmount = new TextBox();
            txtAmount.Location = new Point(28, 31);
            txtAmount.Size = new Size(175, 30);
            txtAmount.BackColor = NavyLight;
            txtAmount.ForeColor = Color.White;
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.Font = new Font("Segoe UI", 11.5f);
            txtAmount.PlaceholderText = "0.00";
            txtAmount.KeyPress += txtAmount_KeyPress;

            btnDeposit = CreateActionButton("DEPOSIT", new Point(216, 27), SuccessGreen, NavyDark);
            btnDeposit.Click += btnDeposit_Click;

            btnWithdraw = CreateActionButton("WITHDRAW", new Point(366, 27), ErrorRed, Color.White);
            btnWithdraw.Click += btnWithdraw_Click;

            // Only visible for Investment and Omni accounts (they support interest)
            btnCalcInterest = CreateActionButton("CALC INTEREST", new Point(516, 27), Gold, NavyDark);
            btnCalcInterest.Size = new Size(155, 36);
            btnCalcInterest.Visible = false;
            btnCalcInterest.Click += btnCalcInterest_Click;

            var lblNote = new Label();
            lblNote.Text = "***Bank Staff users receive a 50% discount on any transaction fees";
            lblNote.AutoSize = false;
            lblNote.Location = new Point(28, 70);
            lblNote.Size = new Size(620, 14);
            lblNote.ForeColor = Color.FromArgb(110, 130, 170);
            lblNote.BackColor = Color.Transparent;
            lblNote.Font = new Font("Segoe UI", 7.5f, FontStyle.Italic);

            panel.Controls.Add(lblAmtHead);
            panel.Controls.Add(txtAmount);
            panel.Controls.Add(btnDeposit);
            panel.Controls.Add(btnWithdraw);
            panel.Controls.Add(btnCalcInterest);
            panel.Controls.Add(lblNote);
            return panel;
        }

        // Transaction history list
        private Panel BuildHistory()
        {
            var panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = NavyDark;
            panel.Padding = new Padding(28, 14, 28, 10);

            // Title label at the top
            var lblTitle = new Label();
            lblTitle.Text = "TRANSACTION HISTORY";
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 26;
            lblTitle.ForeColor = TextGray;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            // Clear button at the bottom
            var btnClear = new Button();
            btnClear.Text = "Clear History";
            btnClear.Dock = DockStyle.Bottom;
            btnClear.Height = 32;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.BackColor = NavyLight;
            btnClear.ForeColor = TextGray;
            btnClear.Font = new Font("Segoe UI", 8.5f);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderColor = Color.FromArgb(60, 80, 120);
            btnClear.FlatAppearance.BorderSize = 1;
            btnClear.Click += btnClear_Click;

            // List fills the remaining space between the title and the button
            lstHistory = new ListBox();
            lstHistory.Dock = DockStyle.Fill;
            lstHistory.BackColor = CardBg;
            lstHistory.ForeColor = Color.White;
            lstHistory.BorderStyle = BorderStyle.None;
            lstHistory.Font = new Font("Consolas", 9f);
            lstHistory.SelectionMode = SelectionMode.None;

            panel.Controls.Add(lstHistory);
            panel.Controls.Add(btnClear);
            panel.Controls.Add(lblTitle);
            return panel;
        }

        private Button CreateActionButton(string text, Point location, Color backColor, Color foreColor)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Location = location;
            btn.Size = new Size(142, 36);
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = backColor;
            btn.ForeColor = foreColor;
            btn.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void cmbUser_SelectedIndexChanged(object? sender, EventArgs e)
        {
            SwitchUser(cmbUser.SelectedIndex);
        }

        private void btnDeposit_Click(object? sender, EventArgs e)
        {
            var (success, amount) = TryParseAmount();
            if (!success)
            {
                return;
            }

            try
            {
                string result = currentAccount.Deposit(amount, currentUser.IsStaff);
                LogTransaction(result);
                RefreshAccountDisplay();
                txtAmount.Clear();
            }
            catch (BankingException ex)
            {
                LogTransaction($"[FAIL] {ex.Message}");
                MessageBox.Show(ex.Message, "Transaction Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefreshAccountDisplay();
            }
        }

        private void btnWithdraw_Click(object? sender, EventArgs e)
        {
            var (success, amount) = TryParseAmount();
            if (!success)
            {
                return;
            }

            try
            {
                string result = currentAccount.Withdraw(amount, currentUser.IsStaff);
                LogTransaction(result);
                RefreshAccountDisplay();
                txtAmount.Clear();
            }
            catch (BankingException ex)
            {
                LogTransaction($"[FAIL] {ex.Message}");
                MessageBox.Show(ex.Message, "Transaction Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefreshAccountDisplay();
            }
        }

        private void btnCalcInterest_Click(object? sender, EventArgs e)
        {
            string result;

            if (currentAccount is InvestmentAccount investmentAccount)
            {
                result = investmentAccount.CalculateInterest();
            }
            else if (currentAccount is OmniAccount omniAccount)
            {
                result = omniAccount.CalculateInterest();
            }
            else
            {
                result = "Interest not applicable for this account type.";
            }

            LogTransaction(result);
            RefreshAccountDisplay();
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            lstHistory.Items.Clear();
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

        private void SwitchUser(int index)
        {
            var customers = customerController.GetAllCustomers();
            if (index < 0 || index >= customers.Count)
            {
                index = 0;
            }
            currentUser = customers[index];

            if (currentUser.IsStaff)
            {
                lblUserInfo.Text = $"**STAFF BENEFITS ACTIVE** - {currentUser.Name}";
                lblUserInfo.ForeColor = Gold;
            }
            else
            {
                lblUserInfo.Text = $"{currentUser.Name} - #{currentUser.CustomerNumber} - {currentUser.ContactDetails}";
                lblUserInfo.ForeColor = TextGray;
            }

            RebuildAccountTabs();
            SelectAccount(0);
        }

        private void SelectAccount(int index)
        {
            if (currentUser == null)
            {
                var customers = customerController.GetAllCustomers();
                if (customers.Count > 0)
                {
                    currentUser = customers[0];
                }
                else
                {
                    return;
                }
            }

            if (currentUser.Accounts.Count == 0)
            {
                return;
            }

            if (index < 0 || index >= currentUser.Accounts.Count)
            {
                index = 0;
            }

            currentAccount = currentUser.Accounts[index];

            for (int i = 0; i < navButtons.Count; i++)
            {
                bool isActive = (i == index);
                navIndicators[i].Visible = isActive;
                navButtons[i].ForeColor = isActive ? Color.White : TextGray;
                navButtons[i].Font = new Font("Segoe UI", 9.5f, isActive ? FontStyle.Bold : FontStyle.Regular);
            }

            btnCalcInterest.Visible = (currentAccount is InvestmentAccount) || (currentAccount is OmniAccount);
            RefreshAccountDisplay();
        }

        private void RefreshAccountDisplay()
        {
            lblAccTitle.Text = $"{currentAccount.AccountName} - Account ID: {currentAccount.AccountId}";
            lblBalance.Text = currentAccount.Balance.ToString("C2");
            lblBalance.ForeColor = currentAccount.Balance < 0 ? ErrorRed : Gold;

            if (currentAccount is EverydayAccount)
            {
                lblAccDetails.Text = "No interest - No overdraft - No transaction fees";
            }
            else if (currentAccount is InvestmentAccount inv)
            {
                string staffNote = currentUser.IsStaff ? "  (staff rate: $1.25)" : "";
                lblAccDetails.Text = $"Interest Rate: {inv.InterestRate:P1} - No overdraft"
                                   + $" - Failed transaction fee: $2.50{staffNote}";
            }
            else if (currentAccount is OmniAccount omni)
            {
                string staffNote = currentUser.IsStaff ? "  (staff rate: $2.50)" : "";
                lblAccDetails.Text = $"Interest Rate: 4.00% on balances > $1,000"
                                   + $" - Overdraft limit: {omni.OverdraftLimit:C0}"
                                   + $" - Failed fee: $5.00{staffNote}";
            }
        }

        private (bool success, decimal amount) TryParseAmount()
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount) && amount > 0)
            {
                return (true, amount);
            }

            MessageBox.Show(
                "Please enter a valid positive amount.",
                "Invalid Input",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return (false, 0);
        }

        private void LogTransaction(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string entry = $"[{timestamp}]  {currentAccount.AccountName,-22}  {message}";
            lstHistory.Items.Insert(0, entry);
        }
    }
}
