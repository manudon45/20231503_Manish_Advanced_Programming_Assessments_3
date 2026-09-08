namespace _20231503_ManishRay_Assignment3
{
    partial class AddAccountForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlBrand = new Panel();
            lblBrandLogo = new Label();
            lblBrandName = new Label();
            lblBrandSub = new Label();
            pnlBrandRule = new Panel();
            lblHead = new Label();
            lblType = new Label();
            cmbType = new ComboBox();
            lblInit = new Label();
            txtInitial = new TextBox();
            lblExtra = new Label();
            txtExtra = new TextBox();
            btnCreate = new Button();
            btnCancel = new Button();
            lblHint = new Label();
            pnlBrand.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBrand
            // 
            pnlBrand.BackColor = Color.FromArgb(10, 20, 42);
            pnlBrand.Controls.Add(lblBrandLogo);
            pnlBrand.Controls.Add(lblBrandName);
            pnlBrand.Controls.Add(lblBrandSub);
            pnlBrand.Controls.Add(pnlBrandRule);
            pnlBrand.Dock = DockStyle.Top;
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Size = new Size(440, 64);
            pnlBrand.TabIndex = 0;
            // 
            // lblBrandLogo
            // 
            lblBrandLogo.BackColor = Color.FromArgb(201, 168, 76);
            lblBrandLogo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBrandLogo.ForeColor = Color.FromArgb(15, 27, 53);
            lblBrandLogo.Location = new Point(16, 12);
            lblBrandLogo.Name = "lblBrandLogo";
            lblBrandLogo.Size = new Size(46, 40);
            lblBrandLogo.TabIndex = 1;
            lblBrandLogo.Text = "MB";
            lblBrandLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblBrandLogo.Click += lblBrandLogo_Click;
            // 
            // lblBrandName
            // 
            lblBrandName.BackColor = Color.Transparent;
            lblBrandName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBrandName.ForeColor = Color.White;
            lblBrandName.Location = new Point(66, 10);
            lblBrandName.Name = "lblBrandName";
            lblBrandName.Size = new Size(360, 24);
            lblBrandName.TabIndex = 2;
            lblBrandName.Text = "ManishRayyy Bank";
            // 
            // lblBrandSub
            // 
            lblBrandSub.BackColor = Color.Transparent;
            lblBrandSub.Font = new Font("Segoe UI", 8F);
            lblBrandSub.ForeColor = Color.FromArgb(201, 168, 76);
            lblBrandSub.Location = new Point(68, 36);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Size = new Size(360, 16);
            lblBrandSub.TabIndex = 3;
            lblBrandSub.Text = "Add a new account to a customer profile";
            // 
            // pnlBrandRule
            // 
            pnlBrandRule.BackColor = Color.FromArgb(201, 168, 76);
            pnlBrandRule.Dock = DockStyle.Bottom;
            pnlBrandRule.Location = new Point(0, 62);
            pnlBrandRule.Name = "pnlBrandRule";
            pnlBrandRule.Size = new Size(440, 2);
            pnlBrandRule.TabIndex = 0;
            // 
            // lblHead
            // 
            lblHead.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHead.ForeColor = Color.FromArgb(201, 168, 76);
            lblHead.Location = new Point(20, 80);
            lblHead.Name = "lblHead";
            lblHead.Size = new Size(400, 22);
            lblHead.TabIndex = 1;
            lblHead.Text = "NEW ACCOUNT";
            // 
            // lblType
            // 
            lblType.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblType.ForeColor = Color.FromArgb(160, 180, 210);
            lblType.Location = new Point(20, 120);
            lblType.Name = "lblType";
            lblType.Size = new Size(150, 20);
            lblType.TabIndex = 2;
            lblType.Text = "Account Type:";
            // 
            // cmbType
            // 
            cmbType.BackColor = Color.FromArgb(40, 60, 100);
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.FlatStyle = FlatStyle.Flat;
            cmbType.Font = new Font("Segoe UI", 9.5F);
            cmbType.ForeColor = Color.White;
            cmbType.Items.AddRange(new object[] { "Everyday Account", "Investment Account", "Omni Account" });
            cmbType.Location = new Point(170, 117);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(240, 25);
            cmbType.TabIndex = 3;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            // 
            // lblInit
            // 
            lblInit.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblInit.ForeColor = Color.FromArgb(160, 180, 210);
            lblInit.Location = new Point(20, 160);
            lblInit.Name = "lblInit";
            lblInit.Size = new Size(150, 20);
            lblInit.TabIndex = 4;
            lblInit.Text = "Initial Balance ($):";
            // 
            // txtInitial
            // 
            txtInitial.BackColor = Color.FromArgb(40, 60, 100);
            txtInitial.BorderStyle = BorderStyle.FixedSingle;
            txtInitial.Font = new Font("Segoe UI", 10F);
            txtInitial.ForeColor = Color.White;
            txtInitial.Location = new Point(170, 157);
            txtInitial.Name = "txtInitial";
            txtInitial.PlaceholderText = "0.00";
            txtInitial.Size = new Size(240, 25);
            txtInitial.TabIndex = 5;
            // 
            // lblExtra
            // 
            lblExtra.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblExtra.ForeColor = Color.FromArgb(160, 180, 210);
            lblExtra.Location = new Point(20, 200);
            lblExtra.Name = "lblExtra";
            lblExtra.Size = new Size(150, 20);
            lblExtra.TabIndex = 6;
            lblExtra.Text = "Interest Rate:";
            // 
            // txtExtra
            // 
            txtExtra.BackColor = Color.FromArgb(40, 60, 100);
            txtExtra.BorderStyle = BorderStyle.FixedSingle;
            txtExtra.Font = new Font("Segoe UI", 10F);
            txtExtra.ForeColor = Color.White;
            txtExtra.Location = new Point(170, 197);
            txtExtra.Name = "txtExtra";
            txtExtra.Size = new Size(240, 25);
            txtExtra.TabIndex = 7;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(46, 204, 113);
            btnCreate.Cursor = Cursors.Hand;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnCreate.ForeColor = Color.FromArgb(15, 27, 53);
            btnCreate.Location = new Point(170, 254);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(160, 36);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "CREATE ACCOUNT";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(40, 60, 100);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(338, 254);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(72, 36);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "CANCEL";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblHint
            // 
            lblHint.Font = new Font("Segoe UI", 7.5F);
            lblHint.ForeColor = Color.FromArgb(160, 180, 210);
            lblHint.Location = new Point(20, 304);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(400, 52);
            lblHint.TabIndex = 10;
            lblHint.Text = "Everyday accounts carry no interest, fees or overdraft.\r\nInvestment: enter an interest rate (e.g. 0.05).\r\nOmni: enter an overdraft limit (e.g. 500.00).";
            // 
            // AddAccountForm
            // 
            AcceptButton = btnCreate;
            AutoScaleDimensions = new SizeF(7F, 15F);
            CancelButton = btnCancel;
            ClientSize = new Size(440, 372);
            Controls.Add(lblHint);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            Controls.Add(txtExtra);
            Controls.Add(lblExtra);
            Controls.Add(txtInitial);
            Controls.Add(lblInit);
            Controls.Add(cmbType);
            Controls.Add(lblType);
            Controls.Add(lblHead);
            Controls.Add(pnlBrand);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddAccountForm";
            Text = "Add New Account";
            pnlBrand.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel pnlBrand;
        private Panel pnlBrandRule;
        private Label lblBrandLogo;
        private Label lblBrandName;
        private Label lblBrandSub;
        private Label lblHead;
        private Label lblType;
        private ComboBox cmbType;
        private Label lblInit;
        private TextBox txtInitial;
        private Label lblExtra;
        private TextBox txtExtra;
        private Button btnCreate;
        private Button btnCancel;
        private Label lblHint;
    }
}
