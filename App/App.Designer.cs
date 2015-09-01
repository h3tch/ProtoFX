namespace gled
{
    partial class App
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glControl = new OpenTK.GLControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCompile = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.splitCode = new System.Windows.Forms.SplitContainer();
            this.codeText = new System.Windows.Forms.RichTextBox();
            this.codeError = new System.Windows.Forms.RichTextBox();
            this.tabDebugData = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCode.SuspendLayout();
            this.tabDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCode)).BeginInit();
            this.splitCode.Panel1.SuspendLayout();
            this.splitCode.Panel2.SuspendLayout();
            this.splitCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1776, 1018);
            this.splitContainer1.SplitterDistance = 1053;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(1053, 1018);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);
            this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseDown);
            this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseMove);
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
            this.glControl.Resize += new System.EventHandler(this.glControl_Resize);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(717, 1018);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCompile);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 961);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(709, 52);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnCompile
            // 
            this.btnCompile.Location = new System.Drawing.Point(4, 5);
            this.btnCompile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(112, 35);
            this.btnCompile.TabIndex = 0;
            this.btnCompile.Text = "Compile";
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCode);
            this.tabControl1.Controls.Add(this.tabDebug);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(711, 950);
            this.tabControl1.TabIndex = 1;
            // 
            // tabCode
            // 
            this.tabCode.Controls.Add(this.splitCode);
            this.tabCode.Location = new System.Drawing.Point(4, 29);
            this.tabCode.Name = "tabCode";
            this.tabCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabCode.Size = new System.Drawing.Size(703, 917);
            this.tabCode.TabIndex = 0;
            this.tabCode.Text = "Code";
            this.tabCode.UseVisualStyleBackColor = true;
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.tabDebugData);
            this.tabDebug.Location = new System.Drawing.Point(4, 29);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(703, 917);
            this.tabDebug.TabIndex = 1;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // splitCode
            // 
            this.splitCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCode.Location = new System.Drawing.Point(3, 3);
            this.splitCode.Margin = new System.Windows.Forms.Padding(0);
            this.splitCode.Name = "splitCode";
            this.splitCode.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCode.Panel1
            // 
            this.splitCode.Panel1.Controls.Add(this.codeText);
            this.splitCode.Panel1MinSize = 75;
            // 
            // splitCode.Panel2
            // 
            this.splitCode.Panel2.Controls.Add(this.codeError);
            this.splitCode.Panel2MinSize = 75;
            this.splitCode.Size = new System.Drawing.Size(697, 911);
            this.splitCode.SplitterDistance = 600;
            this.splitCode.TabIndex = 0;
            // 
            // codeText
            // 
            this.codeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeText.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeText.Location = new System.Drawing.Point(0, 0);
            this.codeText.Margin = new System.Windows.Forms.Padding(0);
            this.codeText.Name = "codeText";
            this.codeText.Size = new System.Drawing.Size(697, 600);
            this.codeText.TabIndex = 0;
            this.codeText.Text = "";
            this.codeText.WordWrap = false;
            // 
            // codeError
            // 
            this.codeError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeError.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeError.Location = new System.Drawing.Point(0, 0);
            this.codeError.Margin = new System.Windows.Forms.Padding(0);
            this.codeError.Name = "codeError";
            this.codeError.Size = new System.Drawing.Size(697, 307);
            this.codeError.TabIndex = 0;
            this.codeError.Text = "";
            this.codeError.WordWrap = false;
            // 
            // tabDebugData
            // 
            this.tabDebugData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDebugData.Location = new System.Drawing.Point(3, 3);
            this.tabDebugData.Name = "tabDebugData";
            this.tabDebugData.SelectedIndex = 0;
            this.tabDebugData.Size = new System.Drawing.Size(697, 911);
            this.tabDebugData.TabIndex = 0;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1776, 1018);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "App";
            this.Text = "GLED";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabCode.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            this.splitCode.Panel1.ResumeLayout(false);
            this.splitCode.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCode)).EndInit();
            this.splitCode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCompile;
        private OpenTK.GLControl glControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCode;
        private System.Windows.Forms.SplitContainer splitCode;
        private System.Windows.Forms.RichTextBox codeText;
        private System.Windows.Forms.RichTextBox codeError;
        private System.Windows.Forms.TabPage tabDebug;
        private System.Windows.Forms.TabControl tabDebugData;
    }
}