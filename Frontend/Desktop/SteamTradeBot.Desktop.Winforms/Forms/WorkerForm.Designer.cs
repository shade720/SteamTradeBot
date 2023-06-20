namespace SteamTradeBot.Desktop.Winforms.Forms
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            label9 = new Label();
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
            LogsDataGridView = new DataGridView();
            Time = new DataGridViewTextBoxColumn();
            ItemName = new DataGridViewTextBoxColumn();
            BuyPrice = new DataGridViewTextBoxColumn();
            SellPrice = new DataGridViewTextBoxColumn();
            Profit = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            CheckConnectionButton = new Button();
            CancelOrdersButtons = new Button();
            StartButton = new Button();
            StopButton = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogsDataGridView).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label9);
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
            groupBox1.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.Red;
            groupBox1.Location = new Point(1051, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(464, 505);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "State";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(239, 383);
            label9.Name = "label9";
            label9.Padding = new Padding(5);
            label9.Size = new Size(96, 38);
            label9.TabIndex = 15;
            label9.Text = "01:52:51";
            // 
            // WarningsLabel
            // 
            WarningsLabel.AutoSize = true;
            WarningsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            WarningsLabel.ForeColor = Color.Black;
            WarningsLabel.Location = new Point(239, 345);
            WarningsLabel.Name = "WarningsLabel";
            WarningsLabel.Padding = new Padding(5);
            WarningsLabel.Size = new Size(33, 38);
            WarningsLabel.TabIndex = 14;
            WarningsLabel.Text = "1";
            // 
            // ErrorsLabel
            // 
            ErrorsLabel.AutoSize = true;
            ErrorsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ErrorsLabel.ForeColor = Color.Black;
            ErrorsLabel.Location = new Point(239, 307);
            ErrorsLabel.Name = "ErrorsLabel";
            ErrorsLabel.Padding = new Padding(5);
            ErrorsLabel.Size = new Size(33, 38);
            ErrorsLabel.TabIndex = 13;
            ErrorsLabel.Text = "0";
            // 
            // ItemsSoldLabel
            // 
            ItemsSoldLabel.AutoSize = true;
            ItemsSoldLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ItemsSoldLabel.ForeColor = Color.Black;
            ItemsSoldLabel.Location = new Point(239, 269);
            ItemsSoldLabel.Name = "ItemsSoldLabel";
            ItemsSoldLabel.Padding = new Padding(5);
            ItemsSoldLabel.Size = new Size(55, 38);
            ItemsSoldLabel.TabIndex = 12;
            ItemsSoldLabel.Text = "120";
            // 
            // ItemsBoughtLabel
            // 
            ItemsBoughtLabel.AutoSize = true;
            ItemsBoughtLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ItemsBoughtLabel.ForeColor = Color.Black;
            ItemsBoughtLabel.Location = new Point(239, 231);
            ItemsBoughtLabel.Name = "ItemsBoughtLabel";
            ItemsBoughtLabel.Padding = new Padding(5);
            ItemsBoughtLabel.Size = new Size(33, 38);
            ItemsBoughtLabel.TabIndex = 11;
            ItemsBoughtLabel.Text = "3";
            // 
            // ItemsAnalyzedLabel
            // 
            ItemsAnalyzedLabel.AutoSize = true;
            ItemsAnalyzedLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ItemsAnalyzedLabel.ForeColor = Color.Black;
            ItemsAnalyzedLabel.Location = new Point(239, 193);
            ItemsAnalyzedLabel.Name = "ItemsAnalyzedLabel";
            ItemsAnalyzedLabel.Padding = new Padding(5);
            ItemsAnalyzedLabel.Size = new Size(44, 38);
            ItemsAnalyzedLabel.TabIndex = 10;
            ItemsAnalyzedLabel.Text = "10";
            // 
            // ServiceStateLabel
            // 
            ServiceStateLabel.AutoSize = true;
            ServiceStateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ServiceStateLabel.ForeColor = Color.Black;
            ServiceStateLabel.Location = new Point(239, 155);
            ServiceStateLabel.Name = "ServiceStateLabel";
            ServiceStateLabel.Padding = new Padding(5);
            ServiceStateLabel.Size = new Size(48, 38);
            ServiceStateLabel.TabIndex = 9;
            ServiceStateLabel.Text = "Up";
            // 
            // ConnectionStateLabel
            // 
            ConnectionStateLabel.AutoSize = true;
            ConnectionStateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ConnectionStateLabel.ForeColor = Color.Black;
            ConnectionStateLabel.Location = new Point(239, 117);
            ConnectionStateLabel.Name = "ConnectionStateLabel";
            ConnectionStateLabel.Padding = new Padding(5);
            ConnectionStateLabel.Size = new Size(116, 38);
            ConnectionStateLabel.TabIndex = 8;
            ConnectionStateLabel.Text = "Connected";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(142, 383);
            label8.Name = "label8";
            label8.Padding = new Padding(5);
            label8.Size = new Size(91, 38);
            label8.TabIndex = 7;
            label8.Text = "Uptime:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(125, 345);
            label7.Name = "label7";
            label7.Padding = new Padding(5);
            label7.Size = new Size(108, 38);
            label7.TabIndex = 6;
            label7.Text = "Warnings:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(156, 307);
            label6.Name = "label6";
            label6.Padding = new Padding(5);
            label6.Size = new Size(77, 38);
            label6.TabIndex = 5;
            label6.Text = "Errors:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(118, 269);
            label5.Name = "label5";
            label5.Padding = new Padding(5);
            label5.Size = new Size(115, 38);
            label5.TabIndex = 4;
            label5.Text = "Items sold:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(90, 231);
            label4.Name = "label4";
            label4.Padding = new Padding(5);
            label4.Size = new Size(143, 38);
            label4.TabIndex = 3;
            label4.Text = "Items bought:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(78, 193);
            label3.Name = "label3";
            label3.Padding = new Padding(5);
            label3.Size = new Size(155, 38);
            label3.TabIndex = 2;
            label3.Text = "Items analyzed:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(98, 155);
            label2.Name = "label2";
            label2.Padding = new Padding(5);
            label2.Size = new Size(135, 38);
            label2.TabIndex = 1;
            label2.Text = "Service state:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(60, 117);
            label1.Name = "label1";
            label1.Padding = new Padding(5);
            label1.Size = new Size(173, 38);
            label1.TabIndex = 0;
            label1.Text = "Connection state:";
            // 
            // LogsDataGridView
            // 
            LogsDataGridView.AllowUserToAddRows = false;
            LogsDataGridView.AllowUserToDeleteRows = false;
            LogsDataGridView.BackgroundColor = Color.White;
            LogsDataGridView.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            LogsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            LogsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogsDataGridView.Columns.AddRange(new DataGridViewColumn[] { Time, ItemName, BuyPrice, SellPrice, Profit });
            LogsDataGridView.Location = new Point(6, 60);
            LogsDataGridView.Name = "LogsDataGridView";
            LogsDataGridView.ReadOnly = true;
            LogsDataGridView.RowHeadersVisible = false;
            LogsDataGridView.RowHeadersWidth = 51;
            LogsDataGridView.RowTemplate.Height = 29;
            LogsDataGridView.Size = new Size(1021, 690);
            LogsDataGridView.TabIndex = 1;
            // 
            // Time
            // 
            Time.HeaderText = "Time";
            Time.MinimumWidth = 6;
            Time.Name = "Time";
            Time.ReadOnly = true;
            Time.Width = 125;
            // 
            // ItemName
            // 
            ItemName.HeaderText = "ItemName";
            ItemName.MinimumWidth = 6;
            ItemName.Name = "ItemName";
            ItemName.ReadOnly = true;
            ItemName.Width = 500;
            // 
            // BuyPrice
            // 
            BuyPrice.HeaderText = "BuyPrice";
            BuyPrice.MinimumWidth = 6;
            BuyPrice.Name = "BuyPrice";
            BuyPrice.ReadOnly = true;
            BuyPrice.Width = 125;
            // 
            // SellPrice
            // 
            SellPrice.HeaderText = "SellPrice";
            SellPrice.MinimumWidth = 6;
            SellPrice.Name = "SellPrice";
            SellPrice.ReadOnly = true;
            SellPrice.Width = 125;
            // 
            // Profit
            // 
            Profit.HeaderText = "Profit";
            Profit.MinimumWidth = 6;
            Profit.Name = "Profit";
            Profit.ReadOnly = true;
            Profit.Width = 120;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(LogsDataGridView);
            groupBox2.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.Red;
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1033, 756);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Log";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(CheckConnectionButton);
            groupBox3.Controls.Add(CancelOrdersButtons);
            groupBox3.Controls.Add(StartButton);
            groupBox3.Controls.Add(StopButton);
            groupBox3.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.ForeColor = Color.Red;
            groupBox3.Location = new Point(1051, 523);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(464, 245);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Control";
            // 
            // CheckConnectionButton
            // 
            CheckConnectionButton.BackColor = Color.Red;
            CheckConnectionButton.FlatStyle = FlatStyle.Flat;
            CheckConnectionButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            CheckConnectionButton.ForeColor = Color.White;
            CheckConnectionButton.Location = new Point(6, 113);
            CheckConnectionButton.Name = "CheckConnectionButton";
            CheckConnectionButton.Size = new Size(200, 60);
            CheckConnectionButton.TabIndex = 3;
            CheckConnectionButton.Text = "Check";
            CheckConnectionButton.UseVisualStyleBackColor = false;
            // 
            // CancelOrdersButtons
            // 
            CancelOrdersButtons.BackColor = Color.Red;
            CancelOrdersButtons.FlatStyle = FlatStyle.Flat;
            CancelOrdersButtons.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            CancelOrdersButtons.ForeColor = Color.White;
            CancelOrdersButtons.Location = new Point(6, 179);
            CancelOrdersButtons.Name = "CancelOrdersButtons";
            CancelOrdersButtons.Size = new Size(200, 60);
            CancelOrdersButtons.TabIndex = 2;
            CancelOrdersButtons.Text = "Cancel Orders";
            CancelOrdersButtons.UseVisualStyleBackColor = false;
            // 
            // StartButton
            // 
            StartButton.BackColor = Color.Red;
            StartButton.FlatStyle = FlatStyle.Flat;
            StartButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            StartButton.ForeColor = Color.White;
            StartButton.Location = new Point(258, 179);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(200, 60);
            StartButton.TabIndex = 0;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartButton_Click;
            // 
            // StopButton
            // 
            StopButton.BackColor = Color.Red;
            StopButton.FlatStyle = FlatStyle.Flat;
            StopButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            StopButton.ForeColor = Color.White;
            StopButton.Location = new Point(258, 179);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(200, 60);
            StopButton.TabIndex = 1;
            StopButton.Text = "Stop";
            StopButton.UseVisualStyleBackColor = false;
            StopButton.Click += StopButton_Click;
            // 
            // WorkerForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1525, 780);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WorkerForm";
            Text = "Worker";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogsDataGridView).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView LogsDataGridView;
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
        private Label label9;
        private Label WarningsLabel;
        private Label ErrorsLabel;
        private Label ItemsSoldLabel;
        private Label ItemsBoughtLabel;
        private Label ItemsAnalyzedLabel;
        private Label ServiceStateLabel;
        private Label ConnectionStateLabel;
        private Button CheckConnectionButton;
        private Button CancelOrdersButtons;
        private DataGridViewTextBoxColumn Time;
        private DataGridViewTextBoxColumn ItemName;
        private DataGridViewTextBoxColumn BuyPrice;
        private DataGridViewTextBoxColumn SellPrice;
        private DataGridViewTextBoxColumn Profit;
    }
}