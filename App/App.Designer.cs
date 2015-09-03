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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.splitCode = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCompile = new System.Windows.Forms.Button();
            this.codeText = new System.Windows.Forms.RichTextBox();
            this.codeError = new System.Windows.Forms.RichTextBox();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.tabData = new System.Windows.Forms.TabControl();
            this.tabDataImg = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureImg = new System.Windows.Forms.PictureBox();
            this.comboImg = new System.Windows.Forms.ComboBox();
            this.tabDataBuf = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBuf = new System.Windows.Forms.ComboBox();
            this.tableBuf = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCode)).BeginInit();
            this.splitCode.Panel1.SuspendLayout();
            this.splitCode.Panel2.SuspendLayout();
            this.splitCode.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabDataImg.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImg)).BeginInit();
            this.tabDataBuf.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableBuf)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
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
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCode);
            this.tabControl.Controls.Add(this.tabDebug);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(717, 1018);
            this.tabControl.TabIndex = 1;
            // 
            // tabCode
            // 
            this.tabCode.Controls.Add(this.splitCode);
            this.tabCode.Location = new System.Drawing.Point(4, 29);
            this.tabCode.Name = "tabCode";
            this.tabCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabCode.Size = new System.Drawing.Size(709, 985);
            this.tabCode.TabIndex = 0;
            this.tabCode.Text = "Code";
            this.tabCode.UseVisualStyleBackColor = true;
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
            this.splitCode.Panel1.Controls.Add(this.tableLayoutPanel4);
            this.splitCode.Panel1MinSize = 75;
            // 
            // splitCode.Panel2
            // 
            this.splitCode.Panel2.Controls.Add(this.codeError);
            this.splitCode.Panel2MinSize = 75;
            this.splitCode.Size = new System.Drawing.Size(703, 979);
            this.splitCode.SplitterDistance = 644;
            this.splitCode.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.codeText, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(703, 644);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btnCompile
            // 
            this.btnCompile.Location = new System.Drawing.Point(3, 5);
            this.btnCompile.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(120, 35);
            this.btnCompile.TabIndex = 0;
            this.btnCompile.Text = "Compile";
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // codeText
            // 
            this.codeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeText.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeText.Location = new System.Drawing.Point(0, 45);
            this.codeText.Margin = new System.Windows.Forms.Padding(0);
            this.codeText.Name = "codeText";
            this.codeText.Size = new System.Drawing.Size(703, 599);
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
            this.codeError.Size = new System.Drawing.Size(703, 331);
            this.codeError.TabIndex = 0;
            this.codeError.Text = "";
            this.codeError.WordWrap = false;
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.tabData);
            this.tabDebug.Location = new System.Drawing.Point(4, 29);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(709, 985);
            this.tabDebug.TabIndex = 1;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.tabDataImg);
            this.tabData.Controls.Add(this.tabDataBuf);
            this.tabData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabData.Location = new System.Drawing.Point(3, 3);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(703, 979);
            this.tabData.TabIndex = 0;
            // 
            // tabDataImg
            // 
            this.tabDataImg.Controls.Add(this.tableLayoutPanel2);
            this.tabDataImg.Location = new System.Drawing.Point(4, 29);
            this.tabDataImg.Name = "tabDataImg";
            this.tabDataImg.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataImg.Size = new System.Drawing.Size(695, 946);
            this.tabDataImg.TabIndex = 0;
            this.tabDataImg.Text = "Images";
            this.tabDataImg.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.comboImg, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(689, 940);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pictureImg
            // 
            this.pictureImg.Location = new System.Drawing.Point(0, 0);
            this.pictureImg.Name = "pictureImg";
            this.pictureImg.Size = new System.Drawing.Size(410, 353);
            this.pictureImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureImg.TabIndex = 0;
            this.pictureImg.TabStop = false;
            this.pictureImg.Click += new System.EventHandler(this.pictureImg_Click);
            // 
            // comboImg
            // 
            this.comboImg.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboImg.FormattingEnabled = true;
            this.comboImg.Location = new System.Drawing.Point(3, 3);
            this.comboImg.Name = "comboImg";
            this.comboImg.Size = new System.Drawing.Size(683, 28);
            this.comboImg.TabIndex = 1;
            this.comboImg.SelectedIndexChanged += new System.EventHandler(this.comboImg_SelectedIndexChanged);
            // 
            // tabDataBuf
            // 
            this.tabDataBuf.Controls.Add(this.tableLayoutPanel3);
            this.tabDataBuf.Location = new System.Drawing.Point(4, 29);
            this.tabDataBuf.Name = "tabDataBuf";
            this.tabDataBuf.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataBuf.Size = new System.Drawing.Size(689, 878);
            this.tabDataBuf.TabIndex = 1;
            this.tabDataBuf.Text = "Buffers";
            this.tabDataBuf.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.comboBuf, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableBuf, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(683, 872);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // comboBuf
            // 
            this.comboBuf.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBuf.FormattingEnabled = true;
            this.comboBuf.Location = new System.Drawing.Point(3, 3);
            this.comboBuf.Name = "comboBuf";
            this.comboBuf.Size = new System.Drawing.Size(677, 28);
            this.comboBuf.TabIndex = 0;
            // 
            // tableBuf
            // 
            this.tableBuf.AllowUserToAddRows = false;
            this.tableBuf.AllowUserToDeleteRows = false;
            this.tableBuf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableBuf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBuf.Location = new System.Drawing.Point(3, 43);
            this.tableBuf.Name = "tableBuf";
            this.tableBuf.ReadOnly = true;
            this.tableBuf.RowTemplate.Height = 28;
            this.tableBuf.Size = new System.Drawing.Size(677, 826);
            this.tableBuf.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCompile);
            this.flowLayoutPanel1.Controls.Add(this.btnOpen);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(703, 45);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(129, 5);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(120, 35);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(255, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureImg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(683, 894);
            this.panel1.TabIndex = 2;
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
            this.tabControl.ResumeLayout(false);
            this.tabCode.ResumeLayout(false);
            this.splitCode.Panel1.ResumeLayout(false);
            this.splitCode.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCode)).EndInit();
            this.splitCode.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tabDataImg.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureImg)).EndInit();
            this.tabDataBuf.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableBuf)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnCompile;
        private OpenTK.GLControl glControl;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCode;
        private System.Windows.Forms.SplitContainer splitCode;
        private System.Windows.Forms.RichTextBox codeText;
        private System.Windows.Forms.RichTextBox codeError;
        private System.Windows.Forms.TabPage tabDebug;
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage tabDataImg;
        private System.Windows.Forms.TabPage tabDataBuf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureImg;
        private System.Windows.Forms.ComboBox comboImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox comboBuf;
        private System.Windows.Forms.DataGridView tableBuf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
    }
}