#nullable enable
namespace _20231503_ManishRay_Assignment3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer? components = null;

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
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 27, 53);
            ClientSize = new Size(980, 720);
            ForeColor = Color.White;
            MinimumSize = new Size(900, 600);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManishRayy Bank Account Management";
            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
        }
    }
}
