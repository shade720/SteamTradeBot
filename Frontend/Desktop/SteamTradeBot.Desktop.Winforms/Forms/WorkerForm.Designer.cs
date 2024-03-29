﻿namespace SteamTradeBot.Desktop.Winforms.Forms
{
    partial class WorkerForm
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            UptimeLabel = new Label();
            WarningsLabel = new Label();
            ErrorsLabel = new Label();
            ItemsSoldLabel = new Label();
            ItemsBoughtLabel = new Label();
            ItemsAnalyzedLabel = new Label();
            ServiceStateLabel = new Label();
            ConnectionStateLabel = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            ViewLogsButton = new Button();
            HistoryFilterComboBox = new ComboBox();
            HistoryDataGridView = new DataGridView();
            TimeColumn = new DataGridViewTextBoxColumn();
            Item = new DataGridViewTextBoxColumn();
            OrderTypeColumn = new DataGridViewTextBoxColumn();
            BuyPrice = new DataGridViewTextBoxColumn();
            SellPrice = new DataGridViewTextBoxColumn();
            Profit = new DataGridViewTextBoxColumn();
            groupBox3 = new GroupBox();
            CancelOrdersButtons = new Button();
            StartButton = new Button();
            StopButton = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            this.StateRefresher = new System.ComponentModel.BackgroundWorker();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HistoryDataGridView).BeginInit();
            groupBox3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(UptimeLabel);
            groupBox1.Controls.Add(WarningsLabel);
            groupBox1.Controls.Add(ErrorsLabel);
            groupBox1.Controls.Add(ItemsSoldLabel);
            groupBox1.Controls.Add(ItemsBoughtLabel);
            groupBox1.Controls.Add(ItemsAnalyzedLabel);
            groupBox1.Controls.Add(ServiceStateLabel);
            groupBox1.Controls.Add(ConnectionStateLabel);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.DodgerBlue;
            groupBox1.Location = new Point(0, 10);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(372, 414);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "State";
            // 
            // UptimeLabel
            // 
            UptimeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            UptimeLabel.AutoSize = true;
            UptimeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            UptimeLabel.ForeColor = Color.Black;
            UptimeLabel.Location = new Point(192, 306);
            UptimeLabel.Margin = new Padding(2, 0, 2, 0);
            UptimeLabel.Name = "UptimeLabel";
            UptimeLabel.Padding = new Padding(4);
            UptimeLabel.Size = new Size(8, 29);
            UptimeLabel.TabIndex = 15;
            // 
            // WarningsLabel
            // 
            WarningsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WarningsLabel.AutoSize = true;
            WarningsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            WarningsLabel.ForeColor = Color.Black;
            WarningsLabel.Location = new Point(192, 276);
            WarningsLabel.Margin = new Padding(2, 0, 2, 0);
            WarningsLabel.Name = "WarningsLabel";
            WarningsLabel.Padding = new Padding(4);
            WarningsLabel.Size = new Size(8, 29);
            WarningsLabel.TabIndex = 14;
            // 
            // ErrorsLabel
            // 
            ErrorsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ErrorsLabel.AutoSize = true;
            ErrorsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ErrorsLabel.ForeColor = Color.Black;
            ErrorsLabel.Location = new Point(192, 246);
            ErrorsLabel.Margin = new Padding(2, 0, 2, 0);
            ErrorsLabel.Name = "ErrorsLabel";
            ErrorsLabel.Padding = new Padding(4);
            ErrorsLabel.Size = new Size(8, 29);
            ErrorsLabel.TabIndex = 13;
            // 
            // ItemsSoldLabel
            // 
            ItemsSoldLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ItemsSoldLabel.AutoSize = true;
            ItemsSoldLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ItemsSoldLabel.ForeColor = Color.Black;
            ItemsSoldLabel.Location = new Point(192, 215);
            ItemsSoldLabel.Margin = new Padding(2, 0, 2, 0);
            ItemsSoldLabel.Name = "ItemsSoldLabel";
            ItemsSoldLabel.Padding = new Padding(4);
            ItemsSoldLabel.Size = new Size(8, 29);
            ItemsSoldLabel.TabIndex = 12;
            // 
            // ItemsBoughtLabel
            // 
            ItemsBoughtLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ItemsBoughtLabel.AutoSize = true;
            ItemsBoughtLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ItemsBoughtLabel.ForeColor = Color.Black;
            ItemsBoughtLabel.Location = new Point(192, 185);
            ItemsBoughtLabel.Margin = new Padding(2, 0, 2, 0);
            ItemsBoughtLabel.Name = "ItemsBoughtLabel";
            ItemsBoughtLabel.Padding = new Padding(4);
            ItemsBoughtLabel.Size = new Size(8, 29);
            ItemsBoughtLabel.TabIndex = 11;
            // 
            // ItemsAnalyzedLabel
            // 
            ItemsAnalyzedLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ItemsAnalyzedLabel.AutoSize = true;
            ItemsAnalyzedLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ItemsAnalyzedLabel.ForeColor = Color.Black;
            ItemsAnalyzedLabel.Location = new Point(192, 154);
            ItemsAnalyzedLabel.Margin = new Padding(2, 0, 2, 0);
            ItemsAnalyzedLabel.Name = "ItemsAnalyzedLabel";
            ItemsAnalyzedLabel.Padding = new Padding(4);
            ItemsAnalyzedLabel.Size = new Size(8, 29);
            ItemsAnalyzedLabel.TabIndex = 10;
            // 
            // ServiceStateLabel
            // 
            ServiceStateLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ServiceStateLabel.AutoSize = true;
            ServiceStateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ServiceStateLabel.ForeColor = Color.Black;
            ServiceStateLabel.Location = new Point(192, 124);
            ServiceStateLabel.Margin = new Padding(2, 0, 2, 0);
            ServiceStateLabel.Name = "ServiceStateLabel";
            ServiceStateLabel.Padding = new Padding(4);
            ServiceStateLabel.Size = new Size(8, 29);
            ServiceStateLabel.TabIndex = 9;
            // 
            // ConnectionStateLabel
            // 
            ConnectionStateLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ConnectionStateLabel.AutoSize = true;
            ConnectionStateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ConnectionStateLabel.ForeColor = Color.Black;
            ConnectionStateLabel.Location = new Point(192, 94);
            ConnectionStateLabel.Margin = new Padding(2, 0, 2, 0);
            ConnectionStateLabel.Name = "ConnectionStateLabel";
            ConnectionStateLabel.Padding = new Padding(4);
            ConnectionStateLabel.Size = new Size(8, 29);
            ConnectionStateLabel.TabIndex = 8;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(115, 306);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Padding = new Padding(4);
            label8.Size = new Size(72, 29);
            label8.TabIndex = 7;
            label8.Text = "Uptime:";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(101, 276);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Padding = new Padding(4);
            label7.Size = new Size(87, 29);
            label7.TabIndex = 6;
            label7.Text = "Warnings:";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(126, 246);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Padding = new Padding(4);
            label6.Size = new Size(63, 29);
            label6.TabIndex = 5;
            label6.Text = "Errors:";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(95, 215);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Padding = new Padding(4);
            label5.Size = new Size(92, 29);
            label5.TabIndex = 4;
            label5.Text = "Items sold:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(73, 185);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Padding = new Padding(4);
            label4.Size = new Size(113, 29);
            label4.TabIndex = 3;
            label4.Text = "Items bought:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(63, 154);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Padding = new Padding(4);
            label3.Size = new Size(124, 29);
            label3.TabIndex = 2;
            label3.Text = "Items analyzed:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(79, 124);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Padding = new Padding(4);
            label2.Size = new Size(108, 29);
            label2.TabIndex = 1;
            label2.Text = "Service state:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(49, 94);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Padding = new Padding(4);
            label1.Size = new Size(137, 29);
            label1.TabIndex = 0;
            label1.Text = "Connection state:";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(ViewLogsButton);
            groupBox2.Controls.Add(HistoryFilterComboBox);
            groupBox2.Controls.Add(HistoryDataGridView);
            groupBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.DodgerBlue;
            groupBox2.Location = new Point(11, 10);
            groupBox2.Margin = new Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(10);
            groupBox2.Size = new Size(927, 615);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Trade history";
            // 
            // ViewLogsButton
            // 
            ViewLogsButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ViewLogsButton.BackColor = Color.DodgerBlue;
            ViewLogsButton.FlatStyle = FlatStyle.Flat;
            ViewLogsButton.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            ViewLogsButton.ForeColor = Color.White;
            ViewLogsButton.Location = new Point(833, -5);
            ViewLogsButton.Margin = new Padding(2);
            ViewLogsButton.Name = "ViewLogsButton";
            ViewLogsButton.Size = new Size(85, 33);
            ViewLogsButton.TabIndex = 4;
            ViewLogsButton.Text = "Logs";
            ViewLogsButton.UseVisualStyleBackColor = false;
            ViewLogsButton.Click += ViewLogsButton_Click;
            // 
            // HistoryFilterComboBox
            // 
            HistoryFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            HistoryFilterComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            HistoryFilterComboBox.FormattingEnabled = true;
            HistoryFilterComboBox.Items.AddRange(new object[] { "All", "Bought", "Sold", "Canceled", "Error", "Warning" });
            HistoryFilterComboBox.Location = new Point(154, 0);
            HistoryFilterComboBox.Name = "HistoryFilterComboBox";
            HistoryFilterComboBox.Size = new Size(125, 28);
            HistoryFilterComboBox.TabIndex = 1;
            HistoryFilterComboBox.SelectedIndexChanged += HistoryFilterComboBox_SelectedIndexChanged;
            // 
            // HistoryDataGridView
            // 
            HistoryDataGridView.AllowUserToAddRows = false;
            HistoryDataGridView.AllowUserToDeleteRows = false;
            HistoryDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            HistoryDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            HistoryDataGridView.BackgroundColor = Color.White;
            HistoryDataGridView.BorderStyle = BorderStyle.None;
            HistoryDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            HistoryDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.DodgerBlue;
            dataGridViewCellStyle4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = Color.DodgerBlue;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            HistoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            HistoryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HistoryDataGridView.Columns.AddRange(new DataGridViewColumn[] { TimeColumn, Item, OrderTypeColumn, BuyPrice, SellPrice, Profit });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.DodgerBlue;
            dataGridViewCellStyle5.SelectionBackColor = Color.Red;
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            HistoryDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            HistoryDataGridView.EnableHeadersVisualStyles = false;
            HistoryDataGridView.Location = new Point(10, 36);
            HistoryDataGridView.Margin = new Padding(2);
            HistoryDataGridView.Name = "HistoryDataGridView";
            HistoryDataGridView.ReadOnly = true;
            HistoryDataGridView.RowHeadersVisible = false;
            HistoryDataGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = Color.DodgerBlue;
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            HistoryDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            HistoryDataGridView.RowTemplate.Height = 29;
            HistoryDataGridView.Size = new Size(907, 569);
            HistoryDataGridView.TabIndex = 0;
            // 
            // TimeColumn
            // 
            TimeColumn.HeaderText = "Time";
            TimeColumn.MinimumWidth = 6;
            TimeColumn.Name = "TimeColumn";
            TimeColumn.ReadOnly = true;
            TimeColumn.Width = 125;
            // 
            // Item
            // 
            Item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Item.HeaderText = "Item";
            Item.MinimumWidth = 6;
            Item.Name = "Item";
            Item.ReadOnly = true;
            // 
            // OrderTypeColumn
            // 
            OrderTypeColumn.HeaderText = "Type";
            OrderTypeColumn.MinimumWidth = 6;
            OrderTypeColumn.Name = "OrderTypeColumn";
            OrderTypeColumn.ReadOnly = true;
            OrderTypeColumn.Width = 110;
            // 
            // BuyPrice
            // 
            BuyPrice.HeaderText = "Buy price";
            BuyPrice.MinimumWidth = 6;
            BuyPrice.Name = "BuyPrice";
            BuyPrice.ReadOnly = true;
            BuyPrice.Width = 110;
            // 
            // SellPrice
            // 
            SellPrice.HeaderText = "Sell price";
            SellPrice.MinimumWidth = 6;
            SellPrice.Name = "SellPrice";
            SellPrice.ReadOnly = true;
            SellPrice.Width = 110;
            // 
            // Profit
            // 
            Profit.HeaderText = "Profit";
            Profit.MinimumWidth = 6;
            Profit.Name = "Profit";
            Profit.ReadOnly = true;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox3.Controls.Add(CancelOrdersButtons);
            groupBox3.Controls.Add(StartButton);
            groupBox3.Controls.Add(StopButton);
            groupBox3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.ForeColor = Color.DodgerBlue;
            groupBox3.Location = new Point(0, 428);
            groupBox3.Margin = new Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2);
            groupBox3.Size = new Size(372, 197);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Control";
            // 
            // CancelOrdersButtons
            // 
            CancelOrdersButtons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CancelOrdersButtons.BackColor = Color.DodgerBlue;
            CancelOrdersButtons.FlatStyle = FlatStyle.Flat;
            CancelOrdersButtons.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            CancelOrdersButtons.ForeColor = Color.White;
            CancelOrdersButtons.Location = new Point(27, 82);
            CancelOrdersButtons.Margin = new Padding(2);
            CancelOrdersButtons.Name = "CancelOrdersButtons";
            CancelOrdersButtons.Size = new Size(140, 48);
            CancelOrdersButtons.TabIndex = 2;
            CancelOrdersButtons.Text = "Cancel Orders";
            CancelOrdersButtons.UseVisualStyleBackColor = false;
            CancelOrdersButtons.Click += CancelOrdersButtons_Click;
            // 
            // StartButton
            // 
            StartButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StartButton.BackColor = Color.DodgerBlue;
            StartButton.FlatStyle = FlatStyle.Flat;
            StartButton.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            StartButton.ForeColor = Color.White;
            StartButton.Location = new Point(192, 82);
            StartButton.Margin = new Padding(2);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(140, 48);
            StartButton.TabIndex = 0;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartButton_Click;
            // 
            // StopButton
            // 
            StopButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StopButton.BackColor = Color.Red;
            StopButton.FlatStyle = FlatStyle.Flat;
            StopButton.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            StopButton.ForeColor = Color.White;
            StopButton.Location = new Point(192, 82);
            StopButton.Margin = new Padding(2);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(140, 48);
            StopButton.TabIndex = 1;
            StopButton.Text = "Stop";
            StopButton.UseVisualStyleBackColor = false;
            StopButton.Click += StopButton_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(groupBox2);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(947, 635);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(groupBox3);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(953, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 10, 10, 10);
            panel2.Size = new Size(382, 635);
            panel2.TabIndex = 5;
            // 
            // StateRefresher
            // 
            this.StateRefresher.DoWork += this.StateRefresher_DoWork;
            // 
            // WorkerForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1335, 635);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "WorkerForm";
            Text = "Worker";
            FormClosing += WorkerForm_FormClosing;
            Load += WorkerForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)HistoryDataGridView).EndInit();
            groupBox3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button StopButton;
        private Button StartButton;
        private Label label1;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label UptimeLabel;
        private Label WarningsLabel;
        private Label ErrorsLabel;
        private Label ItemsSoldLabel;
        private Label ItemsBoughtLabel;
        private Label ItemsAnalyzedLabel;
        private Label ServiceStateLabel;
        private Label ConnectionStateLabel;
        private Button CancelOrdersButtons;
        private Button ViewLogsButton;
        private DataGridView HistoryDataGridView;
        private Panel panel1;
        private Panel panel2;
        private DataGridViewTextBoxColumn TimeColumn;
        private DataGridViewTextBoxColumn Item;
        private DataGridViewTextBoxColumn OrderTypeColumn;
        private DataGridViewTextBoxColumn BuyPrice;
        private DataGridViewTextBoxColumn SellPrice;
        private DataGridViewTextBoxColumn Profit;
        private ComboBox HistoryFilterComboBox;
        private System.ComponentModel.BackgroundWorker StateRefresher;
    }
}