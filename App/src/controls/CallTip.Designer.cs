namespace System.Windows.Forms
{
    partial class CallTip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.text = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.text.Location = new System.Drawing.Point(2, 2);
            this.text.Margin = new System.Windows.Forms.Padding(2);
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.Size = new System.Drawing.Size(263, 61);
            this.text.TabIndex = 100;
            this.text.TabStop = false;
            this.text.Text = "";
            this.text.WordWrap = false;
            this.text.MouseEnter += new System.EventHandler(this.text_MouseEnter);
            // 
            // CallTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 65);
            this.Controls.Add(this.text);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CallTip";
            this.Opacity = 0.9D;
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowInTaskbar = false;
            this.Text = "CallTip";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox text;
    }
}