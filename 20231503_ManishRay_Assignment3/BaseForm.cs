namespace _20231503_ManishRay_Assignment3
{
    public class BaseForm : Form
    {
        protected static readonly Color NavyDark = Color.FromArgb(15, 27, 53);
        protected static readonly Color NavyMid = Color.FromArgb(27, 42, 74);
        protected static readonly Color NavyLight = Color.FromArgb(40, 60, 100);
        protected static readonly Color NavStrip = Color.FromArgb(10, 20, 42);
        protected static readonly Color Gold = Color.FromArgb(201, 168, 76);
        protected static readonly Color TextGray = Color.FromArgb(160, 180, 210);
        protected static readonly Color SuccessGreen = Color.FromArgb(46, 204, 113);
        protected static readonly Color ErrorRed = Color.FromArgb(231, 76, 60);
        protected static readonly Color CardBg = Color.FromArgb(22, 38, 68);

        protected const string BankName = "ManishRayyy Bank";
        protected const string BankTagline = "Smart Banking. Real Returns.";

        public BaseForm()
        {
            BackColor = NavyDark;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9f);
            StartPosition = FormStartPosition.CenterParent;
            AutoScaleMode = AutoScaleMode.Font;
        }

        protected Panel CreateBrandBar(string subtitle)
        {
            var bar = new Panel();
            bar.Dock = DockStyle.Top;
            bar.Height = 64;
            bar.BackColor = NavStrip;

            var logo = new Label();
            logo.Text = "MB";
            logo.Size = new Size(40, 40);
            logo.Location = new Point(16, 12);
            logo.BackColor = Gold;
            logo.ForeColor = NavyDark;
            logo.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
            logo.TextAlign = ContentAlignment.MiddleCenter;

            var lblName = new Label();
            lblName.Text = BankName;
            lblName.AutoSize = false;
            lblName.Location = new Point(66, 10);
            lblName.Size = new Size(420, 24);
            lblName.ForeColor = Color.White;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Segoe UI", 13f, FontStyle.Bold);

            var lblSub = new Label();
            lblSub.Text = subtitle;
            lblSub.AutoSize = false;
            lblSub.Location = new Point(68, 36);
            lblSub.Size = new Size(440, 16);
            lblSub.ForeColor = Gold;
            lblSub.BackColor = Color.Transparent;
            lblSub.Font = new Font("Segoe UI", 8f);

            var border = new Panel();
            border.Dock = DockStyle.Bottom;
            border.Height = 2;
            border.BackColor = Gold;

            bar.Controls.Add(logo);
            bar.Controls.Add(lblName);
            bar.Controls.Add(lblSub);
            bar.Controls.Add(border);
            return bar;
        }

        protected Button CreatePrimaryButton(string text) => StyleButton(text, SuccessGreen, NavyDark);

        protected Button CreateAccentButton(string text) => StyleButton(text, Gold, NavyDark);

        protected Button CreateNeutralButton(string text) => StyleButton(text, NavyLight, Color.White);

        protected Button CreateDangerButton(string text) => StyleButton(text, ErrorRed, Color.White);

        protected static Button StyleButton(string text, Color back, Color fore)
        {
            var b = new Button();
            b.Text = text;
            b.BackColor = back;
            b.ForeColor = fore;
            b.FlatStyle = FlatStyle.Flat;
            b.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
            b.Size = new Size(150, 34);
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        protected static Label CreateFieldLabel(string text)
        {
            var l = new Label();
            l.Text = text;
            l.ForeColor = TextGray;
            l.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            l.AutoSize = false;
            l.Size = new Size(150, 20);
            return l;
        }

        protected static TextBox CreateStyledTextBox()
        {
            var t = new TextBox();
            t.BackColor = NavyLight;
            t.ForeColor = Color.White;
            t.BorderStyle = BorderStyle.FixedSingle;
            t.Font = new Font("Segoe UI", 10f);
            return t;
        }

        protected static ComboBox CreateStyledComboBox()
        {
            var c = new ComboBox();
            c.DropDownStyle = ComboBoxStyle.DropDownList;
            c.FlatStyle = FlatStyle.Flat;
            c.BackColor = NavyLight;
            c.ForeColor = Color.White;
            c.Font = new Font("Segoe UI", 9.5f);
            return c;
        }
    }
}
