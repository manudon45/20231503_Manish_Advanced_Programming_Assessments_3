namespace _20231503_ManishRay_Assignment3
{
    partial class TransferForm
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
            pnlBrandRule = new Panel();
            lblBrandLogo = new Label();
            lblBrandName = new Label();
            lblBrandSub = new Label();
            lblCustHead = new Label();
            cmbCustomer = new ComboBox();
            divider = new Panel();
            lblFrom = new Label();
            cmbSource = new ComboBox();
            lblSourceBal = new Label();
            lblTo = new Label();
            cmbDestination = new ComboBox();
            lblDestBal = new Label();
            lblAmtHead = new Label();
            txtAmount = new TextBox();
            lblStaff = new Label();
            btnExecute = new Button();
            btnClose = new Button();
            lblResult = new Label();
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
            pnlBrand.Size = new Size(480, 64);
            pnlBrand.TabIndex = 0;
            //
            // pnlBrandRule
            //
            pnlBrandRule.BackColor = Color.FromArgb(201, 168, 76);
            pnlBrandRule.Dock = DockStyle.Bottom;
            pnlBrandRule.Location = new Point(0, 62);
            pnlBrandRule.Name = "pnlBrandRule";
            pnlBrandRule.Size = new Size(480, 2);
            pnlBrandRule.TabIndex = 0;
            //
            // lblBrandLogo
            //
            lblBrandLogo.BackColor = Color.FromArgb(201, 168, 76);
            lblBrandLogo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBrandLogo.ForeColor = Color.FromArgb(15, 27, 53);
            lblBrandLogo.Location = new Point(16, 12);
            lblBrandLogo.Name = "lblBrandLogo";
            lblBrandLogo.Size = new Size(40, 40);
            lblBrandLogo.TabIndex = 0;
            lblBrandLogo.Text = "MB";
            lblBrandLogo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblBrandName
            //
            lblBrandName.BackColor = Color.Transparent;
            lblBrandName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBrandName.ForeColor = Color.White;
            lblBrandName.Location = new Point(66, 10);
            lblBrandName.Name = "lblBrandName";
            lblBrandName.Size = new Size(400, 24);
            lblBrandName.TabIndex = 1;
            lblBrandName.Text = "ManishRayyy Bank";
            //
            // lblBrandSub
            //
            lblBrandSub.BackColor = Color.Transparent;
            lblBrandSub.Font = new Font("Segoe UI", 8F);
            lblBrandSub.ForeColor = Color.FromArgb(201, 168, 76);
            lblBrandSub.Location = new Point(68, 36);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Size = new Size(400, 16);
            lblBrandSub.TabIndex = 2;
            lblBrandSub.Text = "Intra-Account Transfer  -  move funds between your own accounts";
            //
            // lblCustHead
            //
            lblCustHead.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCustHead.ForeColor = Color.FromArgb(160, 180, 210);
            lblCustHead.Location = new Point(24, 78);
            lblCustHead.Name = "lblCustHead";
            lblCustHead.Size = new Size(432, 16);
            lblCustHead.TabIndex = 1;
            lblCustHead.Text = "CUSTOMER PROFILE";
            //
            // cmbCustomer
            //
            cmbCustomer.BackColor = Color.FromArgb(40, 60, 100);
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustomer.FlatStyle = FlatStyle.Flat;
            cmbCustomer.Font = new Font("Segoe UI", 9.5F);
            cmbCustomer.ForeColor = Color.White;
            cmbCustomer.Location = new Point(24, 96);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(432, 26);
            cmbCustomer.TabIndex = 2;
            cmbCustomer.SelectedIndexChanged += cmbCustomer_SelectedIndexChanged;
            //
            // divider
            //
            divider.BackColor = Color.FromArgb(35, 52, 88);
            divider.Location = new Point(24, 136);
            divider.Name = "divider";
            divider.Size = new Size(432, 1);
            divider.TabIndex = 3;
            //
            // lblFrom
            //
            lblFrom.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblFrom.ForeColor = Color.FromArgb(160, 180, 210);
            lblFrom.Location = new Point(24, 150);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(432, 16);
            lblFrom.TabIndex = 4;
            lblFrom.Text = "TRANSFER FROM  (SOURCE)";
            //
            // cmbSource
            //
            cmbSource.BackColor = Color.FromArgb(40, 60, 100);
            cmbSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSource.FlatStyle = FlatStyle.Flat;
            cmbSource.Font = new Font("Segoe UI", 9.5F);
            cmbSource.ForeColor = Color.White;
            cmbSource.Location = new Point(24, 168);
            cmbSource.Name = "cmbSource";
            cmbSource.Size = new Size(432, 26);
            cmbSource.TabIndex = 5;
            cmbSource.SelectedIndexChanged += cmbSource_SelectedIndexChanged;
            //
            // lblSourceBal
            //
            lblSourceBal.Font = new Font("Segoe UI", 8F);
            lblSourceBal.ForeColor = Color.FromArgb(201, 168, 76);
            lblSourceBal.Location = new Point(24, 198);
            lblSourceBal.Name = "lblSourceBal";
            lblSourceBal.Size = new Size(432, 16);
            lblSourceBal.TabIndex = 6;
            //
            // lblTo
            //
            lblTo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTo.ForeColor = Color.FromArgb(160, 180, 210);
            lblTo.Location = new Point(24, 226);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(432, 16);
            lblTo.TabIndex = 7;
            lblTo.Text = "TRANSFER TO  (DESTINATION)";
            //
            // cmbDestination
            //
            cmbDestination.BackColor = Color.FromArgb(40, 60, 100);
            cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDestination.FlatStyle = FlatStyle.Flat;
            cmbDestination.Font = new Font("Segoe UI", 9.5F);
            cmbDestination.ForeColor = Color.White;
            cmbDestination.Location = new Point(24, 244);
            cmbDestination.Name = "cmbDestination";
            cmbDestination.Size = new Size(432, 26);
            cmbDestination.TabIndex = 8;
            cmbDestination.SelectedIndexChanged += cmbDestination_SelectedIndexChanged;
            //
            // lblDestBal
            //
            lblDestBal.Font = new Font("Segoe UI", 8F);
            lblDestBal.ForeColor = Color.FromArgb(201, 168, 76);
            lblDestBal.Location = new Point(24, 274);
            lblDestBal.Name = "lblDestBal";
            lblDestBal.Size = new Size(432, 16);
            lblDestBal.TabIndex = 9;
            //
            // lblAmtHead
            //
            lblAmtHead.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblAmtHead.ForeColor = Color.FromArgb(160, 180, 210);
            lblAmtHead.Location = new Point(24, 302);
            lblAmtHead.Name = "lblAmtHead";
            lblAmtHead.Size = new Size(432, 16);
            lblAmtHead.TabIndex = 10;
            lblAmtHead.Text = "AMOUNT  ($NZD)";
            //
            // txtAmount
            //
            txtAmount.BackColor = Color.FromArgb(40, 60, 100);
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.Font = new Font("Segoe UI", 10F);
            txtAmount.ForeColor = Color.White;
            txtAmount.Location = new Point(24, 320);
            txtAmount.Name = "txtAmount";
            txtAmount.PlaceholderText = "0.00";
            txtAmount.Size = new Size(200, 23);
            txtAmount.TabIndex = 11;
            txtAmount.KeyPress += txtAmount_KeyPress;
            //
            // lblStaff
            //
            lblStaff.Font = new Font("Segoe UI", 7.5F, FontStyle.Italic);
            lblStaff.ForeColor = Color.FromArgb(110, 130, 170);
            lblStaff.Location = new Point(24, 356);
            lblStaff.Name = "lblStaff";
            lblStaff.Size = new Size(432, 16);
            lblStaff.TabIndex = 12;
            //
            // btnExecute
            //
            btnExecute.BackColor = Color.FromArgb(46, 204, 113);
            btnExecute.Cursor = Cursors.Hand;
            btnExecute.FlatAppearance.BorderSize = 0;
            btnExecute.FlatStyle = FlatStyle.Flat;
            btnExecute.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnExecute.ForeColor = Color.FromArgb(15, 27, 53);
            btnExecute.Location = new Point(24, 384);
            btnExecute.Name = "btnExecute";
            btnExecute.Size = new Size(200, 38);
            btnExecute.TabIndex = 13;
            btnExecute.Text = "EXECUTE TRANSFER";
            btnExecute.UseVisualStyleBackColor = false;
            btnExecute.Click += btnExecute_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.FromArgb(40, 60, 100);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(236, 384);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(90, 38);
            btnClose.TabIndex = 14;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // lblResult
            //
            lblResult.Font = new Font("Segoe UI", 8F);
            lblResult.ForeColor = Color.FromArgb(160, 180, 210);
            lblResult.Location = new Point(24, 432);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(432, 44);
            lblResult.TabIndex = 15;
            //
            // TransferForm
            //
            AcceptButton = btnExecute;
            CancelButton = btnClose;
            ClientSize = new Size(480, 486);
            Controls.Add(lblResult);
            Controls.Add(btnClose);
            Controls.Add(btnExecute);
            Controls.Add(lblStaff);
            Controls.Add(txtAmount);
            Controls.Add(lblAmtHead);
            Controls.Add(lblDestBal);
            Controls.Add(cmbDestination);
            Controls.Add(lblTo);
            Controls.Add(lblSourceBal);
            Controls.Add(cmbSource);
            Controls.Add(lblFrom);
            Controls.Add(divider);
            Controls.Add(cmbCustomer);
            Controls.Add(lblCustHead);
            Controls.Add(pnlBrand);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TransferForm";
            Text = "Intra-Account Transfer";
            pnlBrand.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel pnlBrand;
        private Panel pnlBrandRule;
        private Label lblBrandLogo;
        private Label lblBrandName;
        private Label lblBrandSub;
        private Label lblCustHead;
        private ComboBox cmbCustomer;
        private Panel divider;
        private Label lblFrom;
        private ComboBox cmbSource;
        private Label lblSourceBal;
        private Label lblTo;
        private ComboBox cmbDestination;
        private Label lblDestBal;
        private Label lblAmtHead;
        private TextBox txtAmount;
        private Label lblStaff;
        private Button btnExecute;
        private Button btnClose;
        private Label lblResult;
    }
}
