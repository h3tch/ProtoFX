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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.text = new System.Windows.Forms.RichTextBox();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.text.Location = new System.Drawing.Point(0, 0);
            this.text.Margin = new System.Windows.Forms.Padding(2);
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.text.Size = new System.Drawing.Size(263, 61);
            this.text.TabIndex = 100;
            this.text.TabStop = false;
            this.text.Text = "";
            this.text.WordWrap = false;
            this.text.MouseEnter += new System.EventHandler(this.MouseEnterHandler);
            // 
            // chart
            // 
            chartArea.AxisX.IsStartedFromZero = true;
            chartArea.AxisX.LabelAutoFitMaxFontSize = 8;
            chartArea.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea.AxisY.IsStartedFromZero = true;
            chartArea.AxisY.LabelAutoFitMaxFontSize = 8;
            chartArea.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea.Name = "ChartArea";
            chartArea.AxisX.IsMarginVisible = false;
            this.chart.ChartAreas.Add(chartArea);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Margin = new System.Windows.Forms.Padding(0);
            this.chart.Name = "chart";
            series.ChartArea = "ChartArea";
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series.IsVisibleInLegend = false;
            series.Name = "ChartSeries";
            this.chart.Series.Add(series);
            this.chart.Size = new System.Drawing.Size(263, 61);
            this.chart.TabIndex = 101;
            this.chart.Text = "chart";
            this.chart.Visible = false;
            this.chart.MouseEnter += new System.EventHandler(this.MouseEnterHandler);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel.Controls.Add(this.chart);
            this.panel.Controls.Add(this.text);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(0);
            this.panel.Size = new System.Drawing.Size(263, 61);
            this.panel.TabIndex = 102;
            // 
            // CallTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 65);
            this.ControlBox = false;
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CallTip";
            this.Opacity = 0.95D;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = false;
            this.Text = "CallTip";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox text;
        private DataVisualization.Charting.Chart chart;
        private Panel panel;
    }
}