namespace _20231503_ManishRay_Assignment3
{
    partial class CustomerManagementForm
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
            pnlMain = new Panel();
            split = new TableLayoutPanel();
            pnlLeft = new Panel();
            lblListHead = new Label();
            pnlRight = new Panel();
            pnlFields = new Panel();
            lblId = new Label();
            txtCustId = new TextBox();
            lblName = new Label();
            txtCustName = new TextBox();
            lblContact = new Label();
            txtCustContact = new TextBox();
            lblRole = new Label();
            cmbCustRole = new ComboBox();
            lblStaffId = new Label();
            txtCustStaffId = new TextBox();
            lblEv = new Label();
            txtEverydayBal = new TextBox();
            lblInv = new Label();
            txtInvBal = new TextBox();
            lblOmni = new Label();
            txtOmniBal = new TextBox();
            lblOd = new Label();
            txtOmniOverdraft = new TextBox();
            pnlBtns = new Panel();
            btnAddCust = new Button();
            btnUpdateCust = new Button();
            btnDeleteCust = new Button();
            btnClearCust = new Button();
            lblFormHead = new Label();
            lstCustomers = new ListBox();
            pnlMain.SuspendLayout();
            split.SuspendLayout();
            pnlLeft.SuspendLayout();
            pnlRight.SuspendLayout();
            pnlFields.SuspendLayout();
            pnlBtns.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(15, 27, 53);
            pnlMain.Controls.Add(split);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(15);
            pnlMain.Size = new Size(920, 580);
            pnlMain.TabIndex = 0;
            // 
            // split
            // 
            split.ColumnCount = 2;
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            split.Controls.Add(pnlLeft, 0, 0);
            split.Controls.Add(pnlRight, 1, 0);
            split.Dock = DockStyle.Fill;
            split.Location = new Point(15, 15);
            split.Name = "split";
            split.RowCount = 1;
            split.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            split.Size = new Size(890, 550);
            split.TabIndex = 0;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(22, 38, 68);
            pnlLeft.Controls.Add(lstCustomers);
            pnlLeft.Controls.Add(lblListHead);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(3, 3);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(12);
            pnlLeft.Size = new Size(394, 544);
            pnlLeft.TabIndex = 0;
            // 
            // lblListHead
            // 
            lblListHead.Dock = DockStyle.Top;
            lblListHead.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblListHead.ForeColor = Color.FromArgb(201, 168, 76);
            lblListHead.Location = new Point(12, 12);
            lblListHead.Name = "lblListHead";
            lblListHead.Size = new Size(370, 28);
            lblListHead.TabIndex = 1;
            lblListHead.Text = "REGISTERED CUSTOMERS (MVC MODEL)";
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.FromArgb(22, 38, 68);
            pnlRight.Controls.Add(pnlFields);
            pnlRight.Controls.Add(pnlBtns);
            pnlRight.Controls.Add(lblFormHead);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(403, 3);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(12);
            pnlRight.Size = new Size(484, 544);
            pnlRight.TabIndex = 1;
            // 
            // pnlFields
            // 
            pnlFields.Controls.Add(lblId);
            pnlFields.Controls.Add(txtCustId);
            pnlFields.Controls.Add(lblName);
            pnlFields.Controls.Add(txtCustName);
            pnlFields.Controls.Add(lblContact);
            pnlFields.Controls.Add(txtCustContact);
            pnlFields.Controls.Add(lblRole);
            pnlFields.Controls.Add(cmbCustRole);
            pnlFields.Controls.Add(lblStaffId);
            pnlFields.Controls.Add(txtCustStaffId);
            pnlFields.Controls.Add(lblEv);
            pnlFields.Controls.Add(txtEverydayBal);
            pnlFields.Controls.Add(lblInv);
            pnlFields.Controls.Add(txtInvBal);
            pnlFields.Controls.Add(lblOmni);
            pnlFields.Controls.Add(txtOmniBal);
            pnlFields.Controls.Add(lblOd);
            pnlFields.Controls.Add(txtOmniOverdraft);
            pnlFields.Dock = DockStyle.Fill;
            pnlFields.Location = new Point(12, 40);
            pnlFields.Name = "pnlFields";
            pnlFields.Size = new Size(460, 450);
            pnlFields.TabIndex = 0;
            // 
            // lblId
            // 
            lblId.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(160, 180, 210);
            lblId.Location = new Point(5, 8);
            lblId.Name = "lblId";
            lblId.Size = new Size(130, 20);
            lblId.TabIndex = 0;
            lblId.Text = "Customer ID:";
            // 
            // txtCustId
            // 
            txtCustId.BackColor = Color.FromArgb(27, 42, 74);
            txtCustId.BorderStyle = BorderStyle.FixedSingle;
            txtCustId.ForeColor = Color.FromArgb(160, 180, 210);
            txtCustId.Location = new Point(140, 5);
            txtCustId.Name = "txtCustId";
            txtCustId.ReadOnly = true;
            txtCustId.Size = new Size(260, 23);
            txtCustId.TabIndex = 1;
            txtCustId.Text = "(Auto Generated)";
            // 
            // lblName
            // 
            lblName.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(160, 180, 210);
            lblName.Location = new Point(5, 42);
            lblName.Name = "lblName";
            lblName.Size = new Size(130, 20);
            lblName.TabIndex = 2;
            lblName.Text = "Full Name:";
            // 
            // txtCustName
            // 
            txtCustName.BackColor = Color.FromArgb(40, 60, 100);
            txtCustName.BorderStyle = BorderStyle.FixedSingle;
            txtCustName.ForeColor = Color.White;
            txtCustName.Location = new Point(140, 39);
            txtCustName.Name = "txtCustName";
            txtCustName.Size = new Size(260, 23);
            txtCustName.TabIndex = 3;
            // 
            // lblContact
            // 
            lblContact.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblContact.ForeColor = Color.FromArgb(160, 180, 210);
            lblContact.Location = new Point(5, 76);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(130, 20);
            lblContact.TabIndex = 4;
            lblContact.Text = "Contact Details:";
            // 
            // txtCustContact
            // 
            txtCustContact.BackColor = Color.FromArgb(40, 60, 100);
            txtCustContact.BorderStyle = BorderStyle.FixedSingle;
            txtCustContact.ForeColor = Color.White;
            txtCustContact.Location = new Point(140, 73);
            txtCustContact.Name = "txtCustContact";
            txtCustContact.Size = new Size(260, 23);
            txtCustContact.TabIndex = 5;
            // 
            // lblRole
            // 
            lblRole.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblRole.ForeColor = Color.FromArgb(160, 180, 210);
            lblRole.Location = new Point(5, 110);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(130, 20);
            lblRole.TabIndex = 6;
            lblRole.Text = "Account Type:";
            // 
            // cmbCustRole
            // 
            cmbCustRole.BackColor = Color.FromArgb(40, 60, 100);
            cmbCustRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustRole.FlatStyle = FlatStyle.Flat;
            cmbCustRole.ForeColor = Color.White;
            cmbCustRole.Items.AddRange(new object[] { "Regular Customer", "Bank Staff" });
            cmbCustRole.Location = new Point(140, 107);
            cmbCustRole.Name = "cmbCustRole";
            cmbCustRole.Size = new Size(260, 23);
            cmbCustRole.TabIndex = 7;
            cmbCustRole.SelectedIndexChanged += cmbCustRole_SelectedIndexChanged;
            // 
            // lblStaffId
            // 
            lblStaffId.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblStaffId.ForeColor = Color.FromArgb(160, 180, 210);
            lblStaffId.Location = new Point(5, 144);
            lblStaffId.Name = "lblStaffId";
            lblStaffId.Size = new Size(130, 20);
            lblStaffId.TabIndex = 8;
            lblStaffId.Text = "Staff ID:";
            // 
            // txtCustStaffId
            // 
            txtCustStaffId.BackColor = Color.FromArgb(40, 60, 100);
            txtCustStaffId.BorderStyle = BorderStyle.FixedSingle;
            txtCustStaffId.Enabled = false;
            txtCustStaffId.ForeColor = Color.White;
            txtCustStaffId.Location = new Point(140, 141);
            txtCustStaffId.Name = "txtCustStaffId";
            txtCustStaffId.Size = new Size(260, 23);
            txtCustStaffId.TabIndex = 9;
            // 
            // lblEv
            // 
            lblEv.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblEv.ForeColor = Color.FromArgb(160, 180, 210);
            lblEv.Location = new Point(5, 178);
            lblEv.Name = "lblEv";
            lblEv.Size = new Size(130, 20);
            lblEv.TabIndex = 10;
            lblEv.Text = "Everyday Bal ($):";
            // 
            // txtEverydayBal
            // 
            txtEverydayBal.BackColor = Color.FromArgb(40, 60, 100);
            txtEverydayBal.BorderStyle = BorderStyle.FixedSingle;
            txtEverydayBal.ForeColor = Color.White;
            txtEverydayBal.Location = new Point(140, 175);
            txtEverydayBal.Name = "txtEverydayBal";
            txtEverydayBal.Size = new Size(260, 23);
            txtEverydayBal.TabIndex = 11;
            txtEverydayBal.Text = "500.00";
            // 
            // lblInv
            // 
            lblInv.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblInv.ForeColor = Color.FromArgb(160, 180, 210);
            lblInv.Location = new Point(5, 212);
            lblInv.Name = "lblInv";
            lblInv.Size = new Size(130, 20);
            lblInv.TabIndex = 12;
            lblInv.Text = "Investment Bal ($):";
            // 
            // txtInvBal
            // 
            txtInvBal.BackColor = Color.FromArgb(40, 60, 100);
            txtInvBal.BorderStyle = BorderStyle.FixedSingle;
            txtInvBal.ForeColor = Color.White;
            txtInvBal.Location = new Point(140, 209);
            txtInvBal.Name = "txtInvBal";
            txtInvBal.Size = new Size(260, 23);
            txtInvBal.TabIndex = 13;
            txtInvBal.Text = "1000.00";
            // 
            // lblOmni
            // 
            lblOmni.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblOmni.ForeColor = Color.FromArgb(160, 180, 210);
            lblOmni.Location = new Point(5, 246);
            lblOmni.Name = "lblOmni";
            lblOmni.Size = new Size(130, 20);
            lblOmni.TabIndex = 14;
            lblOmni.Text = "Omni Bal ($):";
            // 
            // txtOmniBal
            // 
            txtOmniBal.BackColor = Color.FromArgb(40, 60, 100);
            txtOmniBal.BorderStyle = BorderStyle.FixedSingle;
            txtOmniBal.ForeColor = Color.White;
            txtOmniBal.Location = new Point(140, 243);
            txtOmniBal.Name = "txtOmniBal";
            txtOmniBal.Size = new Size(260, 23);
            txtOmniBal.TabIndex = 15;
            txtOmniBal.Text = "750.00";
            // 
            // lblOd
            // 
            lblOd.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblOd.ForeColor = Color.FromArgb(160, 180, 210);
            lblOd.Location = new Point(5, 280);
            lblOd.Name = "lblOd";
            lblOd.Size = new Size(130, 20);
            lblOd.TabIndex = 16;
            lblOd.Text = "Omni Overdraft ($):";
            // 
            // txtOmniOverdraft
            // 
            txtOmniOverdraft.BackColor = Color.FromArgb(40, 60, 100);
            txtOmniOverdraft.BorderStyle = BorderStyle.FixedSingle;
            txtOmniOverdraft.ForeColor = Color.White;
            txtOmniOverdraft.Location = new Point(140, 277);
            txtOmniOverdraft.Name = "txtOmniOverdraft";
            txtOmniOverdraft.Size = new Size(260, 23);
            txtOmniOverdraft.TabIndex = 17;
            txtOmniOverdraft.Text = "500.00";
            // 
            // pnlBtns
            // 
            pnlBtns.Controls.Add(btnAddCust);
            pnlBtns.Controls.Add(btnUpdateCust);
            pnlBtns.Controls.Add(btnDeleteCust);
            pnlBtns.Controls.Add(btnClearCust);
            pnlBtns.Dock = DockStyle.Bottom;
            pnlBtns.Location = new Point(12, 490);
            pnlBtns.Name = "pnlBtns";
            pnlBtns.Size = new Size(460, 42);
            pnlBtns.TabIndex = 1;
            // 
            // btnAddCust
            // 
            btnAddCust.BackColor = Color.FromArgb(46, 204, 113);
            btnAddCust.Cursor = Cursors.Hand;
            btnAddCust.FlatAppearance.BorderSize = 0;
            btnAddCust.FlatStyle = FlatStyle.Flat;
            btnAddCust.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnAddCust.ForeColor = Color.FromArgb(15, 27, 53);
            btnAddCust.Location = new Point(5, 5);
            btnAddCust.Name = "btnAddCust";
            btnAddCust.Size = new Size(90, 32);
            btnAddCust.TabIndex = 0;
            btnAddCust.Text = "ADD";
            btnAddCust.UseVisualStyleBackColor = false;
            btnAddCust.Click += btnAddCust_Click;
            // 
            // btnUpdateCust
            // 
            btnUpdateCust.BackColor = Color.FromArgb(201, 168, 76);
            btnUpdateCust.Cursor = Cursors.Hand;
            btnUpdateCust.FlatAppearance.BorderSize = 0;
            btnUpdateCust.FlatStyle = FlatStyle.Flat;
            btnUpdateCust.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnUpdateCust.ForeColor = Color.FromArgb(15, 27, 53);
            btnUpdateCust.Location = new Point(100, 5);
            btnUpdateCust.Name = "btnUpdateCust";
            btnUpdateCust.Size = new Size(90, 32);
            btnUpdateCust.TabIndex = 1;
            btnUpdateCust.Text = "UPDATE";
            btnUpdateCust.UseVisualStyleBackColor = false;
            btnUpdateCust.Click += btnUpdateCust_Click;
            // 
            // btnDeleteCust
            // 
            btnDeleteCust.BackColor = Color.FromArgb(231, 76, 60);
            btnDeleteCust.Cursor = Cursors.Hand;
            btnDeleteCust.FlatAppearance.BorderSize = 0;
            btnDeleteCust.FlatStyle = FlatStyle.Flat;
            btnDeleteCust.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnDeleteCust.ForeColor = Color.White;
            btnDeleteCust.Location = new Point(195, 5);
            btnDeleteCust.Name = "btnDeleteCust";
            btnDeleteCust.Size = new Size(90, 32);
            btnDeleteCust.TabIndex = 2;
            btnDeleteCust.Text = "DELETE";
            btnDeleteCust.UseVisualStyleBackColor = false;
            btnDeleteCust.Click += btnDeleteCust_Click;
            // 
            // btnClearCust
            // 
            btnClearCust.BackColor = Color.FromArgb(40, 60, 100);
            btnClearCust.Cursor = Cursors.Hand;
            btnClearCust.FlatAppearance.BorderSize = 0;
            btnClearCust.FlatStyle = FlatStyle.Flat;
            btnClearCust.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnClearCust.ForeColor = Color.White;
            btnClearCust.Location = new Point(290, 5);
            btnClearCust.Name = "btnClearCust";
            btnClearCust.Size = new Size(80, 32);
            btnClearCust.TabIndex = 3;
            btnClearCust.Text = "CLEAR";
            btnClearCust.UseVisualStyleBackColor = false;
            btnClearCust.Click += btnClearCust_Click;
            // 
            // lblFormHead
            // 
            lblFormHead.Dock = DockStyle.Top;
            lblFormHead.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFormHead.ForeColor = Color.FromArgb(201, 168, 76);
            lblFormHead.Location = new Point(12, 12);
            lblFormHead.Name = "lblFormHead";
            lblFormHead.Size = new Size(460, 28);
            lblFormHead.TabIndex = 2;
            lblFormHead.Text = "CUSTOMER DETAILS & ACTIONS";
            lblFormHead.UseMnemonic = false;
            // 
            // lstCustomers
            // 
            lstCustomers.BackColor = Color.FromArgb(27, 42, 74);
            lstCustomers.BorderStyle = BorderStyle.FixedSingle;
            lstCustomers.Dock = DockStyle.Fill;
            lstCustomers.Font = new Font("Segoe UI", 9.5F);
            lstCustomers.ForeColor = Color.White;
            lstCustomers.Location = new Point(12, 40);
            lstCustomers.Name = "lstCustomers";
            lstCustomers.Size = new Size(370, 492);
            lstCustomers.TabIndex = 0;
            lstCustomers.SelectedIndexChanged += lstCustomers_SelectedIndexChanged;
            // 
            // CustomerManagementForm
            // 
            BackColor = Color.FromArgb(15, 27, 53);
            ClientSize = new Size(920, 580);
            Controls.Add(pnlMain);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CustomerManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ManishRayyy Bank - Customer Information Management";
            pnlMain.ResumeLayout(false);
            split.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            pnlFields.ResumeLayout(false);
            pnlFields.PerformLayout();
            pnlBtns.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMain = new System.Windows.Forms.Panel();
        private System.Windows.Forms.TableLayoutPanel split = new System.Windows.Forms.TableLayoutPanel();
        private System.Windows.Forms.Panel pnlLeft = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblListHead = new System.Windows.Forms.Label();
        private System.Windows.Forms.Panel pnlRight = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblFormHead = new System.Windows.Forms.Label();
        private System.Windows.Forms.Panel pnlFields = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblId = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox txtCustId = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label lblName = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox txtCustName = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label lblContact = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox txtCustContact = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label lblRole = new System.Windows.Forms.Label();
        private System.Windows.Forms.ComboBox cmbCustRole = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.Label lblStaffId = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox txtCustStaffId = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label lblEv = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox txtEverydayBal = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label lblInv = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox txtInvBal = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label lblOmni = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox txtOmniBal = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label lblOd = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox txtOmniOverdraft = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Panel pnlBtns = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Button btnAddCust = new System.Windows.Forms.Button();
        private System.Windows.Forms.Button btnUpdateCust = new System.Windows.Forms.Button();
        private System.Windows.Forms.Button btnDeleteCust = new System.Windows.Forms.Button();
        private System.Windows.Forms.Button btnClearCust = new System.Windows.Forms.Button();
        private ListBox lstCustomers;
    }
}
