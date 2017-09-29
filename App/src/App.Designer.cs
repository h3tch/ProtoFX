using System.Windows.Forms;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.splitRenderCoding = new System.Windows.Forms.SplitContainer();
            this.tableLayoutRenderOutput = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.splitRenderOutput = new System.Windows.Forms.SplitContainer();
            this.glControl = new OpenTK.GraphicControl();
            this.tabOutput = new System.Windows.Forms.FXTabControl();
            this.tabCompile = new System.Windows.Forms.TabPage();
            this.output = new System.Windows.Forms.DataGridView();
            this.File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabDebugger = new System.Windows.Forms.TabPage();
            this.splitDebug = new System.Windows.Forms.SplitContainer();
            this.splitDebugPerf = new System.Windows.Forms.SplitContainer();
            this.debugPanel = new System.Windows.Forms.Panel();
            this.debugListView = new System.Windows.Forms.AutoSizeListView();
            this.debugProperty = new System.Windows.Forms.PropertyGrid();
            this.chartPerf = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.imgAppIcon = new System.Windows.Forms.PictureBox();
            this.btnWindowMinimize2 = new System.Windows.Forms.Button();
            this.btnWindowMaximize2 = new System.Windows.Forms.Button();
            this.btnWindowClose2 = new System.Windows.Forms.Button();
            this.panelCoding = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.tableLayoutMenu = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripContainerMenu = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip = new System.Windows.Forms.FXToolStrip();
            this.toolBtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnRun = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDbg = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDbgStepBreakpoint = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDbgStepOver = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDbgStepInto = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDbgStepBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnPick = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnComment = new System.Windows.Forms.ToolStripButton();
            this.toolBtnUncomment = new System.Windows.Forms.ToolStripButton();
            this.btnWindowMinimize = new System.Windows.Forms.Button();
            this.btnWindowMaximize = new System.Windows.Forms.Button();
            this.btnWindowClose = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.FXTabControl();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.tabCodeTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripContainerCoding = new System.Windows.Forms.ToolStripContainer();
            this.tabSource = new System.Windows.Forms.FXTabControl();
            this.tabResources = new System.Windows.Forms.TabPage();
            this.tabData = new System.Windows.Forms.FXTabControl();
            this.tabDataBuf = new System.Windows.Forms.TabPage();
            this.tableLayoutImages = new System.Windows.Forms.TableLayoutPanel();
            this.numImgLayer = new System.Windows.Forms.NumericUpDown();
            this.comboImg = new System.Windows.Forms.ComboBoxEx();
            this.panelImg = new System.Windows.Forms.ImageViewer();
            this.numImgLevel = new System.Windows.Forms.NumericUpDown();
            this.tabDataImg = new System.Windows.Forms.TabPage();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderCoding)).BeginInit();
            this.splitRenderCoding.Panel1.SuspendLayout();
            this.splitRenderCoding.Panel2.SuspendLayout();
            this.splitRenderCoding.SuspendLayout();
            this.tableLayoutRenderOutput.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitDebugPerf)).BeginInit();
            this.splitDebugPerf.Panel1.SuspendLayout();
            this.splitDebugPerf.Panel2.SuspendLayout();
            this.splitDebugPerf.SuspendLayout();
            this.debugPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAppIcon)).BeginInit();
            this.panelCoding.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.tableLayoutMenu.SuspendLayout();
            this.toolStripContainerMenu.TopToolStripPanel.SuspendLayout();
            this.toolStripContainerMenu.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCode.SuspendLayout();
            this.tabCodeTableLayout.SuspendLayout();
            this.toolStripContainerCoding.ContentPanel.SuspendLayout();
            this.toolStripContainerCoding.SuspendLayout();
            this.tabResources.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabDataImg.SuspendLayout();
            this.tableLayoutImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numImgLayer)).BeginInit();
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
            // splitRenderCoding
            // 
            this.splitRenderCoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRenderCoding.Location = new System.Drawing.Point(4, 5);
            this.splitRenderCoding.Margin = new System.Windows.Forms.Padding(0);
            this.splitRenderCoding.Name = "splitRenderCoding";
            // 
            // splitRenderCoding.Panel1
            // 
            this.splitRenderCoding.Panel1.Controls.Add(this.tableLayoutRenderOutput);
            // 
            // splitRenderCoding.Panel2
            // 
            this.splitRenderCoding.Panel2.Controls.Add(this.panelCoding);
            this.splitRenderCoding.Size = new System.Drawing.Size(1466, 790);
            this.splitRenderCoding.SplitterDistance = 597;
            this.splitRenderCoding.SplitterWidth = 6;
            this.splitRenderCoding.TabIndex = 0;
            // 
            // tableLayoutRenderOutput
            // 
            this.tableLayoutRenderOutput.ColumnCount = 5;
            this.tableLayoutRenderOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutRenderOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRenderOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutRenderOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutRenderOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutRenderOutput.Controls.Add(this.labelTitle, 1, 0);
            this.tableLayoutRenderOutput.Controls.Add(this.splitRenderOutput, 0, 1);
            this.tableLayoutRenderOutput.Controls.Add(this.imgAppIcon, 0, 0);
            this.tableLayoutRenderOutput.Controls.Add(this.btnWindowMinimize2, 2, 0);
            this.tableLayoutRenderOutput.Controls.Add(this.btnWindowMaximize2, 3, 0);
            this.tableLayoutRenderOutput.Controls.Add(this.btnWindowClose2, 4, 0);
            this.tableLayoutRenderOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRenderOutput.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutRenderOutput.Name = "tableLayoutRenderOutput";
            this.tableLayoutRenderOutput.RowCount = 2;
            this.tableLayoutRenderOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutRenderOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRenderOutput.Size = new System.Drawing.Size(597, 790);
            this.tableLayoutRenderOutput.TabIndex = 1;
            this.tableLayoutRenderOutput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            this.tableLayoutRenderOutput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(32, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(107, 31);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "ProtoFX";
            this.labelTitle.UseCompatibleTextRendering = true;
            // 
            // splitRenderOutput
            // 
            this.tableLayoutRenderOutput.SetColumnSpan(this.splitRenderOutput, 5);
            this.splitRenderOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRenderOutput.Location = new System.Drawing.Point(0, 31);
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
            this.splitRenderOutput.Size = new System.Drawing.Size(597, 759);
            this.splitRenderOutput.SplitterDistance = 494;
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
            this.glControl.Size = new System.Drawing.Size(597, 494);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseUp);
            // 
            // tabOutput
            // 
            this.tabOutput.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabOutput.Controls.Add(this.tabCompile);
            this.tabOutput.Controls.Add(this.tabDebugger);
            this.tabOutput.DisplayStyle = System.Windows.Forms.TabStyle.FX;
            // 
            // 
            // 
            this.tabOutput.DisplayStyleProvider.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabOutput.DisplayStyleProvider.BackColorHot = System.Drawing.SystemColors.ButtonFace;
            this.tabOutput.DisplayStyleProvider.BackColorSelected = System.Drawing.SystemColors.ButtonShadow;
            this.tabOutput.DisplayStyleProvider.BorderColor = System.Drawing.Color.Transparent;
            this.tabOutput.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ActiveBorder;
            this.tabOutput.DisplayStyleProvider.BorderColorSelected = System.Drawing.SystemColors.ActiveCaption;
            this.tabOutput.DisplayStyleProvider.CloserColor = System.Drawing.SystemColors.ButtonShadow;
            this.tabOutput.DisplayStyleProvider.CloserColorActive = System.Drawing.SystemColors.ActiveBorder;
            this.tabOutput.DisplayStyleProvider.CloserColorActivePen = null;
            this.tabOutput.DisplayStyleProvider.FocusTrack = false;
            this.tabOutput.DisplayStyleProvider.HotTrack = true;
            this.tabOutput.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabOutput.DisplayStyleProvider.Opacity = 1F;
            this.tabOutput.DisplayStyleProvider.Overlap = 0;
            this.tabOutput.DisplayStyleProvider.Padding = new System.Drawing.Point(8, 3);
            this.tabOutput.DisplayStyleProvider.ShowTabCloser = false;
            this.tabOutput.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlDark;
            this.tabOutput.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tabOutput.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutput.HotTrack = true;
            this.tabOutput.Location = new System.Drawing.Point(0, 0);
            this.tabOutput.Margin = new System.Windows.Forms.Padding(0);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.SelectedIndex = 0;
            this.tabOutput.Size = new System.Drawing.Size(597, 260);
            this.tabOutput.TabIndex = 1;
            // 
            // tabCompile
            // 
            this.tabCompile.Controls.Add(this.output);
            this.tabCompile.Location = new System.Drawing.Point(4, 4);
            this.tabCompile.Margin = new System.Windows.Forms.Padding(0);
            this.tabCompile.Name = "tabCompile";
            this.tabCompile.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabCompile.Size = new System.Drawing.Size(589, 226);
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
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.output.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.output.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.File,
            this.Line,
            this.Description});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.output.DefaultCellStyle = dataGridViewCellStyle2;
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.EnableHeadersVisualStyles = false;
            this.output.Location = new System.Drawing.Point(3, 3);
            this.output.Margin = new System.Windows.Forms.Padding(0);
            this.output.Name = "output";
            this.output.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.output.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.output.RowHeadersVisible = false;
            this.output.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.output.Size = new System.Drawing.Size(583, 220);
            this.output.TabIndex = 1;
            this.output.DoubleClick += new System.EventHandler(this.Output_DoubleClick);
            // 
            // File
            // 
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
            this.tabDebugger.Controls.Add(this.splitDebug);
            this.tabDebugger.Location = new System.Drawing.Point(4, 4);
            this.tabDebugger.Margin = new System.Windows.Forms.Padding(0);
            this.tabDebugger.Name = "tabDebugger";
            this.tabDebugger.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabDebugger.Size = new System.Drawing.Size(589, 226);
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
            this.splitDebug.Panel1.Controls.Add(this.splitDebugPerf);
            // 
            // splitDebug.Panel2
            // 
            this.splitDebug.Panel2.Controls.Add(this.chartPerf);
            this.splitDebug.Size = new System.Drawing.Size(583, 220);
            this.splitDebug.SplitterDistance = 314;
            this.splitDebug.SplitterWidth = 6;
            this.splitDebug.TabIndex = 0;
            // 
            // splitDebugPerf
            // 
            this.splitDebugPerf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDebugPerf.Location = new System.Drawing.Point(0, 0);
            this.splitDebugPerf.Name = "splitDebugPerf";
            // 
            // splitDebugPerf.Panel1
            // 
            this.splitDebugPerf.Panel1.Controls.Add(this.debugPanel);
            // 
            // splitDebugPerf.Panel2
            // 
            this.splitDebugPerf.Panel2.Controls.Add(this.debugProperty);
            this.splitDebugPerf.Size = new System.Drawing.Size(314, 220);
            this.splitDebugPerf.SplitterDistance = 160;
            this.splitDebugPerf.TabIndex = 1;
            // 
            // debugPanel
            // 
            this.debugPanel.AutoScroll = true;
            this.debugPanel.Controls.Add(this.debugListView);
            this.debugPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugPanel.Location = new System.Drawing.Point(0, 0);
            this.debugPanel.Margin = new System.Windows.Forms.Padding(0);
            this.debugPanel.Name = "debugPanel";
            this.debugPanel.Size = new System.Drawing.Size(160, 220);
            this.debugPanel.TabIndex = 0;
            // 
            // debugListView
            // 
            this.debugListView.AutoScroll = true;
            this.debugListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugListView.GroupForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.debugListView.HeaderBackColor = System.Drawing.SystemColors.ButtonFace;
            this.debugListView.HeaderBorderColor = System.Drawing.SystemColors.ControlLight;
            this.debugListView.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.debugListView.HeaderHAllign = System.Drawing.StringAlignment.Center;
            this.debugListView.HeaderVAllign = System.Drawing.StringAlignment.Center;
            this.debugListView.ItemHAllign = System.Drawing.StringAlignment.Near;
            this.debugListView.ItemVAllign = System.Drawing.StringAlignment.Center;
            this.debugListView.Location = new System.Drawing.Point(0, 0);
            this.debugListView.Margin = new System.Windows.Forms.Padding(0);
            this.debugListView.Name = "debugListView";
            this.debugListView.Size = new System.Drawing.Size(10, 9);
            this.debugListView.TabIndex = 0;
            this.debugListView.Visible = false;
            // 
            // debugProperty
            // 
            this.debugProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugProperty.HelpVisible = false;
            this.debugProperty.Location = new System.Drawing.Point(0, 0);
            this.debugProperty.Margin = new System.Windows.Forms.Padding(0);
            this.debugProperty.Name = "debugProperty";
            this.debugProperty.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.debugProperty.Size = new System.Drawing.Size(150, 220);
            this.debugProperty.TabIndex = 1;
            this.debugProperty.ToolbarVisible = false;
            this.debugProperty.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            // 
            // chartPerf
            // 
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 8;
            chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont)
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30)
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisY.LabelAutoFitMaxFontSize = 8;
            chartArea1.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont)
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30)
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.Name = "ChartArea";
            this.chartPerf.ChartAreas.Add(chartArea1);
            this.chartPerf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPerf.Location = new System.Drawing.Point(0, 0);
            this.chartPerf.Margin = new System.Windows.Forms.Padding(0);
            this.chartPerf.Name = "chartPerf";
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsVisibleInLegend = false;
            series1.Name = "ChartSeries";
            this.chartPerf.Series.Add(series1);
            this.chartPerf.Size = new System.Drawing.Size(263, 220);
            this.chartPerf.TabIndex = 101;
            this.chartPerf.Text = "chartPerf";
            // 
            // imgAppIcon
            // 
            this.imgAppIcon.Image = global::App.Properties.Resources.logo;
            this.imgAppIcon.Location = new System.Drawing.Point(0, 0);
            this.imgAppIcon.Margin = new System.Windows.Forms.Padding(0);
            this.imgAppIcon.Name = "imgAppIcon";
            this.imgAppIcon.Size = new System.Drawing.Size(32, 31);
            this.imgAppIcon.TabIndex = 3;
            this.imgAppIcon.TabStop = false;
            // 
            // btnWindowMinimize2
            // 
            this.btnWindowMinimize2.FlatAppearance.BorderSize = 0;
            this.btnWindowMinimize2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnWindowMinimize2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
            this.btnWindowMinimize2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowMinimize2.Image = global::App.Properties.Resources.Minimize;
            this.btnWindowMinimize2.Location = new System.Drawing.Point(444, 0);
            this.btnWindowMinimize2.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowMinimize2.Name = "btnWindowMinimize2";
            this.btnWindowMinimize2.Size = new System.Drawing.Size(51, 31);
            this.btnWindowMinimize2.TabIndex = 4;
            this.btnWindowMinimize2.UseVisualStyleBackColor = true;
            this.btnWindowMinimize2.Visible = false;
            this.btnWindowMinimize2.Click += new System.EventHandler(this.BtnWindowMinimize_Click);
            // 
            // btnWindowMaximize2
            // 
            this.btnWindowMaximize2.FlatAppearance.BorderSize = 0;
            this.btnWindowMaximize2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnWindowMaximize2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
            this.btnWindowMaximize2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowMaximize2.Image = global::App.Properties.Resources.Maximize;
            this.btnWindowMaximize2.Location = new System.Drawing.Point(495, 0);
            this.btnWindowMaximize2.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowMaximize2.Name = "btnWindowMaximize2";
            this.btnWindowMaximize2.Size = new System.Drawing.Size(51, 31);
            this.btnWindowMaximize2.TabIndex = 5;
            this.btnWindowMaximize2.UseVisualStyleBackColor = true;
            this.btnWindowMaximize2.Visible = false;
            this.btnWindowMaximize2.Click += new System.EventHandler(this.BtnWindowMaximize_Click);
            // 
            // btnWindowClose2
            // 
            this.btnWindowClose2.FlatAppearance.BorderSize = 0;
            this.btnWindowClose2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnWindowClose2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnWindowClose2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowClose2.Image = global::App.Properties.Resources.Close;
            this.btnWindowClose2.Location = new System.Drawing.Point(546, 0);
            this.btnWindowClose2.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowClose2.Name = "btnWindowClose2";
            this.btnWindowClose2.Size = new System.Drawing.Size(51, 31);
            this.btnWindowClose2.TabIndex = 6;
            this.btnWindowClose2.UseVisualStyleBackColor = true;
            this.btnWindowClose2.Visible = false;
            this.btnWindowClose2.Click += new System.EventHandler(this.BtnWindowClose_Click);
            // 
            // panelCoding
            // 
            this.panelCoding.Controls.Add(this.panelMenu);
            this.panelCoding.Controls.Add(this.tabControl);
            this.panelCoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCoding.Location = new System.Drawing.Point(0, 0);
            this.panelCoding.Margin = new System.Windows.Forms.Padding(0);
            this.panelCoding.MinimumSize = new System.Drawing.Size(600, 0);
            this.panelCoding.Name = "panelCoding";
            this.panelCoding.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panelCoding.Size = new System.Drawing.Size(863, 790);
            this.panelCoding.TabIndex = 1;
            this.panelCoding.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            this.panelCoding.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenu.Controls.Add(this.tableLayoutMenu);
            this.panelMenu.Location = new System.Drawing.Point(274, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(590, 57);
            this.panelMenu.TabIndex = 1;
            // 
            // tableLayoutMenu
            // 
            this.tableLayoutMenu.ColumnCount = 4;
            this.tableLayoutMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutMenu.Controls.Add(this.toolStripContainerMenu, 0, 0);
            this.tableLayoutMenu.Controls.Add(this.btnWindowMinimize, 1, 0);
            this.tableLayoutMenu.Controls.Add(this.btnWindowMaximize, 2, 0);
            this.tableLayoutMenu.Controls.Add(this.btnWindowClose, 3, 0);
            this.tableLayoutMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutMenu.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMenu.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutMenu.Name = "tableLayoutMenu";
            this.tableLayoutMenu.RowCount = 1;
            this.tableLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMenu.Size = new System.Drawing.Size(590, 57);
            this.tableLayoutMenu.TabIndex = 0;
            // 
            // toolStripContainerMenu
            // 
            this.toolStripContainerMenu.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainerMenu.ContentPanel
            // 
            this.toolStripContainerMenu.ContentPanel.Size = new System.Drawing.Size(437, 18);
            this.toolStripContainerMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripContainerMenu.LeftToolStripPanelVisible = false;
            this.toolStripContainerMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerMenu.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripContainerMenu.Name = "toolStripContainerMenu";
            this.toolStripContainerMenu.RightToolStripPanelVisible = false;
            this.toolStripContainerMenu.Size = new System.Drawing.Size(437, 57);
            this.toolStripContainerMenu.TabIndex = 0;
            this.toolStripContainerMenu.Text = "toolStripContainer1";
            // 
            // toolStripContainerMenu.TopToolStripPanel
            // 
            this.toolStripContainerMenu.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnNew,
            this.toolBtnOpen,
            this.toolBtnSave,
            this.toolBtnSaveAll,
            this.toolBtnSaveAs,
            this.toolStripSeparator3,
            this.toolBtnRun,
            this.toolBtnDbg,
            this.toolBtnDbgStepBreakpoint,
            this.toolBtnDbgStepOver,
            this.toolBtnDbgStepInto,
            this.toolBtnDbgStepBack,
            this.toolStripSeparator2,
            this.toolBtnPick,
            this.toolStripSeparator1,
            this.toolBtnComment,
            this.toolBtnUncomment});
            this.toolStrip.Location = new System.Drawing.Point(3, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(434, 39);
            this.toolStrip.TabIndex = 0;
            // 
            // toolBtnNew
            // 
            this.toolBtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnNew.Image = global::App.Properties.Resources.ImgNew;
            this.toolBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnNew.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnNew.Name = "toolBtnNew";
            this.toolBtnNew.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolBtnNew.Size = new System.Drawing.Size(36, 39);
            this.toolBtnNew.Text = "New";
            this.toolBtnNew.Click += new System.EventHandler(this.ToolBtnNew_Click);
            // 
            // toolBtnOpen
            // 
            this.toolBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnOpen.Image = global::App.Properties.Resources.ImgOpen;
            this.toolBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnOpen.Name = "toolBtnOpen";
            this.toolBtnOpen.Size = new System.Drawing.Size(36, 39);
            this.toolBtnOpen.Text = "Open (Ctrl + O)";
            this.toolBtnOpen.Click += new System.EventHandler(this.ToolBtnOpen_Click);
            // 
            // toolBtnSave
            // 
            this.toolBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSave.Image = global::App.Properties.Resources.ImgSave;
            this.toolBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSave.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnSave.Name = "toolBtnSave";
            this.toolBtnSave.Size = new System.Drawing.Size(36, 39);
            this.toolBtnSave.Text = "Save (Ctrl + S)";
            this.toolBtnSave.Click += new System.EventHandler(this.ToolBtnSave_Click);
            // 
            // toolBtnSaveAll
            // 
            this.toolBtnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSaveAll.Image = global::App.Properties.Resources.ImgSaveAll;
            this.toolBtnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSaveAll.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnSaveAll.Name = "toolBtnSaveAll";
            this.toolBtnSaveAll.Size = new System.Drawing.Size(36, 39);
            this.toolBtnSaveAll.Text = "Save all (Ctrl + Shift + S)";
            this.toolBtnSaveAll.Click += new System.EventHandler(this.ToolBtnSaveAll_Click);
            // 
            // toolBtnSaveAs
            // 
            this.toolBtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSaveAs.Image = global::App.Properties.Resources.ImgSaveAs;
            this.toolBtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSaveAs.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnSaveAs.Name = "toolBtnSaveAs";
            this.toolBtnSaveAs.Size = new System.Drawing.Size(36, 39);
            this.toolBtnSaveAs.Text = "Save as (Alt + S)";
            this.toolBtnSaveAs.Click += new System.EventHandler(this.ToolBtnSaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // toolBtnRun
            // 
            this.toolBtnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnRun.Image = global::App.Properties.Resources.ImgRun;
            this.toolBtnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnRun.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnRun.Name = "toolBtnRun";
            this.toolBtnRun.Size = new System.Drawing.Size(36, 39);
            this.toolBtnRun.Text = "Run (F5)";
            this.toolBtnRun.Click += new System.EventHandler(this.ToolBtnRunDebug_Click);
            // 
            // toolBtnDbg
            // 
            this.toolBtnDbg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDbg.Image = global::App.Properties.Resources.ImgDbg;
            this.toolBtnDbg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDbg.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnDbg.Name = "toolBtnDbg";
            this.toolBtnDbg.Size = new System.Drawing.Size(36, 39);
            this.toolBtnDbg.Text = "Debug (F6)";
            this.toolBtnDbg.Click += new System.EventHandler(this.ToolBtnRunDebug_Click);
            // 
            // toolBtnDbgStepBreakpoint
            // 
            this.toolBtnDbgStepBreakpoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDbgStepBreakpoint.Enabled = false;
            this.toolBtnDbgStepBreakpoint.Image = global::App.Properties.Resources.ImgDbgStepBreakpoint;
            this.toolBtnDbgStepBreakpoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDbgStepBreakpoint.Name = "toolBtnDbgStepBreakpoint";
            this.toolBtnDbgStepBreakpoint.Size = new System.Drawing.Size(36, 36);
            this.toolBtnDbgStepBreakpoint.Text = "Next Breakpoint (F9)";
            this.toolBtnDbgStepBreakpoint.Click += new System.EventHandler(this.DebugStepBreakpoint_Click);
            // 
            // toolBtnDbgStepOver
            // 
            this.toolBtnDbgStepOver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDbgStepOver.Enabled = false;
            this.toolBtnDbgStepOver.Image = global::App.Properties.Resources.ImgDbgStepOver;
            this.toolBtnDbgStepOver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDbgStepOver.Name = "toolBtnDbgStepOver";
            this.toolBtnDbgStepOver.Size = new System.Drawing.Size(36, 36);
            this.toolBtnDbgStepOver.Text = "Step Over (F10)";
            this.toolBtnDbgStepOver.Click += new System.EventHandler(this.DebugStepOver_Click);
            // 
            // toolBtnDbgStepInto
            // 
            this.toolBtnDbgStepInto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDbgStepInto.Enabled = false;
            this.toolBtnDbgStepInto.Image = global::App.Properties.Resources.ImgDbgStepInto;
            this.toolBtnDbgStepInto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDbgStepInto.Name = "toolBtnDbgStepInto";
            this.toolBtnDbgStepInto.Size = new System.Drawing.Size(36, 36);
            this.toolBtnDbgStepInto.Text = "Step Into (F11)";
            this.toolBtnDbgStepInto.Click += new System.EventHandler(this.DebugStepInto_Click);
            // 
            // toolBtnDbgStepInto
            // 
            this.toolBtnDbgStepBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDbgStepBack.Enabled = false;
            this.toolBtnDbgStepBack.Image = global::App.Properties.Resources.ImgDbgStepBack;
            this.toolBtnDbgStepBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDbgStepBack.Name = "toolBtnDbgStepBack";
            this.toolBtnDbgStepBack.Size = new System.Drawing.Size(36, 36);
            this.toolBtnDbgStepBack.Text = "Step Back (SHIFT+F11)";
            this.toolBtnDbgStepBack.Click += new System.EventHandler(this.DebugStepBack_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolBtnPick
            // 
            this.toolBtnPick.CheckOnClick = true;
            this.toolBtnPick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnPick.Image = global::App.Properties.Resources.ImgPick;
            this.toolBtnPick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnPick.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnPick.Name = "toolBtnPick";
            this.toolBtnPick.Size = new System.Drawing.Size(36, 36);
            this.toolBtnPick.Text = "Debug Fragment";
            this.toolBtnPick.CheckedChanged += new System.EventHandler(this.ToolBtnPick_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolBtnComment
            // 
            this.toolBtnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnComment.Image = global::App.Properties.Resources.ImgComment;
            this.toolBtnComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnComment.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnComment.Name = "toolBtnComment";
            this.toolBtnComment.Size = new System.Drawing.Size(36, 36);
            this.toolBtnComment.Text = "Comment Selected Lines";
            this.toolBtnComment.ToolTipText = "Comment selected text";
            this.toolBtnComment.Click += new System.EventHandler(this.ToolBtnComment_Click);
            // 
            // toolBtnUncomment
            // 
            this.toolBtnUncomment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnUncomment.Image = global::App.Properties.Resources.ImgUncomment;
            this.toolBtnUncomment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnUncomment.Margin = new System.Windows.Forms.Padding(0);
            this.toolBtnUncomment.Name = "toolBtnUncomment";
            this.toolBtnUncomment.Size = new System.Drawing.Size(36, 36);
            this.toolBtnUncomment.Text = "Uncomment Selected Lines";
            this.toolBtnUncomment.ToolTipText = "Uncomment selected text";
            this.toolBtnUncomment.Click += new System.EventHandler(this.ToolBtnUncomment_Click);
            // 
            // btnWindowMinimize
            // 
            this.btnWindowMinimize.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWindowMinimize.FlatAppearance.BorderSize = 0;
            this.btnWindowMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnWindowMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
            this.btnWindowMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowMinimize.Image = global::App.Properties.Resources.Minimize;
            this.btnWindowMinimize.Location = new System.Drawing.Point(437, 0);
            this.btnWindowMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowMinimize.Name = "btnWindowMinimize";
            this.btnWindowMinimize.Size = new System.Drawing.Size(51, 38);
            this.btnWindowMinimize.TabIndex = 1;
            this.btnWindowMinimize.UseVisualStyleBackColor = true;
            this.btnWindowMinimize.Click += new System.EventHandler(this.BtnWindowMinimize_Click);
            // 
            // btnWindowMaximize
            // 
            this.btnWindowMaximize.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWindowMaximize.FlatAppearance.BorderSize = 0;
            this.btnWindowMaximize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnWindowMaximize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
            this.btnWindowMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowMaximize.Image = global::App.Properties.Resources.Maximize;
            this.btnWindowMaximize.Location = new System.Drawing.Point(488, 0);
            this.btnWindowMaximize.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowMaximize.Name = "btnWindowMaximize";
            this.btnWindowMaximize.Size = new System.Drawing.Size(51, 38);
            this.btnWindowMaximize.TabIndex = 2;
            this.btnWindowMaximize.UseVisualStyleBackColor = true;
            this.btnWindowMaximize.Click += new System.EventHandler(this.BtnWindowMaximize_Click);
            // 
            // btnWindowClose
            // 
            this.btnWindowClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWindowClose.FlatAppearance.BorderSize = 0;
            this.btnWindowClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnWindowClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnWindowClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowClose.Image = global::App.Properties.Resources.Close;
            this.btnWindowClose.Location = new System.Drawing.Point(539, 0);
            this.btnWindowClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindowClose.Name = "btnWindowClose";
            this.btnWindowClose.Size = new System.Drawing.Size(51, 38);
            this.btnWindowClose.TabIndex = 3;
            this.btnWindowClose.UseVisualStyleBackColor = true;
            this.btnWindowClose.Click += new System.EventHandler(this.BtnWindowClose_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCode);
            this.tabControl.Controls.Add(this.tabResources);
            this.tabControl.Controls.Add(this.tabProperties);
            this.tabControl.DisplayStyle = System.Windows.Forms.TabStyle.FX;
            // 
            // 
            // 
            this.tabControl.DisplayStyleProvider.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabControl.DisplayStyleProvider.BackColorHot = System.Drawing.SystemColors.ButtonFace;
            this.tabControl.DisplayStyleProvider.BackColorSelected = System.Drawing.SystemColors.ButtonShadow;
            this.tabControl.DisplayStyleProvider.BorderColor = System.Drawing.Color.Transparent;
            this.tabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ActiveBorder;
            this.tabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.SystemColors.ActiveCaption;
            this.tabControl.DisplayStyleProvider.CloserColor = System.Drawing.SystemColors.ButtonShadow;
            this.tabControl.DisplayStyleProvider.CloserColorActive = System.Drawing.SystemColors.ActiveBorder;
            this.tabControl.DisplayStyleProvider.CloserColorActivePen = null;
            this.tabControl.DisplayStyleProvider.FocusTrack = false;
            this.tabControl.DisplayStyleProvider.HotTrack = true;
            this.tabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabControl.DisplayStyleProvider.Opacity = 1F;
            this.tabControl.DisplayStyleProvider.Overlap = 0;
            this.tabControl.DisplayStyleProvider.Padding = new System.Drawing.Point(8, 3);
            this.tabControl.DisplayStyleProvider.ShowTabCloser = false;
            this.tabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlDark;
            this.tabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 20);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(863, 770);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabCode
            // 
            this.tabCode.Controls.Add(this.tabCodeTableLayout);
            this.tabCode.Location = new System.Drawing.Point(4, 32);
            this.tabCode.Margin = new System.Windows.Forms.Padding(0);
            this.tabCode.Name = "tabCode";
            this.tabCode.Size = new System.Drawing.Size(855, 734);
            this.tabCode.TabIndex = 0;
            this.tabCode.Text = "Code";
            // 
            // tabCodeTableLayout
            // 
            this.tabCodeTableLayout.ColumnCount = 1;
            this.tabCodeTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabCodeTableLayout.Controls.Add(this.toolStripContainerCoding, 0, 0);
            this.tabCodeTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCodeTableLayout.Location = new System.Drawing.Point(0, 0);
            this.tabCodeTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tabCodeTableLayout.Name = "tabCodeTableLayout";
            this.tabCodeTableLayout.RowCount = 1;
            this.tabCodeTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabCodeTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 715F));
            this.tabCodeTableLayout.Size = new System.Drawing.Size(855, 734);
            this.tabCodeTableLayout.TabIndex = 1;
            // 
            // toolStripContainerCoding
            // 
            // 
            // toolStripContainerCoding.ContentPanel
            // 
            this.toolStripContainerCoding.ContentPanel.Controls.Add(this.tabSource);
            this.toolStripContainerCoding.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripContainerCoding.ContentPanel.Size = new System.Drawing.Size(855, 696);
            this.toolStripContainerCoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainerCoding.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerCoding.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripContainerCoding.Name = "toolStripContainerCoding";
            // 
            // toolStripContainerCoding.RightToolStripPanel
            // 
            this.toolStripContainerCoding.RightToolStripPanel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripContainerCoding.Size = new System.Drawing.Size(855, 734);
            this.toolStripContainerCoding.TabIndex = 1;
            this.toolStripContainerCoding.Text = "toolStripContainer2";
            // 
            // toolStripContainerCoding.TopToolStripPanel
            // 
            this.toolStripContainerCoding.TopToolStripPanel.BackColor = System.Drawing.Color.Transparent;
            // 
            // tabSource
            // 
            this.tabSource.DisplayStyle = System.Windows.Forms.TabStyle.FX;
            // 
            // 
            // 
            this.tabSource.DisplayStyleProvider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tabSource.DisplayStyleProvider.BackColorHot = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tabSource.DisplayStyleProvider.BackColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.tabSource.DisplayStyleProvider.BorderColor = System.Drawing.Color.Transparent;
            this.tabSource.DisplayStyleProvider.BorderColorHot = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(167)))), ((int)(((byte)(183)))));
            this.tabSource.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabSource.DisplayStyleProvider.CloserColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(99)))), ((int)(((byte)(61)))));
            this.tabSource.DisplayStyleProvider.CloserColorActive = System.Drawing.SystemColors.ActiveBorder;
            this.tabSource.DisplayStyleProvider.CloserColorActivePen = null;
            this.tabSource.DisplayStyleProvider.FocusTrack = false;
            this.tabSource.DisplayStyleProvider.HotTrack = true;
            this.tabSource.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabSource.DisplayStyleProvider.Opacity = 1F;
            this.tabSource.DisplayStyleProvider.Overlap = 0;
            this.tabSource.DisplayStyleProvider.Padding = new System.Drawing.Point(16, 3);
            this.tabSource.DisplayStyleProvider.ShowTabCloser = true;
            this.tabSource.DisplayStyleProvider.TextColor = System.Drawing.Color.Gray;
            this.tabSource.DisplayStyleProvider.TextColorDisabled = System.Drawing.Color.Gray;
            this.tabSource.DisplayStyleProvider.TextColorSelected = System.Drawing.Color.Gray;
            this.tabSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSource.HotTrack = true;
            this.tabSource.Location = new System.Drawing.Point(0, 0);
            this.tabSource.Margin = new System.Windows.Forms.Padding(0);
            this.tabSource.Name = "tabSource";
            this.tabSource.SelectedIndex = 0;
            this.tabSource.Size = new System.Drawing.Size(855, 696);
            this.tabSource.TabIndex = 0;
            this.tabSource.TabClosing += new System.EventHandler<System.Windows.Forms.TabControlCancelEventArgs>(this.TabSource_TabClose);
            // 
            // tabResources
            // 
            this.tabResources.Controls.Add(this.tabData);
            this.tabResources.Location = new System.Drawing.Point(4, 32);
            this.tabResources.Name = "tabResources";
            this.tabResources.Size = new System.Drawing.Size(854, 735);
            this.tabResources.TabIndex = 1;
            this.tabResources.Text = "Resources";
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.tabDataImg);
            this.tabData.Controls.Add(this.tabDataBuf);
            this.tabData.DisplayStyle = System.Windows.Forms.TabStyle.FX;
            // 
            // 
            // 
            this.tabData.DisplayStyleProvider.BackColor = System.Drawing.SystemColors.Control;
            this.tabData.DisplayStyleProvider.BackColorHot = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tabData.DisplayStyleProvider.BackColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.tabData.DisplayStyleProvider.BorderColor = System.Drawing.Color.Transparent;
            this.tabData.DisplayStyleProvider.BorderColorHot = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(167)))), ((int)(((byte)(183)))));
            this.tabData.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabData.DisplayStyleProvider.CloserColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(99)))), ((int)(((byte)(61)))));
            this.tabData.DisplayStyleProvider.CloserColorActive = System.Drawing.SystemColors.ActiveBorder;
            this.tabData.DisplayStyleProvider.CloserColorActivePen = null;
            this.tabData.DisplayStyleProvider.FocusTrack = false;
            this.tabData.DisplayStyleProvider.HotTrack = true;
            this.tabData.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabData.DisplayStyleProvider.Opacity = 1F;
            this.tabData.DisplayStyleProvider.Overlap = 0;
            this.tabData.DisplayStyleProvider.Padding = new System.Drawing.Point(8, 3);
            this.tabData.DisplayStyleProvider.ShowTabCloser = false;
            this.tabData.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.tabData.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlText;
            this.tabData.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabData.HotTrack = true;
            this.tabData.Location = new System.Drawing.Point(0, 0);
            this.tabData.Margin = new System.Windows.Forms.Padding(0);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(854, 735);
            this.tabData.TabIndex = 0;
            // 
            // tabDataImg
            // 
            this.tabDataImg.Controls.Add(this.tableLayoutImages);
            this.tabDataImg.Location = new System.Drawing.Point(4, 30);
            this.tabDataImg.Name = "tabDataImg";
            this.tabDataImg.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabDataImg.Size = new System.Drawing.Size(846, 701);
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
            this.tableLayoutImages.Size = new System.Drawing.Size(840, 695);
            this.tableLayoutImages.TabIndex = 0;
            // 
            // numImgLayer
            // 
            this.numImgLayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numImgLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numImgLayer.Location = new System.Drawing.Point(507, 3);
            this.numImgLayer.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numImgLayer.Name = "numImgLayer";
            this.numImgLayer.Size = new System.Drawing.Size(162, 26);
            this.numImgLayer.TabIndex = 4;
            // 
            // comboImg
            // 
            this.comboImg.BackColor = System.Drawing.SystemColors.Control;
            this.comboImg.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.comboImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboImg.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboImg.DropDownHeight = 200;
            this.comboImg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboImg.DropDownWidth = 498;
            this.comboImg.FormattingEnabled = true;
            this.comboImg.IsDroppedDown = false;
            this.comboImg.Location = new System.Drawing.Point(3, 3);
            this.comboImg.MaxDropDownItems = 8;
            this.comboImg.Name = "comboImg";
            this.comboImg.SelectedIndex = -1;
            this.comboImg.SelectedItem = null;
            this.comboImg.Size = new System.Drawing.Size(498, 27);
            this.comboImg.Soreted = false;
            this.comboImg.TabIndex = 1;
            this.comboImg.Transparent = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboImg.SelectedIndexChanged += new System.EventHandler(this.ComboImg_SelectedIndexChanged);
            // 
            // panelImg
            // 
            this.panelImg.AutoScroll = true;
            this.tableLayoutImages.SetColumnSpan(this.panelImg, 3);
            this.panelImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImg.Location = new System.Drawing.Point(0, 40);
            this.panelImg.Margin = new System.Windows.Forms.Padding(0);
            this.panelImg.Name = "panelImg";
            this.panelImg.Size = new System.Drawing.Size(840, 655);
            this.panelImg.TabIndex = 2;
            this.panelImg.Click += new System.EventHandler(this.PictureImg_Click);
            // 
            // numImgLevel
            // 
            this.numImgLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numImgLevel.Location = new System.Drawing.Point(675, 3);
            this.numImgLevel.Name = "numImgLevel";
            this.numImgLevel.Size = new System.Drawing.Size(162, 26);
            this.numImgLevel.TabIndex = 5;
            // 
            // tabDataBuf
            // 
            this.tabDataBuf.Controls.Add(this.tableLayoutBufferDef);
            this.tabDataBuf.Location = new System.Drawing.Point(4, 30);
            this.tabDataBuf.Name = "tabDataBuf";
            this.tabDataBuf.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabDataBuf.Size = new System.Drawing.Size(280, 68);
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
            this.tableLayoutBufferDef.Size = new System.Drawing.Size(274, 62);
            this.tableLayoutBufferDef.TabIndex = 0;
            // 
            // tableBuf
            // 
            this.tableBuf.AllowUserToAddRows = false;
            this.tableBuf.AllowUserToDeleteRows = false;
            this.tableBuf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableBuf.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableBuf.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.tableBuf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableBuf.ColumnHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableBuf.DefaultCellStyle = dataGridViewCellStyle5;
            this.tableBuf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBuf.EnableHeadersVisualStyles = false;
            this.tableBuf.Location = new System.Drawing.Point(0, 40);
            this.tableBuf.Margin = new System.Windows.Forms.Padding(0);
            this.tableBuf.Name = "tableBuf";
            this.tableBuf.ReadOnly = true;
            this.tableBuf.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableBuf.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.tableBuf.RowHeadersVisible = false;
            this.tableBuf.RowTemplate.Height = 28;
            this.tableBuf.Size = new System.Drawing.Size(274, 658);
            this.tableBuf.TabIndex = 1;
            this.tableBuf.Click += new System.EventHandler(this.TableBuf_Click);
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
            this.tableLayoutBuffers.Size = new System.Drawing.Size(274, 40);
            this.tableLayoutBuffers.TabIndex = 2;
            // 
            // comboBuf
            // 
            this.comboBuf.BackColor = System.Drawing.SystemColors.Control;
            this.comboBuf.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.comboBuf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBuf.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboBuf.DropDownHeight = 200;
            this.comboBuf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBuf.DropDownWidth = 158;
            this.comboBuf.FormattingEnabled = true;
            this.comboBuf.IsDroppedDown = false;
            this.comboBuf.Location = new System.Drawing.Point(3, 3);
            this.comboBuf.MaxDropDownItems = 8;
            this.comboBuf.Name = "comboBuf";
            this.comboBuf.SelectedIndex = -1;
            this.comboBuf.SelectedItem = null;
            this.comboBuf.Size = new System.Drawing.Size(158, 27);
            this.comboBuf.Soreted = false;
            this.comboBuf.TabIndex = 0;
            this.comboBuf.Transparent = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboBuf.SelectedIndexChanged += new System.EventHandler(this.ComboBuf_SelectedIndexChanged);
            // 
            // comboBufType
            // 
            this.comboBufType.BackColor = System.Drawing.SystemColors.Control;
            this.comboBufType.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.comboBufType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBufType.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboBufType.DropDownHeight = 200;
            this.comboBufType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBufType.DropDownWidth = 48;
            this.comboBufType.FormattingEnabled = true;
            this.comboBufType.IsDroppedDown = false;
            this.comboBufType.Location = new System.Drawing.Point(167, 3);
            this.comboBufType.MaxDropDownItems = 8;
            this.comboBufType.Name = "comboBufType";
            this.comboBufType.SelectedIndex = -1;
            this.comboBufType.SelectedItem = null;
            this.comboBufType.Size = new System.Drawing.Size(48, 27);
            this.comboBufType.Soreted = false;
            this.comboBufType.TabIndex = 1;
            this.comboBufType.Transparent = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboBufType.SelectedIndexChanged += new System.EventHandler(this.ComboBufType_SelectedIndexChanged);
            // 
            // numBufDim
            // 
            this.numBufDim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numBufDim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numBufDim.Location = new System.Drawing.Point(221, 3);
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
            this.numBufDim.Size = new System.Drawing.Size(50, 26);
            this.numBufDim.TabIndex = 2;
            this.numBufDim.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numBufDim.ValueChanged += new System.EventHandler(this.NumBufDim_ValueChanged);
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.tableLayoutPanel1);
            this.tabProperties.Location = new System.Drawing.Point(4, 32);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabProperties.Size = new System.Drawing.Size(854, 735);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(848, 729);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(3, 43);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(842, 683);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            this.propertyGrid.Click += new System.EventHandler(this.PropertyGrid_Click);
            // 
            // comboProp
            // 
            this.comboProp.BackColor = System.Drawing.SystemColors.Control;
            this.comboProp.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.comboProp.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboProp.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboProp.DropDownHeight = 200;
            this.comboProp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProp.DropDownWidth = 842;
            this.comboProp.FormattingEnabled = true;
            this.comboProp.IsDroppedDown = false;
            this.comboProp.Location = new System.Drawing.Point(3, 3);
            this.comboProp.MaxDropDownItems = 8;
            this.comboProp.Name = "comboProp";
            this.comboProp.SelectedIndex = -1;
            this.comboProp.SelectedItem = null;
            this.comboProp.Size = new System.Drawing.Size(842, 27);
            this.comboProp.Soreted = false;
            this.comboProp.TabIndex = 0;
            this.comboProp.Transparent = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboProp.SelectedIndexChanged += new System.EventHandler(this.ComboProp_SelectedIndexChanged);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 800);
            this.ControlBox = false;
            this.Controls.Add(this.splitRenderCoding);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "App";
            this.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.Load += new System.EventHandler(this.App_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.App_KeyUp);
            this.splitRenderCoding.Panel1.ResumeLayout(false);
            this.splitRenderCoding.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderCoding)).EndInit();
            this.splitRenderCoding.ResumeLayout(false);
            this.tableLayoutRenderOutput.ResumeLayout(false);
            this.tableLayoutRenderOutput.PerformLayout();
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
            this.splitDebugPerf.Panel1.ResumeLayout(false);
            this.splitDebugPerf.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDebugPerf)).EndInit();
            this.splitDebugPerf.ResumeLayout(false);
            this.debugPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPerf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAppIcon)).EndInit();
            this.panelCoding.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.tableLayoutMenu.ResumeLayout(false);
            this.toolStripContainerMenu.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainerMenu.TopToolStripPanel.PerformLayout();
            this.toolStripContainerMenu.ResumeLayout(false);
            this.toolStripContainerMenu.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabCode.ResumeLayout(false);
            this.tabCodeTableLayout.ResumeLayout(false);
            this.toolStripContainerCoding.ContentPanel.ResumeLayout(false);
            this.toolStripContainerCoding.ResumeLayout(false);
            this.toolStripContainerCoding.PerformLayout();
            this.tabResources.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tabDataImg.ResumeLayout(false);
            this.tableLayoutImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numImgLayer)).EndInit();
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
            // ADJUST TITLE BAR

            var img = Properties.Resources.logo;

            tableLayoutRenderOutput.ColumnStyles[0].Width = img.Width + 2;
            tableLayoutRenderOutput.RowStyles[0].Height = img.Height + 3;

            imgAppIcon.Width = img.Width + 1;
            imgAppIcon.Height = img.Height + 1;
            labelTitle.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                imgAppIcon.Height - 4,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Pixel, 0);

            // ADJUST WINDOW BUTTONS

            tableLayoutMenu.ColumnStyles[1].Width = btnWindowClose.Image.Width + 9;
            tableLayoutMenu.ColumnStyles[2].Width = btnWindowMaximize.Image.Width + 9;
            tableLayoutMenu.ColumnStyles[3].Width = btnWindowMinimize.Image.Width + 9;

            tableLayoutRenderOutput.ColumnStyles[2].Width = btnWindowClose.Image.Width + 9;
            tableLayoutRenderOutput.ColumnStyles[3].Width = btnWindowMaximize.Image.Width + 9;
            tableLayoutRenderOutput.ColumnStyles[4].Width = btnWindowMinimize.Image.Width + 9;

            btnWindowClose.Width = btnWindowClose.Image.Width + 9;
            btnWindowClose.Height = btnWindowClose.Image.Height + 3;

            btnWindowMaximize.Width = btnWindowMaximize.Image.Width + 9;
            btnWindowMaximize.Height = btnWindowMaximize.Image.Height + 3;

            btnWindowMinimize.Width = btnWindowMinimize.Image.Width + 9;
            btnWindowMinimize.Height = btnWindowMinimize.Image.Height + 3;

            btnWindowClose2.Width = btnWindowClose.Image.Width + 9;
            btnWindowClose2.Height = btnWindowClose.Image.Height + 3;

            btnWindowMaximize2.Width = btnWindowMaximize.Image.Width + 9;
            btnWindowMaximize2.Height = btnWindowMaximize.Image.Height + 3;

            btnWindowMinimize2.Width = btnWindowMinimize.Image.Width + 9;
            btnWindowMinimize2.Height = btnWindowMinimize.Image.Height + 3;

            // ADJUST TOOL STRIP PANEL

            int w = 0, h = 0;
            foreach (System.Windows.Forms.ToolStripItem item in toolStrip.Items)
            {
                w += item.Width;
                h = System.Math.Max(h, item.Height);
            }

            tableLayoutMenu.ColumnStyles[0].Width = w + 30;

            panelCoding.Padding = new System.Windows.Forms.Padding(0, img.Height + 10 - tabControl.GetTabRect(0).Height, 0, 0);

            panelMenu.Width = w + 30 + (btnWindowClose.Width + 2) * 3;
            panelMenu.Height = h + 4;
            panelMenu.Location = new System.Drawing.Point(panelCoding.Width - panelMenu.Width, 0);

            tableLayoutMenu.Height = panelMenu.Height - 1;
            toolStripContainerMenu.Height = panelMenu.Height - 1;
        }

        #endregion

        private OpenTK.GraphicControl glControl;
        private System.Windows.Forms.SplitContainer splitRenderCoding;
        private System.Windows.Forms.TabPage tabCode;
        private System.Windows.Forms.TabPage tabResources;
        private System.Windows.Forms.FXTabControl tabData;
        private System.Windows.Forms.TabPage tabDataImg;
        private System.Windows.Forms.TabPage tabDataBuf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutImages;
        private System.Windows.Forms.ComboBoxEx comboImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBufferDef;
        private System.Windows.Forms.ComboBoxEx comboBuf;
        private System.Windows.Forms.DataGridView tableBuf;
        private System.Windows.Forms.TableLayoutPanel tabCodeTableLayout;
        private System.Windows.Forms.ImageViewer panelImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBuffers;
        private System.Windows.Forms.ComboBoxEx comboBufType;
        private System.Windows.Forms.FXTabControl tabSource;
        private System.Windows.Forms.NumericUpDown numBufDim;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ComboBoxEx comboProp;
        private System.Windows.Forms.NumericUpDown numImgLayer;
        private System.Windows.Forms.NumericUpDown numImgLevel;
        private System.Windows.Forms.FXTabControl tabOutput;
        private System.Windows.Forms.TabPage tabCompile;
        private System.Windows.Forms.TabPage tabDebugger;
        private System.Windows.Forms.PropertyGrid debugProperty;
        private System.Windows.Forms.SplitContainer splitDebug;
        private System.Windows.Forms.Panel debugPanel;
        private System.Windows.Forms.AutoSizeListView debugListView;
        private System.Windows.Forms.DataGridView output;
        private System.Windows.Forms.SplitContainer splitRenderOutput;
        private System.Windows.Forms.ToolStripContainer toolStripContainerCoding;
        private System.Windows.Forms.ToolStripContainer toolStripContainerMenu;
        private System.Windows.Forms.FXToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolBtnOpen;
        private System.Windows.Forms.ToolStripButton toolBtnSave;
        private System.Windows.Forms.ToolStripButton toolBtnSaveAll;
        private System.Windows.Forms.ToolStripButton toolBtnRun;
        private System.Windows.Forms.ToolStripButton toolBtnNew;
        private System.Windows.Forms.ToolStripButton toolBtnSaveAs;
        private System.Windows.Forms.ToolStripButton toolBtnDbg;
        private System.Windows.Forms.ToolStripButton toolBtnDbgStepBreakpoint;
        private System.Windows.Forms.ToolStripButton toolBtnDbgStepOver;
        private System.Windows.Forms.ToolStripButton toolBtnDbgStepInto;
        private System.Windows.Forms.ToolStripButton toolBtnDbgStepBack;
        private System.Windows.Forms.ToolStripButton toolBtnPick;
        private System.Windows.Forms.ToolStripButton toolBtnComment;
        private System.Windows.Forms.ToolStripButton toolBtnUncomment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox imgAppIcon;
        private System.Windows.Forms.FXTabControl tabControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn File;
        private System.Windows.Forms.DataGridViewTextBoxColumn Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Panel panelCoding;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMenu;
        private System.Windows.Forms.Button btnWindowMinimize;
        private System.Windows.Forms.Button btnWindowMaximize;
        private System.Windows.Forms.Button btnWindowClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRenderOutput;
        private System.Windows.Forms.Button btnWindowMinimize2;
        private System.Windows.Forms.Button btnWindowMaximize2;
        private System.Windows.Forms.Button btnWindowClose2;
        private System.Windows.Forms.SplitContainer splitDebugPerf;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerf;
    }
}