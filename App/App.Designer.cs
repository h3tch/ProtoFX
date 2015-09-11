using System;

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
            this.splitRenderCoding = new System.Windows.Forms.SplitContainer();
            this.glControl = new OpenTK.GLControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.splitCodeError = new System.Windows.Forms.SplitContainer();
            this.tabCodeTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tabSource = new System.Windows.Forms.TabControl();
            this.codeError = new System.Windows.Forms.RichTextBox();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.tabData = new System.Windows.Forms.TabControl();
            this.tabDataImg = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.comboImg = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureImg = new System.Windows.Forms.PictureBox();
            this.tabDataBuf = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableBuf = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBuf = new System.Windows.Forms.ComboBox();
            this.comboBufType = new System.Windows.Forms.ComboBox();
            this.textBufDim = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderCoding)).BeginInit();
            this.splitRenderCoding.Panel1.SuspendLayout();
            this.splitRenderCoding.Panel2.SuspendLayout();
            this.splitRenderCoding.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCodeError)).BeginInit();
            this.splitCodeError.Panel1.SuspendLayout();
            this.splitCodeError.Panel2.SuspendLayout();
            this.splitCodeError.SuspendLayout();
            this.tabCodeTableLayout.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabDataImg.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImg)).BeginInit();
            this.tabDataBuf.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableBuf)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitRenderCoding
            // 
            this.splitRenderCoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRenderCoding.Location = new System.Drawing.Point(0, 0);
            this.splitRenderCoding.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitRenderCoding.Name = "splitRenderCoding";
            // 
            // splitRenderCoding.Panel1
            // 
            this.splitRenderCoding.Panel1.Controls.Add(this.glControl);
            // 
            // splitRenderCoding.Panel2
            // 
            this.splitRenderCoding.Panel2.Controls.Add(this.tabControl);
            this.splitRenderCoding.Size = new System.Drawing.Size(1776, 1018);
            this.splitRenderCoding.SplitterDistance = 1053;
            this.splitRenderCoding.SplitterWidth = 6;
            this.splitRenderCoding.TabIndex = 0;
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
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(717, 1018);
            this.tabControl.TabIndex = 1;
            // 
            // tabCode
            // 
            this.tabCode.Controls.Add(this.splitCodeError);
            this.tabCode.Location = new System.Drawing.Point(4, 31);
            this.tabCode.Name = "tabCode";
            this.tabCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabCode.Size = new System.Drawing.Size(709, 983);
            this.tabCode.TabIndex = 0;
            this.tabCode.Text = "Code";
            this.tabCode.UseVisualStyleBackColor = true;
            // 
            // splitCodeError
            // 
            this.splitCodeError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCodeError.Location = new System.Drawing.Point(3, 3);
            this.splitCodeError.Margin = new System.Windows.Forms.Padding(0);
            this.splitCodeError.Name = "splitCodeError";
            this.splitCodeError.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCodeError.Panel1
            // 
            this.splitCodeError.Panel1.Controls.Add(this.tabCodeTableLayout);
            this.splitCodeError.Panel1MinSize = 75;
            // 
            // splitCodeError.Panel2
            // 
            this.splitCodeError.Panel2.Controls.Add(this.codeError);
            this.splitCodeError.Panel2MinSize = 75;
            this.splitCodeError.Size = new System.Drawing.Size(703, 977);
            this.splitCodeError.SplitterDistance = 642;
            this.splitCodeError.TabIndex = 0;
            // 
            // tabCodeTableLayout
            // 
            this.tabCodeTableLayout.ColumnCount = 1;
            this.tabCodeTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabCodeTableLayout.Controls.Add(this.tabSource, 0, 0);
            this.tabCodeTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCodeTableLayout.Location = new System.Drawing.Point(0, 0);
            this.tabCodeTableLayout.Name = "tabCodeTableLayout";
            this.tabCodeTableLayout.RowCount = 1;
            this.tabCodeTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabCodeTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 642F));
            this.tabCodeTableLayout.Size = new System.Drawing.Size(703, 642);
            this.tabCodeTableLayout.TabIndex = 1;
            // 
            // tabSource
            // 
            this.tabSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSource.Location = new System.Drawing.Point(0, 3);
            this.tabSource.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tabSource.Name = "tabSource";
            this.tabSource.SelectedIndex = 0;
            this.tabSource.Size = new System.Drawing.Size(703, 639);
            this.tabSource.TabIndex = 0;
            // 
            // codeError
            // 
            this.codeError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeError.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeError.ForeColor = System.Drawing.SystemColors.HighlightText;
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
            this.tabDebug.Location = new System.Drawing.Point(4, 31);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(709, 983);
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
            this.tabData.Size = new System.Drawing.Size(703, 977);
            this.tabData.TabIndex = 0;
            // 
            // tabDataImg
            // 
            this.tabDataImg.Controls.Add(this.tableLayoutPanel2);
            this.tabDataImg.Location = new System.Drawing.Point(4, 31);
            this.tabDataImg.Name = "tabDataImg";
            this.tabDataImg.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataImg.Size = new System.Drawing.Size(695, 942);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(689, 936);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // comboImg
            // 
            this.comboImg.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboImg.FormattingEnabled = true;
            this.comboImg.Location = new System.Drawing.Point(3, 3);
            this.comboImg.Name = "comboImg";
            this.comboImg.Size = new System.Drawing.Size(683, 30);
            this.comboImg.TabIndex = 1;
            this.comboImg.SelectedIndexChanged += new System.EventHandler(this.comboImg_SelectedIndexChanged);
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
            // tabDataBuf
            // 
            this.tabDataBuf.Controls.Add(this.tableLayoutPanel3);
            this.tabDataBuf.Location = new System.Drawing.Point(4, 31);
            this.tabDataBuf.Name = "tabDataBuf";
            this.tabDataBuf.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataBuf.Size = new System.Drawing.Size(695, 942);
            this.tabDataBuf.TabIndex = 1;
            this.tabDataBuf.Text = "Buffers";
            this.tabDataBuf.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableBuf, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(689, 936);
            this.tableLayoutPanel3.TabIndex = 0;
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
            this.tableBuf.Size = new System.Drawing.Size(683, 894);
            this.tableBuf.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.comboBuf, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBufType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBufDim, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(689, 40);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // comboBuf
            // 
            this.comboBuf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBuf.FormattingEnabled = true;
            this.comboBuf.Location = new System.Drawing.Point(3, 3);
            this.comboBuf.Name = "comboBuf";
            this.comboBuf.Size = new System.Drawing.Size(407, 30);
            this.comboBuf.TabIndex = 0;
            this.comboBuf.SelectedIndexChanged += new System.EventHandler(this.comboBuf_SelectedIndexChanged);
            // 
            // comboBufType
            // 
            this.comboBufType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBufType.FormattingEnabled = true;
            this.comboBufType.Items.AddRange(new object[] {
            "byte",
            "short",
            "ushort",
            "int",
            "uint",
            "long",
            "ulong",
            "float",
            "double"});
            this.comboBufType.Location = new System.Drawing.Point(416, 3);
            this.comboBufType.Name = "comboBufType";
            this.comboBufType.Size = new System.Drawing.Size(131, 30);
            this.comboBufType.TabIndex = 1;
            this.comboBufType.SelectedIndexChanged += new System.EventHandler(this.comboBufType_SelectedIndexChanged);
            // 
            // textBufDim
            // 
            this.textBufDim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBufDim.Location = new System.Drawing.Point(553, 3);
            this.textBufDim.Name = "textBufDim";
            this.textBufDim.Size = new System.Drawing.Size(133, 28);
            this.textBufDim.TabIndex = 2;
            this.textBufDim.Text = "4";
            this.textBufDim.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBufDim_KeyUp);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1776, 1018);
            this.Controls.Add(this.splitRenderCoding);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "App";
            this.Text = "GLED";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.App_KeyUp);
            this.splitRenderCoding.Panel1.ResumeLayout(false);
            this.splitRenderCoding.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderCoding)).EndInit();
            this.splitRenderCoding.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabCode.ResumeLayout(false);
            this.splitCodeError.Panel1.ResumeLayout(false);
            this.splitCodeError.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCodeError)).EndInit();
            this.splitCodeError.ResumeLayout(false);
            this.tabCodeTableLayout.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tabDataImg.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImg)).EndInit();
            this.tabDataBuf.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableBuf)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitRenderCoding;
        private OpenTK.GLControl glControl;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCode;
        private System.Windows.Forms.SplitContainer splitCodeError;
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
        private System.Windows.Forms.TableLayoutPanel tabCodeTableLayout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBufType;
        private System.Windows.Forms.TextBox textBufDim;
        private System.Windows.Forms.TabControl tabSource;
    }
}