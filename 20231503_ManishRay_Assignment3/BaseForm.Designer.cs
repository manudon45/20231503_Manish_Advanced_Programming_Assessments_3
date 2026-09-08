namespace _20231503_ManishRay_Assignment3
{
    partial class BaseForm
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

        // Shared branding (navy background, white text, Segoe UI base font, DPI-aware scaling)
        // that every derived form inherits.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SuspendLayout();
            //
            // BaseForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 27, 53);
            ClientSize = new Size(440, 300);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            Name = "BaseForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ManishRayyy Bank";
            ResumeLayout(false);
        }
    }
}
