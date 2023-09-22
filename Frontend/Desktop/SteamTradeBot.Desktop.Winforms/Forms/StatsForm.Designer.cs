namespace SteamTradeBot.Desktop.Winforms.Forms
{
    partial class StatsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            TradingChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            groupBox3 = new GroupBox();
            UptimeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            groupBox4 = new GroupBox();
            BudgetChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TradingChart).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)UptimeChart).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BudgetChart).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.DodgerBlue;
            groupBox2.Location = new Point(671, 313);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(653, 264);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Info";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TradingChart);
            groupBox1.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.DodgerBlue;
            groupBox1.Location = new Point(12, 43);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(653, 264);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Trading Chart";
            // 
            // TradingChart
            // 
            chartArea4.Name = "ChartArea1";
            TradingChart.ChartAreas.Add(chartArea4);
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend4.Name = "Legend1";
            TradingChart.Legends.Add(legend4);
            TradingChart.Location = new Point(16, 33);
            TradingChart.Name = "TradingChart";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = Color.Lime;
            series4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            series4.Legend = "Legend1";
            series4.MarkerBorderWidth = 2;
            series4.Name = "Series1";
            TradingChart.Series.Add(series4);
            TradingChart.Size = new Size(624, 210);
            TradingChart.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(UptimeChart);
            groupBox3.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.ForeColor = Color.DodgerBlue;
            groupBox3.Location = new Point(671, 43);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(653, 264);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Uptime";
            // 
            // UptimeChart
            // 
            chartArea5.Name = "ChartArea1";
            UptimeChart.ChartAreas.Add(chartArea5);
            legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend5.Name = "Legend1";
            UptimeChart.Legends.Add(legend5);
            UptimeChart.Location = new Point(16, 33);
            UptimeChart.Name = "UptimeChart";
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = Color.Lime;
            series5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            series5.Legend = "Legend1";
            series5.MarkerBorderWidth = 2;
            series5.Name = "Series1";
            UptimeChart.Series.Add(series5);
            UptimeChart.Size = new Size(624, 210);
            UptimeChart.TabIndex = 1;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(BudgetChart);
            groupBox4.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox4.ForeColor = Color.DodgerBlue;
            groupBox4.Location = new Point(12, 313);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(653, 264);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Balance changing";
            // 
            // BudgetChart
            // 
            chartArea6.Name = "ChartArea1";
            BudgetChart.ChartAreas.Add(chartArea6);
            legend6.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend6.Name = "Legend1";
            BudgetChart.Legends.Add(legend6);
            BudgetChart.Location = new Point(16, 33);
            BudgetChart.Name = "BudgetChart";
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = Color.Lime;
            series6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            series6.Legend = "Legend1";
            series6.MarkerBorderWidth = 2;
            series6.Name = "Series1";
            BudgetChart.Series.Add(series6);
            BudgetChart.Size = new Size(624, 210);
            BudgetChart.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(458, 213);
            label1.Name = "label1";
            label1.Size = new Size(159, 30);
            label1.TabIndex = 2;
            label1.Text = "* - 100 = 24/24 hours worked\r\n       50 = 12/24 hours worked";
            // 
            // StatsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1335, 635);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "StatsForm";
            Text = "StatsForm";
            FormClosing += StatsForm_FormClosing;
            Load += StatsForm_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TradingChart).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)UptimeChart).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)BudgetChart).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart TradingChart;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart UptimeChart;
        private GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart BudgetChart;
        private Label label1;
    }
}