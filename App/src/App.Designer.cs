namespace App
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.layoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutTitleBar = new System.Windows.Forms.TableLayoutPanel();
            this.btnWindowClose = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnWindowMinimize = new System.Windows.Forms.Button();
            this.btnWindowMaximize = new System.Windows.Forms.Button();
            this.imgAppIcon = new System.Windows.Forms.PictureBox();
            this.splitRenderCoding = new System.Windows.Forms.SplitContainer();
            this.splitRenderOutput = new System.Windows.Forms.SplitContainer();
            this.glControl = new OpenTK.GraphicControl();
            this.tabOutput = new System.Windows.Forms.TabControlEx();
            this.tabCompile = new System.Windows.Forms.TabPage();
            this.output = new System.Windows.Forms.DataGridView();
            this.File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabDebugger = new System.Windows.Forms.TabPage();
            this.splitDebug = new System.Windows.Forms.SplitContainer();
            this.debugListView = new System.Windows.Forms.ListView();
            this.debugProperty = new System.Windows.Forms.PropertyGrid();
            this.tabControl = new System.Windows.Forms.TabControlEx();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.tabCodeTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.tabSource = new System.Windows.Forms.TabControlEx();
            this.toolStrip = new System.Windows.Forms.ToolStripEx();
            this.toolBtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolBtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnRun = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDbg = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnPick = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnComment = new System.Windows.Forms.ToolStripButton();
            this.toolBtnUncomment = new System.Windows.Forms.ToolStripButton();
            this.tabResources = new System.Windows.Forms.TabPage();
            this.tabData = new System.Windows.Forms.TabControlEx();
            this.tabDataImg = new System.Windows.Forms.TabPage();
            this.tableLayoutImages = new System.Windows.Forms.TableLayoutPanel();
            this.numImgLayer = new System.Windows.Forms.NumericUpDown();
            this.comboImg = new System.Windows.Forms.ComboBoxEx();
            this.panelImg = new System.Windows.Forms.Panel();
            this.pictureImg = new System.Windows.Forms.PictureBox();
            this.numImgLevel = new System.Windows.Forms.NumericUpDown();
            this.tabDataBuf = new System.Windows.Forms.TabPage();
            this.tableLayoutBufferDef = new System.Windows.Forms.TableLayoutPanel();
            this.tableBuf = new System.Windows.Forms.DataGridView();
            this.tableLayoutBuffers = new System.Windows.Forms.TableLayoutPanel();
            this.comboBuf = new System.Windows.Forms.ComboBoxEx();
            this.comboBufType = new System.Windows.Forms.ComboBoxEx();
            this.numBufDim = new System.Windows.Forms.NumericUpDown();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.comboProp = new System.Windows.Forms.ComboBoxEx();
            this.layoutMain.SuspendLayout();
            this.tableLayoutTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgAppIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderCoding)).BeginInit();
            this.splitRenderCoding.Panel1.SuspendLayout();
            this.splitRenderCoding.Panel2.SuspendLayout();
            this.splitRenderCoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderOutput)).BeginInit();
            this.splitRenderOutput.Panel1.SuspendLayout();
            this.splitRenderOutput.Panel2.SuspendLayout();
            this.splitRenderOutput.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabCompile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.output)).BeginInit();
            this.tabDebugger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDebug)).BeginInit();
            this.splitDebug.Panel1.SuspendLayout();
            this.splitDebug.Panel2.SuspendLayout();
            this.splitDebug.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCode.SuspendLayout();
            this.tabCodeTableLayout.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.RightToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabResources.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabDataImg.SuspendLayout();
            this.tableLayoutImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numImgLayer)).BeginInit();
            this.panelImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImgLevel)).BeginInit();
            this.tabDataBuf.SuspendLayout();
            this.tableLayoutBufferDef.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableBuf)).BeginInit();
            this.tableLayoutBuffers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBufDim)).BeginInit();
            this.tabProperties.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutMain
            // 
            this.layoutMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.layoutMain.ColumnCount = 1;
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.Controls.Add(this.tableLayoutTitleBar, 0, 0);
            this.layoutMain.Controls.Add(this.splitRenderCoding, 0, 1);
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.Location = new System.Drawing.Point(4, 5);
            this.layoutMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.RowCount = 2;
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.Size = new System.Drawing.Size(1466, 790);
            this.layoutMain.TabIndex = 1;
            // 
            // tableLayoutTitleBar
            // 
            this.tableLayoutTitleBar.ColumnCount = 5;
            this.tableLayoutTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutTitleBar.Controls.Add(this.btnWindowClose, 4, 0);
            this.tableLayoutTitleBar.Controls.Add(this.labelTitle, 1, 0);
            this.tableLayoutTitleBar.Controls.Add(this.btnWindowMinimize, 2, 0);
            this.tableLayoutTitleBar.Controls.Add(this.btnWindowMaximize, 3, 0);
            this.tableLayoutTitleBar.Controls.Add(this.imgAppIcon, 0, 0);
            this.tableLayoutTitleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutTitleBar.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutTitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutTitleBar.Name = "tableLayoutTitleBar";
            this.tableLayoutTitleBar.RowCount = 1;
            this.tableLayoutTitleBar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutTitleBar.Size = new System.Drawing.Size(1466, 34);
            this.tableLayoutTitleBar.TabIndex = 1;
            this.tableLayoutTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            this.tableLayoutTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
            // 
            // btnWindowClose
            // 
            this.btnWindowClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWindowClose.FlatAppearance.BorderSize = 0;
            this.btnWindowClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnWindowClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowClose.Image = global::App.Properties.Resources.Close;
            this.btnWindowClose.Location = new System.Drawing.Point(1420, 0);
            this.btnWindowClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowClose.Name = "btnWindowClose";
            this.btnWindowClose.Size = new System.Drawing.Size(46, 55);
            this.btnWindowClose.TabIndex = 2;
            this.btnWindowClose.UseVisualStyleBackColor = true;
            this.btnWindowClose.Click += new System.EventHandler(this.btnWindowClose_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Gray;
            this.labelTitle.Location = new System.Drawing.Point(51, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(107, 36);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "ProtoFX";
            this.labelTitle.UseCompatibleTextRendering = true;
            // 
            // btnWindowMinimize
            // 
            this.btnWindowMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWindowMinimize.FlatAppearance.BorderSize = 0;
            this.btnWindowMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnWindowMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowMinimize.Image = global::App.Properties.Resources.Minimize;
            this.btnWindowMinimize.Location = new System.Drawing.Point(1318, 0);
            this.btnWindowMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowMinimize.Name = "btnWindowMinimize";
            this.btnWindowMinimize.Size = new System.Drawing.Size(46, 55);
            this.btnWindowMinimize.TabIndex = 0;
            this.btnWindowMinimize.UseVisualStyleBackColor = true;
            this.btnWindowMinimize.Click += new System.EventHandler(this.btnWindowMinimize_Click);
            // 
            // btnWindowMaximize
            // 
            this.btnWindowMaximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWindowMaximize.FlatAppearance.BorderSize = 0;
            this.btnWindowMaximize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnWindowMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowMaximize.Image = global::App.Properties.Resources.Maximize;
            this.btnWindowMaximize.Location = new System.Drawing.Point(1369, 0);
            this.btnWindowMaximize.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowMaximize.Name = "btnWindowMaximize";
            this.btnWindowMaximize.Size = new System.Drawing.Size(46, 55);
            this.btnWindowMaximize.TabIndex = 1;
            this.btnWindowMaximize.UseVisualStyleBackColor = true;
            this.btnWindowMaximize.Click += new System.EventHandler(this.btnWindowMaximize_Click);
            // 
            // imgAppIcon
            // 
            this.imgAppIcon.Image = global::App.Properties.Resources.logo;
            this.imgAppIcon.Location = new System.Drawing.Point(0, 0);
            this.imgAppIcon.Margin = new System.Windows.Forms.Padding(0);
            this.imgAppIcon.Name = "imgAppIcon";
            this.imgAppIcon.Size = new System.Drawing.Size(46, 48);
            this.imgAppIcon.TabIndex = 3;
            this.imgAppIcon.TabStop = false;
            // 
            // splitRenderCoding
            // 
            this.splitRenderCoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRenderCoding.Location = new System.Drawing.Point(0, 34);
            this.splitRenderCoding.Margin = new System.Windows.Forms.Padding(0);
            this.splitRenderCoding.Name = "splitRenderCoding";
            // 
            // splitRenderCoding.Panel1
            // 
            this.splitRenderCoding.Panel1.Controls.Add(this.splitRenderOutput);
            // 
            // splitRenderCoding.Panel2
            // 
            this.splitRenderCoding.Panel2.Controls.Add(this.tabControl);
            this.splitRenderCoding.Size = new System.Drawing.Size(1466, 756);
            this.splitRenderCoding.SplitterDistance = 750;
            this.splitRenderCoding.SplitterWidth = 6;
            this.splitRenderCoding.TabIndex = 0;
            // 
            // splitRenderOutput
            // 
            this.splitRenderOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRenderOutput.Location = new System.Drawing.Point(0, 0);
            this.splitRenderOutput.Margin = new System.Windows.Forms.Padding(0);
            this.splitRenderOutput.Name = "splitRenderOutput";
            this.splitRenderOutput.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRenderOutput.Panel1
            // 
            this.splitRenderOutput.Panel1.Controls.Add(this.glControl);
            // 
            // splitRenderOutput.Panel2
            // 
            this.splitRenderOutput.Panel2.Controls.Add(this.tabOutput);
            this.splitRenderOutput.Size = new System.Drawing.Size(750, 756);
            this.splitRenderOutput.SplitterDistance = 497;
            this.splitRenderOutput.SplitterWidth = 5;
            this.splitRenderOutput.TabIndex = 1;
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(750, 497);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
            // 
            // tabOutput
            // 
            this.tabOutput.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabOutput.Controls.Add(this.tabCompile);
            this.tabOutput.Controls.Add(this.tabDebugger);
            this.tabOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutput.HighlightForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabOutput.Location = new System.Drawing.Point(0, 0);
            this.tabOutput.Margin = new System.Windows.Forms.Padding(0);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.SelectedIndex = 0;
            this.tabOutput.Size = new System.Drawing.Size(750, 254);
            this.tabOutput.TabIndex = 1;
            this.tabOutput.WorkspaceColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // tabCompile
            // 
            this.tabCompile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tabCompile.Controls.Add(this.output);
            this.tabCompile.ForeColor = System.Drawing.Color.DarkGray;
            this.tabCompile.Location = new System.Drawing.Point(4, 4);
            this.tabCompile.Margin = new System.Windows.Forms.Padding(0);
            this.tabCompile.Name = "tabCompile";
            this.tabCompile.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabCompile.Size = new System.Drawing.Size(742, 221);
            this.tabCompile.TabIndex = 0;
            this.tabCompile.Text = "Compiler Output";
            // 
            // output
            // 
            this.output.AllowUserToAddRows = false;
            this.output.AllowUserToDeleteRows = false;
            this.output.AllowUserToOrderColumns = true;
            this.output.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.output.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.output.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.output.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.output.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.File,
            this.Line,
            this.Description});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.output.DefaultCellStyle = dataGridViewCellStyle3;
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.EnableHeadersVisualStyles = false;
            this.output.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.output.Location = new System.Drawing.Point(3, 3);
            this.output.Margin = new System.Windows.Forms.Padding(0);
            this.output.Name = "output";
            this.output.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.output.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.output.RowHeadersVisible = false;
            this.output.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.output.Size = new System.Drawing.Size(736, 215);
            this.output.TabIndex = 1;
            this.output.DoubleClick += new System.EventHandler(this.output_DoubleClick);
            // 
            // File
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkGray;
            this.File.DefaultCellStyle = dataGridViewCellStyle2;
            this.File.FillWeight = 12F;
            this.File.HeaderText = "File";
            this.File.Name = "File";
            // 
            // Line
            // 
            this.Line.FillWeight = 8F;
            this.Line.HeaderText = "Line";
            this.Line.Name = "Line";
            this.Line.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.FillWeight = 80F;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // tabDebugger
            // 
            this.tabDebugger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tabDebugger.Controls.Add(this.splitDebug);
            this.tabDebugger.ForeColor = System.Drawing.Color.DarkGray;
            this.tabDebugger.Location = new System.Drawing.Point(4, 4);
            this.tabDebugger.Margin = new System.Windows.Forms.Padding(0);
            this.tabDebugger.Name = "tabDebugger";
            this.tabDebugger.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabDebugger.Size = new System.Drawing.Size(742, 221);
            this.tabDebugger.TabIndex = 1;
            this.tabDebugger.Text = "Debug Variables";
            // 
            // splitDebug
            // 
            this.splitDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDebug.Location = new System.Drawing.Point(3, 3);
            this.splitDebug.Margin = new System.Windows.Forms.Padding(0);
            this.splitDebug.Name = "splitDebug";
            // 
            // splitDebug.Panel1
            // 
            this.splitDebug.Panel1.Controls.Add(this.debugListView);
            // 
            // splitDebug.Panel2
            // 
            this.splitDebug.Panel2.Controls.Add(this.debugProperty);
            this.splitDebug.Size = new System.Drawing.Size(736, 215);
            this.splitDebug.SplitterDistance = 401;
            this.splitDebug.SplitterWidth = 6;
            this.splitDebug.TabIndex = 0;
            // 
            // debugListView
            // 
            this.debugListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.debugListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.debugListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugListView.Location = new System.Drawing.Point(0, 0);
            this.debugListView.Margin = new System.Windows.Forms.Padding(0);
            this.debugListView.Name = "debugListView";
            this.debugListView.Size = new System.Drawing.Size(401, 215);
            this.debugListView.TabIndex = 0;
            this.debugListView.UseCompatibleStateImageBehavior = false;
            // 
            // debugProperty
            // 
            this.debugProperty.CategoryForeColor = System.Drawing.Color.DarkGray;
            this.debugProperty.CategorySplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.debugProperty.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.debugProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugProperty.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.debugProperty.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.debugProperty.HelpForeColor = System.Drawing.Color.DarkGray;
            this.debugProperty.HelpVisible = false;
            this.debugProperty.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.debugProperty.Location = new System.Drawing.Point(0, 0);
            this.debugProperty.Margin = new System.Windows.Forms.Padding(0);
            this.debugProperty.Name = "debugProperty";
            this.debugProperty.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.debugProperty.SelectedItemWithFocusForeColor = System.Drawing.Color.LightGray;
            this.debugProperty.Size = new System.Drawing.Size(329, 215);
            this.debugProperty.TabIndex = 1;
            this.debugProperty.ToolbarVisible = false;
            this.debugProperty.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.debugProperty.ViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.debugProperty.ViewForeColor = System.Drawing.Color.DarkGray;
            this.debugProperty.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCode);
            this.tabControl.Controls.Add(this.tabResources);
            this.tabControl.Controls.Add(this.tabProperties);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.HighlightForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(710, 756);
            this.tabControl.TabIndex = 1;
            this.tabControl.WorkspaceColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // tabCode
            // 
            this.tabCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tabCode.Controls.Add(this.tabCodeTableLayout);
            this.tabCode.ForeColor = System.Drawing.Color.DarkGray;
            this.tabCode.Location = new System.Drawing.Point(4, 29);
            this.tabCode.Margin = new System.Windows.Forms.Padding(0);
            this.tabCode.Name = "tabCode";
            this.tabCode.Size = new System.Drawing.Size(702, 723);
            this.tabCode.TabIndex = 0;
            this.tabCode.Text = "Code";
            // 
            // tabCodeTableLayout
            // 
            this.tabCodeTableLayout.ColumnCount = 1;
            this.tabCodeTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabCodeTableLayout.Controls.Add(this.toolStripContainer, 0, 0);
            this.tabCodeTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCodeTableLayout.Location = new System.Drawing.Point(0, 0);
            this.tabCodeTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tabCodeTableLayout.Name = "tabCodeTableLayout";
            this.tabCodeTableLayout.RowCount = 1;
            this.tabCodeTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabCodeTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 712F));
            this.tabCodeTableLayout.Size = new System.Drawing.Size(702, 723);
            this.tabCodeTableLayout.TabIndex = 1;
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.toolStripContainer.ContentPanel.Controls.Add(this.tabSource);
            this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(665, 685);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripContainer.Name = "toolStripContainer";
            // 
            // toolStripContainer.RightToolStripPanel
            // 
            this.toolStripContainer.RightToolStripPanel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripContainer.RightToolStripPanel.Controls.Add(this.toolStrip);
            this.toolStripContainer.Size = new System.Drawing.Size(702, 723);
            this.toolStripContainer.TabIndex = 1;
            this.toolStripContainer.Text = "toolStripContainer2";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.BackColor = System.Drawing.Color.Transparent;
            // 
            // tabSource
            // 
            this.tabSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSource.HighlightForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabSource.Location = new System.Drawing.Point(0, 0);
            this.tabSource.Margin = new System.Windows.Forms.Padding(0);
            this.tabSource.Name = "tabSource";
            this.tabSource.Padding = new System.Drawing.Point(0, 0);
            this.tabSource.SelectedIndex = 0;
            this.tabSource.Size = new System.Drawing.Size(665, 685);
            this.tabSource.TabIndex = 0;
            this.tabSource.WorkspaceColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnClose,
            this.toolBtnNew,
            this.toolStripSeparator4,
            this.toolBtnOpen,
            this.toolBtnSave,
            this.toolBtnSaveAll,
            this.toolBtnSaveAs,
            this.toolStripSeparator3,
            this.toolBtnRun,
            this.toolBtnDbg,
            this.toolStripSeparator2,
            this.toolBtnPick,
            this.toolStripSeparator1,
            this.toolBtnComment,
            this.toolBtnUncomment});
            this.toolStrip.Location = new System.Drawing.Point(0, 3);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(37, 464);
            this.toolStrip.TabIndex = 0;
            // 
            // toolBtnClose
            // 
            this.toolBtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnClose.Image = global::App.Properties.Resources.ImgClose;
            this.toolBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnClose.Name = "toolBtnClose";
            this.toolBtnClose.Size = new System.Drawing.Size(35, 36);
            this.toolBtnClose.Text = "Close Tab";
            this.toolBtnClose.Click += new System.EventHandler(this.toolBtnClose_Click);
            // 
            // toolBtnNew
            // 
            this.toolBtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnNew.Image = global::App.Properties.Resources.ImgNew;
            this.toolBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnNew.Name = "toolBtnNew";
            this.toolBtnNew.Size = new System.Drawing.Size(35, 36);
            this.toolBtnNew.Text = "New";
            this.toolBtnNew.Click += new System.EventHandler(this.toolBtnNew_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(35, 6);
            // 
            // toolBtnOpen
            // 
            this.toolBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnOpen.Image = global::App.Properties.Resources.ImgOpen;
            this.toolBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnOpen.Name = "toolBtnOpen";
            this.toolBtnOpen.Size = new System.Drawing.Size(35, 36);
            this.toolBtnOpen.Text = "Open (Ctrl + O)";
            this.toolBtnOpen.Click += new System.EventHandler(this.toolBtnOpen_Click);
            // 
            // toolBtnSave
            // 
            this.toolBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSave.Image = global::App.Properties.Resources.ImgSave;
            this.toolBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSave.Name = "toolBtnSave";
            this.toolBtnSave.Size = new System.Drawing.Size(35, 36);
            this.toolBtnSave.Text = "Save (Ctrl + S)";
            this.toolBtnSave.Click += new System.EventHandler(this.toolBtnSave_Click);
            // 
            // toolBtnSaveAll
            // 
            this.toolBtnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSaveAll.Image = global::App.Properties.Resources.ImgSaveAll;
            this.toolBtnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSaveAll.Name = "toolBtnSaveAll";
            this.toolBtnSaveAll.Size = new System.Drawing.Size(35, 36);
            this.toolBtnSaveAll.Text = "Save all (Ctrl + Shift + S)";
            this.toolBtnSaveAll.Click += new System.EventHandler(this.toolBtnSaveAll_Click);
            // 
            // toolBtnSaveAs
            // 
            this.toolBtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSaveAs.Image = global::App.Properties.Resources.ImgSaveAs;
            this.toolBtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSaveAs.Name = "toolBtnSaveAs";
            this.toolBtnSaveAs.Size = new System.Drawing.Size(35, 36);
            this.toolBtnSaveAs.Text = "Save as (Alt + S)";
            this.toolBtnSaveAs.Click += new System.EventHandler(this.toolBtnSaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(35, 6);
            // 
            // toolBtnRun
            // 
            this.toolBtnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnRun.Image = global::App.Properties.Resources.ImgRun;
            this.toolBtnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnRun.Name = "toolBtnRun";
            this.toolBtnRun.Size = new System.Drawing.Size(35, 36);
            this.toolBtnRun.Text = "Run (F5)";
            this.toolBtnRun.Click += new System.EventHandler(this.toolBtnRunDebug_Click);
            // 
            // toolBtnDbg
            // 
            this.toolBtnDbg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDbg.Image = global::App.Properties.Resources.ImgDbg;
            this.toolBtnDbg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDbg.Name = "toolBtnDbg";
            this.toolBtnDbg.Size = new System.Drawing.Size(35, 36);
            this.toolBtnDbg.Text = "Debug (F6)";
            this.toolBtnDbg.Click += new System.EventHandler(this.toolBtnRunDebug_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.DarkGray;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(35, 6);
            // 
            // toolBtnPick
            // 
            this.toolBtnPick.CheckOnClick = true;
            this.toolBtnPick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnPick.Image = global::App.Properties.Resources.ImgPick;
            this.toolBtnPick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnPick.Name = "toolBtnPick";
            this.toolBtnPick.Size = new System.Drawing.Size(35, 36);
            this.toolBtnPick.Text = "Debug Fragment";
            this.toolBtnPick.CheckedChanged += new System.EventHandler(this.toolBtnPick_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(35, 6);
            // 
            // toolBtnComment
            // 
            this.toolBtnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnComment.Image = global::App.Properties.Resources.ImgComment;
            this.toolBtnComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnComment.Name = "toolBtnComment";
            this.toolBtnComment.Size = new System.Drawing.Size(35, 36);
            this.toolBtnComment.Text = "toolStripButton1";
            this.toolBtnComment.ToolTipText = "Comment selected text";
            this.toolBtnComment.Click += new System.EventHandler(this.toolBtnComment_Click);
            // 
            // toolBtnUncomment
            // 
            this.toolBtnUncomment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnUncomment.Image = global::App.Properties.Resources.ImgUncomment;
            this.toolBtnUncomment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnUncomment.Name = "toolBtnUncomment";
            this.toolBtnUncomment.Size = new System.Drawing.Size(35, 36);
            this.toolBtnUncomment.Text = "toolStripButton2";
            this.toolBtnUncomment.ToolTipText = "Uncomment selected text";
            this.toolBtnUncomment.Click += new System.EventHandler(this.toolBtnUncomment_Click);
            // 
            // tabResources
            // 
            this.tabResources.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tabResources.Controls.Add(this.tabData);
            this.tabResources.ForeColor = System.Drawing.Color.DarkGray;
            this.tabResources.Location = new System.Drawing.Point(4, 29);
            this.tabResources.Name = "tabResources";
            this.tabResources.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabResources.Size = new System.Drawing.Size(702, 723);
            this.tabResources.TabIndex = 1;
            this.tabResources.Text = "Resources";
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.tabDataImg);
            this.tabData.Controls.Add(this.tabDataBuf);
            this.tabData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabData.HighlightForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabData.Location = new System.Drawing.Point(3, 3);
            this.tabData.Margin = new System.Windows.Forms.Padding(0);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(696, 717);
            this.tabData.TabIndex = 0;
            this.tabData.WorkspaceColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // tabDataImg
            // 
            this.tabDataImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tabDataImg.Controls.Add(this.tableLayoutImages);
            this.tabDataImg.Location = new System.Drawing.Point(4, 29);
            this.tabDataImg.Name = "tabDataImg";
            this.tabDataImg.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabDataImg.Size = new System.Drawing.Size(688, 684);
            this.tabDataImg.TabIndex = 0;
            this.tabDataImg.Text = "Images";
            // 
            // tableLayoutImages
            // 
            this.tableLayoutImages.ColumnCount = 3;
            this.tableLayoutImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutImages.Controls.Add(this.numImgLayer, 1, 0);
            this.tableLayoutImages.Controls.Add(this.comboImg, 0, 0);
            this.tableLayoutImages.Controls.Add(this.panelImg, 0, 1);
            this.tableLayoutImages.Controls.Add(this.numImgLevel, 2, 0);
            this.tableLayoutImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutImages.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutImages.Name = "tableLayoutImages";
            this.tableLayoutImages.RowCount = 2;
            this.tableLayoutImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutImages.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutImages.Size = new System.Drawing.Size(682, 678);
            this.tableLayoutImages.TabIndex = 0;
            // 
            // numImgLayer
            // 
            this.numImgLayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.numImgLayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numImgLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numImgLayer.ForeColor = System.Drawing.Color.DarkGray;
            this.numImgLayer.Location = new System.Drawing.Point(412, 3);
            this.numImgLayer.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numImgLayer.Name = "numImgLayer";
            this.numImgLayer.Size = new System.Drawing.Size(130, 28);
            this.numImgLayer.TabIndex = 4;
            // 
            // comboImg
            // 
            this.comboImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.comboImg.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.comboImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboImg.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboImg.DropDownHeight = 200;
            this.comboImg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboImg.DropDownWidth = 403;
            this.comboImg.FormattingEnabled = true;
            this.comboImg.IsDroppedDown = false;
            this.comboImg.Location = new System.Drawing.Point(3, 3);
            this.comboImg.MaxDropDownItems = 8;
            this.comboImg.Name = "comboImg";
            this.comboImg.SelectedIndex = -1;
            this.comboImg.SelectedItem = null;
            this.comboImg.Size = new System.Drawing.Size(403, 29);
            this.comboImg.Soreted = false;
            this.comboImg.TabIndex = 1;
            this.comboImg.Transparent = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboImg.SelectedIndexChanged += new System.EventHandler(this.comboImg_SelectedIndexChanged);
            // 
            // panelImg
            // 
            this.panelImg.AutoScroll = true;
            this.panelImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutImages.SetColumnSpan(this.panelImg, 3);
            this.panelImg.Controls.Add(this.pictureImg);
            this.panelImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImg.Location = new System.Drawing.Point(0, 40);
            this.panelImg.Margin = new System.Windows.Forms.Padding(0);
            this.panelImg.Name = "panelImg";
            this.panelImg.Size = new System.Drawing.Size(682, 651);
            this.panelImg.TabIndex = 2;
            // 
            // pictureImg
            // 
            this.pictureImg.Location = new System.Drawing.Point(0, 0);
            this.pictureImg.Name = "pictureImg";
            this.pictureImg.Size = new System.Drawing.Size(10, 10);
            this.pictureImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureImg.TabIndex = 0;
            this.pictureImg.TabStop = false;
            this.pictureImg.Click += new System.EventHandler(this.pictureImg_Click);
            // 
            // numImgLevel
            // 
            this.numImgLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.numImgLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numImgLevel.ForeColor = System.Drawing.Color.DarkGray;
            this.numImgLevel.Location = new System.Drawing.Point(548, 3);
            this.numImgLevel.Name = "numImgLevel";
            this.numImgLevel.Size = new System.Drawing.Size(131, 28);
            this.numImgLevel.TabIndex = 5;
            // 
            // tabDataBuf
            // 
            this.tabDataBuf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tabDataBuf.Controls.Add(this.tableLayoutBufferDef);
            this.tabDataBuf.Location = new System.Drawing.Point(4, 29);
            this.tabDataBuf.Name = "tabDataBuf";
            this.tabDataBuf.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabDataBuf.Size = new System.Drawing.Size(684, 673);
            this.tabDataBuf.TabIndex = 1;
            this.tabDataBuf.Text = "Buffers";
            // 
            // tableLayoutBufferDef
            // 
            this.tableLayoutBufferDef.ColumnCount = 1;
            this.tableLayoutBufferDef.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBufferDef.Controls.Add(this.tableBuf, 0, 1);
            this.tableLayoutBufferDef.Controls.Add(this.tableLayoutBuffers, 0, 0);
            this.tableLayoutBufferDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutBufferDef.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutBufferDef.Name = "tableLayoutBufferDef";
            this.tableLayoutBufferDef.RowCount = 2;
            this.tableLayoutBufferDef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutBufferDef.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutBufferDef.Size = new System.Drawing.Size(678, 667);
            this.tableLayoutBufferDef.TabIndex = 0;
            // 
            // tableBuf
            // 
            this.tableBuf.AllowUserToAddRows = false;
            this.tableBuf.AllowUserToDeleteRows = false;
            this.tableBuf.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableBuf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableBuf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableBuf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBuf.Location = new System.Drawing.Point(3, 43);
            this.tableBuf.Name = "tableBuf";
            this.tableBuf.ReadOnly = true;
            this.tableBuf.RowTemplate.Height = 28;
            this.tableBuf.Size = new System.Drawing.Size(672, 638);
            this.tableBuf.TabIndex = 1;
            // 
            // tableLayoutBuffers
            // 
            this.tableLayoutBuffers.ColumnCount = 3;
            this.tableLayoutBuffers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutBuffers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutBuffers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutBuffers.Controls.Add(this.comboBuf, 0, 0);
            this.tableLayoutBuffers.Controls.Add(this.comboBufType, 1, 0);
            this.tableLayoutBuffers.Controls.Add(this.numBufDim, 2, 0);
            this.tableLayoutBuffers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutBuffers.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutBuffers.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutBuffers.Name = "tableLayoutBuffers";
            this.tableLayoutBuffers.RowCount = 1;
            this.tableLayoutBuffers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBuffers.Size = new System.Drawing.Size(678, 40);
            this.tableLayoutBuffers.TabIndex = 2;
            // 
            // comboBuf
            // 
            this.comboBuf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.comboBuf.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.comboBuf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBuf.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboBuf.DropDownHeight = 200;
            this.comboBuf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBuf.DropDownWidth = 400;
            this.comboBuf.FormattingEnabled = true;
            this.comboBuf.IsDroppedDown = false;
            this.comboBuf.Location = new System.Drawing.Point(3, 3);
            this.comboBuf.MaxDropDownItems = 8;
            this.comboBuf.Name = "comboBuf";
            this.comboBuf.SelectedIndex = -1;
            this.comboBuf.SelectedItem = null;
            this.comboBuf.Size = new System.Drawing.Size(400, 29);
            this.comboBuf.Soreted = false;
            this.comboBuf.TabIndex = 0;
            this.comboBuf.Transparent = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboBuf.SelectedIndexChanged += new System.EventHandler(this.comboBuf_SelectedIndexChanged);
            // 
            // comboBufType
            // 
            this.comboBufType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.comboBufType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.comboBufType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBufType.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboBufType.DropDownHeight = 200;
            this.comboBufType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBufType.DropDownWidth = 129;
            this.comboBufType.FormattingEnabled = true;
            this.comboBufType.IsDroppedDown = false;
            this.comboBufType.Location = new System.Drawing.Point(409, 3);
            this.comboBufType.MaxDropDownItems = 8;
            this.comboBufType.Name = "comboBufType";
            this.comboBufType.SelectedIndex = -1;
            this.comboBufType.SelectedItem = null;
            this.comboBufType.Size = new System.Drawing.Size(129, 29);
            this.comboBufType.Soreted = false;
            this.comboBufType.TabIndex = 1;
            this.comboBufType.Transparent = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboBufType.SelectedIndexChanged += new System.EventHandler(this.comboBufType_SelectedIndexChanged);
            // 
            // numBufDim
            // 
            this.numBufDim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.numBufDim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numBufDim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numBufDim.ForeColor = System.Drawing.Color.DarkGray;
            this.numBufDim.Location = new System.Drawing.Point(544, 3);
            this.numBufDim.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.numBufDim.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBufDim.Name = "numBufDim";
            this.numBufDim.Size = new System.Drawing.Size(131, 28);
            this.numBufDim.TabIndex = 2;
            this.numBufDim.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numBufDim.ValueChanged += new System.EventHandler(this.numBufDim_ValueChanged);
            // 
            // tabProperties
            // 
            this.tabProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tabProperties.Controls.Add(this.tableLayoutPanel1);
            this.tabProperties.ForeColor = System.Drawing.Color.DarkGray;
            this.tabProperties.Location = new System.Drawing.Point(4, 29);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabProperties.Size = new System.Drawing.Size(702, 724);
            this.tabProperties.TabIndex = 2;
            this.tabProperties.Text = "Properties";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboProp, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(696, 718);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid.CategorySplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.propertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.propertyGrid.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.propertyGrid.HelpForeColor = System.Drawing.Color.DarkGray;
            this.propertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.propertyGrid.Location = new System.Drawing.Point(3, 43);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.SelectedItemWithFocusForeColor = System.Drawing.Color.LightGray;
            this.propertyGrid.Size = new System.Drawing.Size(690, 672);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.propertyGrid.ViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.propertyGrid.ViewForeColor = System.Drawing.Color.DarkGray;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            this.propertyGrid.Click += new System.EventHandler(this.propertyGrid_Click);
            // 
            // comboProp
            // 
            this.comboProp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.comboProp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.comboProp.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboProp.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboProp.DropDownHeight = 200;
            this.comboProp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProp.DropDownWidth = 690;
            this.comboProp.FormattingEnabled = true;
            this.comboProp.IsDroppedDown = false;
            this.comboProp.Location = new System.Drawing.Point(3, 3);
            this.comboProp.MaxDropDownItems = 8;
            this.comboProp.Name = "comboProp";
            this.comboProp.SelectedIndex = -1;
            this.comboProp.SelectedItem = null;
            this.comboProp.Size = new System.Drawing.Size(690, 29);
            this.comboProp.Soreted = false;
            this.comboProp.TabIndex = 0;
            this.comboProp.Transparent = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboProp.SelectedIndexChanged += new System.EventHandler(this.comboProp_SelectedIndexChanged);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1474, 800);
            this.ControlBox = false;
            this.Controls.Add(this.layoutMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "App";
            this.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.Load += new System.EventHandler(this.App_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.App_KeyUp);
            this.layoutMain.ResumeLayout(false);
            this.tableLayoutTitleBar.ResumeLayout(false);
            this.tableLayoutTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgAppIcon)).EndInit();
            this.splitRenderCoding.Panel1.ResumeLayout(false);
            this.splitRenderCoding.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderCoding)).EndInit();
            this.splitRenderCoding.ResumeLayout(false);
            this.splitRenderOutput.Panel1.ResumeLayout(false);
            this.splitRenderOutput.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderOutput)).EndInit();
            this.splitRenderOutput.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tabCompile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.output)).EndInit();
            this.tabDebugger.ResumeLayout(false);
            this.splitDebug.Panel1.ResumeLayout(false);
            this.splitDebug.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDebug)).EndInit();
            this.splitDebug.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabCode.ResumeLayout(false);
            this.tabCodeTableLayout.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.RightToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.RightToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabResources.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tabDataImg.ResumeLayout(false);
            this.tableLayoutImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numImgLayer)).EndInit();
            this.panelImg.ResumeLayout(false);
            this.panelImg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImgLevel)).EndInit();
            this.tabDataBuf.ResumeLayout(false);
            this.tableLayoutBufferDef.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableBuf)).EndInit();
            this.tableLayoutBuffers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBufDim)).EndInit();
            this.tabProperties.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region Make Form DPI-Aware
        protected void MakeDPIAware()
        {
            layoutMain.RowStyles[0].Height = Properties.Resources.logo.Height + 4;

            imgAppIcon.Width = Properties.Resources.logo.Width + 1;
            imgAppIcon.Height = Properties.Resources.logo.Height + 1;

            labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", imgAppIcon.Height - 4, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));

            btnWindowMinimize.Width = btnWindowMinimize.Image.Width + 9;
            btnWindowMinimize.Height = btnWindowMinimize.Image.Height + 1;
            btnWindowMaximize.Width = btnWindowMaximize.Image.Width + 9;
            btnWindowMaximize.Height = btnWindowMaximize.Image.Height + 1;
            btnWindowClose.Width = btnWindowClose.Image.Width + 9;
            btnWindowClose.Height = btnWindowClose.Image.Height + 1;

            tableLayoutTitleBar.ColumnStyles[2].Width = btnWindowMinimize.Width + 2;
            tableLayoutTitleBar.ColumnStyles[3].Width = btnWindowMaximize.Width + 2;
            tableLayoutTitleBar.ColumnStyles[4].Width = btnWindowClose.Width + 2;
        }
        #endregion

        private System.Windows.Forms.SplitContainer splitRenderCoding;
        private OpenTK.GraphicControl glControl;
        private System.Windows.Forms.TabPage tabCode;
        private System.Windows.Forms.TabPage tabResources;
        private System.Windows.Forms.TabControlEx tabData;
        private System.Windows.Forms.TabPage tabDataImg;
        private System.Windows.Forms.TabPage tabDataBuf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutImages;
        private System.Windows.Forms.PictureBox pictureImg;
        private System.Windows.Forms.ComboBoxEx comboImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBufferDef;
        private System.Windows.Forms.ComboBoxEx comboBuf;
        private System.Windows.Forms.DataGridView tableBuf;
        private System.Windows.Forms.TableLayoutPanel tabCodeTableLayout;
        private System.Windows.Forms.Panel panelImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBuffers;
        private System.Windows.Forms.ComboBoxEx comboBufType;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.TabControlEx tabSource;
        private System.Windows.Forms.ToolStripEx toolStrip;
        private System.Windows.Forms.ToolStripButton toolBtnOpen;
        private System.Windows.Forms.ToolStripButton toolBtnSave;
        private System.Windows.Forms.ToolStripButton toolBtnSaveAll;
        private System.Windows.Forms.ToolStripButton toolBtnRun;
        private System.Windows.Forms.ToolStripButton toolBtnNew;
        private System.Windows.Forms.ToolStripButton toolBtnSaveAs;
        private System.Windows.Forms.ToolStripButton toolBtnClose;
        private System.Windows.Forms.NumericUpDown numBufDim;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ComboBoxEx comboProp;
        private System.Windows.Forms.NumericUpDown numImgLayer;
        private System.Windows.Forms.NumericUpDown numImgLevel;
        private System.Windows.Forms.TabControlEx tabOutput;
        private System.Windows.Forms.TabPage tabCompile;
        private System.Windows.Forms.TabPage tabDebugger;
        private System.Windows.Forms.PropertyGrid debugProperty;
        private System.Windows.Forms.ToolStripButton toolBtnDbg;
        private System.Windows.Forms.SplitContainer splitDebug;
        private System.Windows.Forms.ListView debugListView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolBtnPick;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DataGridView output;
        private System.Windows.Forms.SplitContainer splitRenderOutput;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolBtnComment;
        private System.Windows.Forms.ToolStripButton toolBtnUncomment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TableLayoutPanel layoutMain;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnWindowClose;
        private System.Windows.Forms.Button btnWindowMaximize;
        private System.Windows.Forms.Button btnWindowMinimize;
        private System.Windows.Forms.TableLayoutPanel tableLayoutTitleBar;
        private System.Windows.Forms.PictureBox imgAppIcon;
        private System.Windows.Forms.TabControlEx tabControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn File;
        private System.Windows.Forms.DataGridViewTextBoxColumn Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}