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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            groupBox2 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            RefreshButton = new Button();
            ToDatePicker = new DateTimePicker();
            FromDatePicker = new DateTimePicker();
            InfoTable = new DataGridView();
            groupBox1 = new GroupBox();
            TradingChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            groupBox3 = new GroupBox();
            label1 = new Label();
            UptimeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            groupBox4 = new GroupBox();
            BalanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            DayColumn = new DataGridViewTextBoxColumn();
            BoughtColumn = new DataGridViewTextBoxColumn();
            SoldColumn = new DataGridViewTextBoxColumn();
            CanceledColumn = new DataGridViewTextBoxColumn();
            UptimeColumn = new DataGridViewTextBoxColumn();
            ProfitColumn = new DataGridViewTextBoxColumn();
            BalanceColumn = new DataGridViewTextBoxColumn();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InfoTable).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TradingChart).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)UptimeChart).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BalanceChart).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(RefreshButton);
            groupBox2.Controls.Add(ToDatePicker);
            groupBox2.Controls.Add(FromDatePicker);
            groupBox2.Controls.Add(InfoTable);
            groupBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.DodgerBlue;
            groupBox2.Location = new Point(12, 323);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(653, 300);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Info";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(495, 72);
            label3.Name = "label3";
            label3.Size = new Size(28, 21);
            label3.TabIndex = 7;
            label3.Text = "To:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(495, 16);
            label2.Name = "label2";
            label2.Size = new Size(50, 21);
            label2.TabIndex = 6;
            label2.Text = "From:";
            // 
            // RefreshButton
            // 
            RefreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RefreshButton.BackColor = Color.DodgerBlue;
            RefreshButton.FlatStyle = FlatStyle.Flat;
            RefreshButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            RefreshButton.ForeColor = Color.White;
            RefreshButton.Location = new Point(498, 152);
            RefreshButton.Margin = new Padding(2);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(149, 33);
            RefreshButton.TabIndex = 5;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = false;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // ToDatePicker
            // 
            ToDatePicker.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ToDatePicker.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ToDatePicker.Location = new Point(500, 96);
            ToDatePicker.Name = "ToDatePicker";
            ToDatePicker.Size = new Size(147, 29);
            ToDatePicker.TabIndex = 2;
            // 
            // FromDatePicker
            // 
            FromDatePicker.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FromDatePicker.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FromDatePicker.Location = new Point(500, 40);
            FromDatePicker.Name = "FromDatePicker";
            FromDatePicker.Size = new Size(147, 29);
            FromDatePicker.TabIndex = 1;
            FromDatePicker.Value = new DateTime(2017, 9, 9, 0, 0, 0, 0);
            // 
            // InfoTable
            // 
            InfoTable.AllowUserToAddRows = false;
            InfoTable.AllowUserToDeleteRows = false;
            InfoTable.AllowUserToResizeColumns = false;
            InfoTable.AllowUserToResizeRows = false;
            InfoTable.BackgroundColor = Color.White;
            InfoTable.BorderStyle = BorderStyle.None;
            InfoTable.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            InfoTable.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.White;
            dataGridViewCellStyle1.SelectionForeColor = Color.DodgerBlue;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            InfoTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            InfoTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            InfoTable.Columns.AddRange(new DataGridViewColumn[] { DayColumn, BoughtColumn, SoldColumn, CanceledColumn, UptimeColumn, ProfitColumn, BalanceColumn });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.DodgerBlue;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.DodgerBlue;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            InfoTable.DefaultCellStyle = dataGridViewCellStyle2;
            InfoTable.EnableHeadersVisualStyles = false;
            InfoTable.GridColor = Color.White;
            InfoTable.Location = new Point(15, 41);
            InfoTable.Name = "InfoTable";
            InfoTable.ReadOnly = true;
            InfoTable.RowHeadersVisible = false;
            InfoTable.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.DodgerBlue;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            InfoTable.RowsDefaultCellStyle = dataGridViewCellStyle3;
            InfoTable.RowTemplate.Height = 25;
            InfoTable.Size = new Size(478, 250);
            InfoTable.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TradingChart);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.DodgerBlue;
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(653, 305);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Trading Chart";
            // 
            // TradingChart
            // 
            chartArea1.Name = "ChartArea1";
            TradingChart.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend1.Name = "Legend1";
            TradingChart.Legends.Add(legend1);
            TradingChart.Location = new Point(16, 33);
            TradingChart.Name = "TradingChart";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = Color.Lime;
            series1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            series1.Legend = "Legend1";
            series1.MarkerBorderWidth = 2;
            series1.Name = "Series1";
            TradingChart.Series.Add(series1);
            TradingChart.Size = new Size(624, 266);
            TradingChart.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(UptimeChart);
            groupBox3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.ForeColor = Color.DodgerBlue;
            groupBox3.Location = new Point(671, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(653, 305);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Uptime";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(457, 256);
            label1.Name = "label1";
            label1.Size = new Size(159, 30);
            label1.TabIndex = 2;
            label1.Text = "* - 100 = 24/24 hours worked\r\n       50 = 12/24 hours worked";
            // 
            // UptimeChart
            // 
            chartArea2.Name = "ChartArea1";
            UptimeChart.ChartAreas.Add(chartArea2);
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.Name = "Legend1";
            UptimeChart.Legends.Add(legend2);
            UptimeChart.Location = new Point(16, 33);
            UptimeChart.Name = "UptimeChart";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = Color.Lime;
            series2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            series2.Legend = "Legend1";
            series2.MarkerBorderWidth = 2;
            series2.Name = "Series1";
            UptimeChart.Series.Add(series2);
            UptimeChart.Size = new Size(624, 262);
            UptimeChart.TabIndex = 1;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(BalanceChart);
            groupBox4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox4.ForeColor = Color.DodgerBlue;
            groupBox4.Location = new Point(671, 323);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(653, 300);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Balance changing";
            // 
            // BalanceChart
            // 
            chartArea3.Name = "ChartArea1";
            BalanceChart.ChartAreas.Add(chartArea3);
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend3.Name = "Legend1";
            BalanceChart.Legends.Add(legend3);
            BalanceChart.Location = new Point(16, 33);
            BalanceChart.Name = "BalanceChart";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = Color.Lime;
            series3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            series3.Legend = "Legend1";
            series3.MarkerBorderWidth = 2;
            series3.Name = "Series1";
            BalanceChart.Series.Add(series3);
            BalanceChart.Size = new Size(624, 261);
            BalanceChart.TabIndex = 1;
            // 
            // DayColumn
            // 
            DayColumn.HeaderText = "Day";
            DayColumn.Name = "DayColumn";
            DayColumn.ReadOnly = true;
            DayColumn.Width = 80;
            // 
            // BoughtColumn
            // 
            BoughtColumn.HeaderText = "Bought";
            BoughtColumn.Name = "BoughtColumn";
            BoughtColumn.ReadOnly = true;
            BoughtColumn.Width = 60;
            // 
            // SoldColumn
            // 
            SoldColumn.HeaderText = "Sold";
            SoldColumn.Name = "SoldColumn";
            SoldColumn.ReadOnly = true;
            SoldColumn.Width = 50;
            // 
            // CanceledColumn
            // 
            CanceledColumn.HeaderText = "Canceled";
            CanceledColumn.Name = "CanceledColumn";
            CanceledColumn.ReadOnly = true;
            CanceledColumn.Width = 70;
            // 
            // UptimeColumn
            // 
            UptimeColumn.HeaderText = "Uptime";
            UptimeColumn.Name = "UptimeColumn";
            UptimeColumn.ReadOnly = true;
            UptimeColumn.Width = 70;
            // 
            // ProfitColumn
            // 
            ProfitColumn.HeaderText = "Profit";
            ProfitColumn.Name = "ProfitColumn";
            ProfitColumn.ReadOnly = true;
            ProfitColumn.Width = 60;
            // 
            // BalanceColumn
            // 
            BalanceColumn.HeaderText = "Balance";
            BalanceColumn.Name = "BalanceColumn";
            BalanceColumn.ReadOnly = true;
            BalanceColumn.Width = 80;
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
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InfoTable).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TradingChart).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)UptimeChart).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)BalanceChart).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart TradingChart;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart UptimeChart;
        private GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart BalanceChart;
        private Label label1;
        private DataGridView InfoTable;
        private DateTimePicker ToDatePicker;
        private DateTimePicker FromDatePicker;
        private Label label3;
        private Label label2;
        private Button RefreshButton;
        private DataGridViewTextBoxColumn DayColumn;
        private DataGridViewTextBoxColumn BoughtColumn;
        private DataGridViewTextBoxColumn SoldColumn;
        private DataGridViewTextBoxColumn CanceledColumn;
        private DataGridViewTextBoxColumn UptimeColumn;
        private DataGridViewTextBoxColumn ProfitColumn;
        private DataGridViewTextBoxColumn BalanceColumn;
    }
}