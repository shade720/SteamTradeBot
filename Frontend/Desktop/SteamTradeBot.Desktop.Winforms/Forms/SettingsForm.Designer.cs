﻿namespace SteamTradeBot.Desktop.Winforms.Forms
{
    partial class SettingsForm
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
            label8 = new Label();
            label6 = new Label();
            label5 = new Label();
            UploadSettingsButton = new Button();
            SaveSettingsButton = new Button();
            groupBox2 = new GroupBox();
            ItemListSizeTextBox = new TextBox();
            MaxPriceTextBox = new TextBox();
            MinPriceTextBox = new TextBox();
            ResetSettingsButton = new Button();
            groupBox3 = new GroupBox();
            SteamCommissionTextBox = new TextBox();
            SteamUserIdTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            label10 = new Label();
            BuyListingFindRangeTextBox = new TextBox();
            label7 = new Label();
            OrderQuantityTextBox = new TextBox();
            label5235262 = new Label();
            label3 = new Label();
            label9 = new Label();
            label1525 = new Label();
            TrendTextBox = new TextBox();
            label163463 = new Label();
            label1515236 = new Label();
            label11 = new Label();
            AnalysisIntervalComboBox = new ComboBox();
            SalesPerWeekTextBox = new TextBox();
            SellListingFindRangeTextBox = new TextBox();
            AveragePriceTextBox = new TextBox();
            RequiredProfitTextBox = new TextBox();
            label4 = new Label();
            AvailibleBalanceTextBox = new TextBox();
            FitRangePriceTextBox = new TextBox();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.DodgerBlue;
            label8.Location = new Point(128, 135);
            label8.Name = "label8";
            label8.Size = new Size(77, 25);
            label8.TabIndex = 76;
            label8.Text = "List size";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.DodgerBlue;
            label6.Location = new Point(108, 100);
            label6.Name = "label6";
            label6.Size = new Size(97, 25);
            label6.TabIndex = 75;
            label6.Text = "Max. price";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.DodgerBlue;
            label5.Location = new Point(110, 65);
            label5.Name = "label5";
            label5.Size = new Size(95, 25);
            label5.TabIndex = 74;
            label5.Text = "Min. price";
            // 
            // UploadSettingsButton
            // 
            UploadSettingsButton.Anchor = AnchorStyles.Bottom;
            UploadSettingsButton.BackColor = Color.DodgerBlue;
            UploadSettingsButton.FlatStyle = FlatStyle.Flat;
            UploadSettingsButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            UploadSettingsButton.ForeColor = Color.White;
            UploadSettingsButton.Location = new Point(200, 508);
            UploadSettingsButton.Margin = new Padding(3, 2, 3, 2);
            UploadSettingsButton.Name = "UploadSettingsButton";
            UploadSettingsButton.Size = new Size(175, 45);
            UploadSettingsButton.TabIndex = 89;
            UploadSettingsButton.Text = "Upload Settings";
            UploadSettingsButton.UseVisualStyleBackColor = false;
            UploadSettingsButton.Click += UploadSettingsButton_Click;
            // 
            // SaveSettingsButton
            // 
            SaveSettingsButton.Anchor = AnchorStyles.Bottom;
            SaveSettingsButton.BackColor = Color.DodgerBlue;
            SaveSettingsButton.FlatStyle = FlatStyle.Flat;
            SaveSettingsButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            SaveSettingsButton.ForeColor = Color.White;
            SaveSettingsButton.Location = new Point(381, 508);
            SaveSettingsButton.Margin = new Padding(3, 2, 3, 2);
            SaveSettingsButton.Name = "SaveSettingsButton";
            SaveSettingsButton.Size = new Size(175, 45);
            SaveSettingsButton.TabIndex = 90;
            SaveSettingsButton.Text = "Save settings";
            SaveSettingsButton.UseVisualStyleBackColor = false;
            SaveSettingsButton.Click += SaveSettingsButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox2.Controls.Add(ItemListSizeTextBox);
            groupBox2.Controls.Add(MaxPriceTextBox);
            groupBox2.Controls.Add(MinPriceTextBox);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label8);
            groupBox2.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.DodgerBlue;
            groupBox2.Location = new Point(466, 32);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(436, 238);
            groupBox2.TabIndex = 92;
            groupBox2.TabStop = false;
            groupBox2.Text = "Items list settings";
            // 
            // ItemListSizeTextBox
            // 
            ItemListSizeTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ItemListSizeTextBox.BackColor = Color.White;
            ItemListSizeTextBox.BorderStyle = BorderStyle.FixedSingle;
            ItemListSizeTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            ItemListSizeTextBox.ForeColor = Color.Black;
            ItemListSizeTextBox.Location = new Point(212, 133);
            ItemListSizeTextBox.Margin = new Padding(2);
            ItemListSizeTextBox.Name = "ItemListSizeTextBox";
            ItemListSizeTextBox.Size = new Size(130, 31);
            ItemListSizeTextBox.TabIndex = 89;
            // 
            // MaxPriceTextBox
            // 
            MaxPriceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MaxPriceTextBox.BackColor = Color.White;
            MaxPriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            MaxPriceTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            MaxPriceTextBox.ForeColor = Color.Black;
            MaxPriceTextBox.Location = new Point(212, 98);
            MaxPriceTextBox.Margin = new Padding(2);
            MaxPriceTextBox.Name = "MaxPriceTextBox";
            MaxPriceTextBox.Size = new Size(130, 31);
            MaxPriceTextBox.TabIndex = 88;
            // 
            // MinPriceTextBox
            // 
            MinPriceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MinPriceTextBox.BackColor = Color.White;
            MinPriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            MinPriceTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            MinPriceTextBox.ForeColor = Color.Black;
            MinPriceTextBox.Location = new Point(212, 63);
            MinPriceTextBox.Margin = new Padding(2);
            MinPriceTextBox.Name = "MinPriceTextBox";
            MinPriceTextBox.Size = new Size(130, 31);
            MinPriceTextBox.TabIndex = 87;
            // 
            // ResetSettingsButton
            // 
            ResetSettingsButton.Anchor = AnchorStyles.Bottom;
            ResetSettingsButton.BackColor = Color.DodgerBlue;
            ResetSettingsButton.FlatStyle = FlatStyle.Flat;
            ResetSettingsButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            ResetSettingsButton.ForeColor = Color.White;
            ResetSettingsButton.Location = new Point(562, 507);
            ResetSettingsButton.Margin = new Padding(3, 2, 3, 2);
            ResetSettingsButton.Name = "ResetSettingsButton";
            ResetSettingsButton.Size = new Size(175, 45);
            ResetSettingsButton.TabIndex = 93;
            ResetSettingsButton.Text = "Reset Settings";
            ResetSettingsButton.UseVisualStyleBackColor = false;
            ResetSettingsButton.Click += ResetSettingsButton_Click;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox3.Controls.Add(SteamCommissionTextBox);
            groupBox3.Controls.Add(SteamUserIdTextBox);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(label2);
            groupBox3.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.ForeColor = Color.DodgerBlue;
            groupBox3.Location = new Point(466, 242);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(436, 206);
            groupBox3.TabIndex = 94;
            groupBox3.TabStop = false;
            groupBox3.Text = "Steam settings";
            // 
            // SteamCommissionTextBox
            // 
            SteamCommissionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SteamCommissionTextBox.BackColor = Color.White;
            SteamCommissionTextBox.BorderStyle = BorderStyle.FixedSingle;
            SteamCommissionTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            SteamCommissionTextBox.ForeColor = Color.Black;
            SteamCommissionTextBox.Location = new Point(212, 112);
            SteamCommissionTextBox.Margin = new Padding(4);
            SteamCommissionTextBox.Name = "SteamCommissionTextBox";
            SteamCommissionTextBox.Size = new Size(130, 31);
            SteamCommissionTextBox.TabIndex = 88;
            // 
            // SteamUserIdTextBox
            // 
            SteamUserIdTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SteamUserIdTextBox.BackColor = Color.White;
            SteamUserIdTextBox.BorderStyle = BorderStyle.FixedSingle;
            SteamUserIdTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            SteamUserIdTextBox.ForeColor = Color.Black;
            SteamUserIdTextBox.Location = new Point(212, 78);
            SteamUserIdTextBox.Margin = new Padding(4);
            SteamUserIdTextBox.Name = "SteamUserIdTextBox";
            SteamUserIdTextBox.Size = new Size(130, 31);
            SteamUserIdTextBox.TabIndex = 87;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.DodgerBlue;
            label1.Location = new Point(78, 80);
            label1.Name = "label1";
            label1.Size = new Size(127, 25);
            label1.TabIndex = 74;
            label1.Text = "Steam user ID";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.DodgerBlue;
            label2.Location = new Point(38, 114);
            label2.Name = "label2";
            label2.Size = new Size(167, 25);
            label2.TabIndex = 75;
            label2.Text = "Steam commission";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(UploadSettingsButton);
            panel1.Controls.Add(SaveSettingsButton);
            panel1.Controls.Add(ResetSettingsButton);
            panel1.Location = new Point(202, 36);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(50, 50, 50, 0);
            panel1.Size = new Size(932, 564);
            panel1.TabIndex = 95;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(BuyListingFindRangeTextBox);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(OrderQuantityTextBox);
            groupBox1.Controls.Add(label5235262);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label1525);
            groupBox1.Controls.Add(TrendTextBox);
            groupBox1.Controls.Add(label163463);
            groupBox1.Controls.Add(label1515236);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(AnalysisIntervalComboBox);
            groupBox1.Controls.Add(SalesPerWeekTextBox);
            groupBox1.Controls.Add(SellListingFindRangeTextBox);
            groupBox1.Controls.Add(AveragePriceTextBox);
            groupBox1.Controls.Add(RequiredProfitTextBox);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(AvailibleBalanceTextBox);
            groupBox1.Controls.Add(FitRangePriceTextBox);
            groupBox1.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.DodgerBlue;
            groupBox1.Location = new Point(37, 32);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(422, 416);
            groupBox1.TabIndex = 95;
            groupBox1.TabStop = false;
            groupBox1.Text = "Core settings";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = Color.DodgerBlue;
            label10.Location = new Point(24, 327);
            label10.Name = "label10";
            label10.Size = new Size(191, 25);
            label10.TabIndex = 89;
            label10.Text = "Buy listing find range";
            // 
            // BuyListingFindRangeTextBox
            // 
            BuyListingFindRangeTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BuyListingFindRangeTextBox.BackColor = Color.White;
            BuyListingFindRangeTextBox.BorderStyle = BorderStyle.FixedSingle;
            BuyListingFindRangeTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            BuyListingFindRangeTextBox.ForeColor = Color.Black;
            BuyListingFindRangeTextBox.Location = new Point(220, 326);
            BuyListingFindRangeTextBox.Margin = new Padding(2);
            BuyListingFindRangeTextBox.Name = "BuyListingFindRangeTextBox";
            BuyListingFindRangeTextBox.Size = new Size(128, 31);
            BuyListingFindRangeTextBox.TabIndex = 88;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.DodgerBlue;
            label7.Location = new Point(79, 292);
            label7.Name = "label7";
            label7.Size = new Size(136, 25);
            label7.TabIndex = 87;
            label7.Text = "Order quantity";
            // 
            // OrderQuantityTextBox
            // 
            OrderQuantityTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OrderQuantityTextBox.BackColor = Color.White;
            OrderQuantityTextBox.BorderStyle = BorderStyle.FixedSingle;
            OrderQuantityTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            OrderQuantityTextBox.ForeColor = Color.Black;
            OrderQuantityTextBox.Location = new Point(220, 291);
            OrderQuantityTextBox.Margin = new Padding(2);
            OrderQuantityTextBox.Name = "OrderQuantityTextBox";
            OrderQuantityTextBox.Size = new Size(128, 31);
            OrderQuantityTextBox.TabIndex = 86;
            // 
            // label5235262
            // 
            label5235262.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5235262.AutoSize = true;
            label5235262.BackColor = Color.Transparent;
            label5235262.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label5235262.ForeColor = Color.DodgerBlue;
            label5235262.Location = new Point(84, 80);
            label5235262.Name = "label5235262";
            label5235262.Size = new Size(129, 25);
            label5235262.TabIndex = 58;
            label5235262.Text = "Require profit";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.DodgerBlue;
            label3.Location = new Point(161, 45);
            label3.Name = "label3";
            label3.Size = new Size(54, 25);
            label3.TabIndex = 54;
            label3.Text = "Sales";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.DodgerBlue;
            label9.Location = new Point(153, 220);
            label9.Name = "label9";
            label9.Size = new Size(60, 25);
            label9.TabIndex = 85;
            label9.Text = "Trend";
            // 
            // label1525
            // 
            label1525.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1525.AutoSize = true;
            label1525.BackColor = Color.Transparent;
            label1525.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label1525.ForeColor = Color.DodgerBlue;
            label1525.Location = new Point(25, 362);
            label1525.Name = "label1525";
            label1525.Size = new Size(190, 25);
            label1525.TabIndex = 56;
            label1525.Text = "Sell listing find range";
            // 
            // TrendTextBox
            // 
            TrendTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TrendTextBox.BackColor = Color.White;
            TrendTextBox.BorderStyle = BorderStyle.FixedSingle;
            TrendTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            TrendTextBox.ForeColor = Color.Black;
            TrendTextBox.Location = new Point(220, 219);
            TrendTextBox.Margin = new Padding(2);
            TrendTextBox.Name = "TrendTextBox";
            TrendTextBox.Size = new Size(128, 31);
            TrendTextBox.TabIndex = 84;
            // 
            // label163463
            // 
            label163463.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label163463.AutoSize = true;
            label163463.BackColor = Color.Transparent;
            label163463.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label163463.ForeColor = Color.DodgerBlue;
            label163463.Location = new Point(83, 150);
            label163463.Name = "label163463";
            label163463.Size = new Size(132, 25);
            label163463.TabIndex = 59;
            label163463.Text = "Fit price range";
            // 
            // label1515236
            // 
            label1515236.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1515236.AutoSize = true;
            label1515236.BackColor = Color.Transparent;
            label1515236.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label1515236.ForeColor = Color.DodgerBlue;
            label1515236.Location = new Point(58, 115);
            label1515236.Name = "label1515236";
            label1515236.Size = new Size(155, 25);
            label1515236.TabIndex = 60;
            label1515236.Text = "Available balance";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = Color.DodgerBlue;
            label11.Location = new Point(76, 256);
            label11.Name = "label11";
            label11.Size = new Size(139, 25);
            label11.TabIndex = 61;
            label11.Text = "Analisys period";
            // 
            // AnalysisIntervalComboBox
            // 
            AnalysisIntervalComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AnalysisIntervalComboBox.BackColor = Color.White;
            AnalysisIntervalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            AnalysisIntervalComboBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            AnalysisIntervalComboBox.ForeColor = Color.Black;
            AnalysisIntervalComboBox.FormattingEnabled = true;
            AnalysisIntervalComboBox.Items.AddRange(new object[] { "7", "30" });
            AnalysisIntervalComboBox.Location = new Point(220, 254);
            AnalysisIntervalComboBox.Margin = new Padding(2);
            AnalysisIntervalComboBox.Name = "AnalysisIntervalComboBox";
            AnalysisIntervalComboBox.Size = new Size(128, 33);
            AnalysisIntervalComboBox.TabIndex = 62;
            // 
            // SalesPerWeekTextBox
            // 
            SalesPerWeekTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SalesPerWeekTextBox.BackColor = Color.White;
            SalesPerWeekTextBox.BorderStyle = BorderStyle.FixedSingle;
            SalesPerWeekTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            SalesPerWeekTextBox.ForeColor = Color.Black;
            SalesPerWeekTextBox.Location = new Point(220, 44);
            SalesPerWeekTextBox.Margin = new Padding(2);
            SalesPerWeekTextBox.Name = "SalesPerWeekTextBox";
            SalesPerWeekTextBox.Size = new Size(128, 31);
            SalesPerWeekTextBox.TabIndex = 69;
            // 
            // SellListingFindRangeTextBox
            // 
            SellListingFindRangeTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SellListingFindRangeTextBox.BackColor = Color.White;
            SellListingFindRangeTextBox.BorderStyle = BorderStyle.FixedSingle;
            SellListingFindRangeTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            SellListingFindRangeTextBox.ForeColor = Color.Black;
            SellListingFindRangeTextBox.Location = new Point(220, 361);
            SellListingFindRangeTextBox.Margin = new Padding(2);
            SellListingFindRangeTextBox.Name = "SellListingFindRangeTextBox";
            SellListingFindRangeTextBox.Size = new Size(128, 31);
            SellListingFindRangeTextBox.TabIndex = 63;
            // 
            // AveragePriceTextBox
            // 
            AveragePriceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AveragePriceTextBox.BackColor = Color.White;
            AveragePriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            AveragePriceTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            AveragePriceTextBox.ForeColor = Color.Black;
            AveragePriceTextBox.Location = new Point(220, 184);
            AveragePriceTextBox.Margin = new Padding(2);
            AveragePriceTextBox.Name = "AveragePriceTextBox";
            AveragePriceTextBox.Size = new Size(128, 31);
            AveragePriceTextBox.TabIndex = 73;
            // 
            // RequiredProfitTextBox
            // 
            RequiredProfitTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RequiredProfitTextBox.BackColor = Color.White;
            RequiredProfitTextBox.BorderStyle = BorderStyle.FixedSingle;
            RequiredProfitTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            RequiredProfitTextBox.ForeColor = Color.Black;
            RequiredProfitTextBox.Location = new Point(220, 79);
            RequiredProfitTextBox.Margin = new Padding(2);
            RequiredProfitTextBox.Name = "RequiredProfitTextBox";
            RequiredProfitTextBox.Size = new Size(128, 31);
            RequiredProfitTextBox.TabIndex = 66;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI Semibold", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.DodgerBlue;
            label4.Location = new Point(89, 185);
            label4.Name = "label4";
            label4.Size = new Size(126, 25);
            label4.TabIndex = 72;
            label4.Text = "Average price";
            // 
            // AvailibleBalanceTextBox
            // 
            AvailibleBalanceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AvailibleBalanceTextBox.BackColor = Color.White;
            AvailibleBalanceTextBox.BorderStyle = BorderStyle.FixedSingle;
            AvailibleBalanceTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            AvailibleBalanceTextBox.ForeColor = Color.Black;
            AvailibleBalanceTextBox.Location = new Point(220, 114);
            AvailibleBalanceTextBox.Margin = new Padding(2);
            AvailibleBalanceTextBox.Name = "AvailibleBalanceTextBox";
            AvailibleBalanceTextBox.Size = new Size(128, 31);
            AvailibleBalanceTextBox.TabIndex = 67;
            // 
            // FitRangePriceTextBox
            // 
            FitRangePriceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FitRangePriceTextBox.BackColor = Color.White;
            FitRangePriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            FitRangePriceTextBox.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            FitRangePriceTextBox.ForeColor = Color.Black;
            FitRangePriceTextBox.Location = new Point(220, 149);
            FitRangePriceTextBox.Margin = new Padding(2);
            FitRangePriceTextBox.Name = "FitRangePriceTextBox";
            FitRangePriceTextBox.Size = new Size(128, 31);
            FitRangePriceTextBox.TabIndex = 68;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1335, 635);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "SettingsForm";
            Text = "Settings";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button ExtendedConsoleButton;
        private Label label12;
        private Button ClearBuyLotsButton;
        private TextBox OrderVolumeTxb;
        private Label label8;
        private TextBox ItemListCountTxb;
        private Button LoadItemListButton;
        private TextBox MaxPriceTxb;
        private Label label6;
        private TextBox MinPriceTxb;
        private Label label5;
        private Button ResetButton;
        private Button SaveConfigurationButton;
        public TextBox MinProfitTextBox;
        private Label label46346;
        private Button UploadSettingsButton;
        private Button SaveSettingsButton;
        private GroupBox groupBox2;
        private Button ResetSettingsButton;
        private TextBox ItemListSizeTextBox;
        private TextBox MaxPriceTextBox;
        private TextBox MinPriceTextBox;
        private GroupBox groupBox3;
        private TextBox SteamCommissionTextBox;
        private TextBox SteamUserIdTextBox;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private GroupBox groupBox1;
        private Label label10;
        private TextBox BuyListingFindRangeTextBox;
        private Label label7;
        private TextBox OrderQuantityTextBox;
        private Label label5235262;
        private Label label3;
        private Label label9;
        private Label label1525;
        private TextBox TrendTextBox;
        private Label label163463;
        private Label label1515236;
        private Label label11;
        private ComboBox AnalysisIntervalComboBox;
        public TextBox SalesPerWeekTextBox;
        public TextBox SellListingFindRangeTextBox;
        private TextBox AveragePriceTextBox;
        public TextBox RequiredProfitTextBox;
        private Label label4;
        public TextBox AvailibleBalanceTextBox;
        public TextBox FitRangePriceTextBox;
    }
}