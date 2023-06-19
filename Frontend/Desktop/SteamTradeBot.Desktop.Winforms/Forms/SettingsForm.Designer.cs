namespace SteamTradeBot.Desktop.Winforms.Forms
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
            ExtendedConsoleButton = new Button();
            label12 = new Label();
            label10 = new Label();
            label9 = new Label();
            TrendTxb = new TextBox();
            ClearBuyLotsButton = new Button();
            label7 = new Label();
            OrderVolumeTxb = new TextBox();
            label8 = new Label();
            ItemListCountTxb = new TextBox();
            LoadItemListButton = new Button();
            MaxPriceTxb = new TextBox();
            label6 = new Label();
            MinPriceTxb = new TextBox();
            label5 = new Label();
            AveragePriceTxb = new TextBox();
            label4 = new Label();
            ResetButton = new Button();
            SaveConfigurationButton = new Button();
            FitPriceIntervalTxb = new TextBox();
            AvailableBalanceTxb = new TextBox();
            MinProfitTxb = new TextBox();
            RequredProfitTxb = new TextBox();
            CoefficientOfSalesTxb = new TextBox();
            PlaceOnListingTxb = new TextBox();
            SalesPerWeekTxb = new TextBox();
            AnalysisInterval = new ComboBox();
            label11 = new Label();
            AvailibleBalanceTextBox = new Label();
            RangeOfPriceTextBox = new Label();
            DesiredProfitTextBox = new Label();
            MinProfitTextBox = new Label();
            PlaceOnListingTextBox = new Label();
            CoefficientOfSalesTextBox = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // ExtendedConsoleButton
            // 
            ExtendedConsoleButton.BackColor = Color.Transparent;
            ExtendedConsoleButton.FlatStyle = FlatStyle.Popup;
            ExtendedConsoleButton.Location = new Point(537, 434);
            ExtendedConsoleButton.Name = "ExtendedConsoleButton";
            ExtendedConsoleButton.Size = new Size(173, 29);
            ExtendedConsoleButton.TabIndex = 88;
            ExtendedConsoleButton.Text = "Расширенная консоль";
            ExtendedConsoleButton.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label12.ForeColor = SystemColors.ControlLight;
            label12.Location = new Point(535, 179);
            label12.Name = "label12";
            label12.Size = new Size(238, 28);
            label12.TabIndex = 87;
            label12.Text = "Формирование списка";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = SystemColors.ControlLight;
            label10.Location = new Point(226, 179);
            label10.Name = "label10";
            label10.Size = new Size(193, 28);
            label10.TabIndex = 86;
            label10.Text = "Настройки логики";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.Control;
            label9.Location = new Point(228, 496);
            label9.Name = "label9";
            label9.Size = new Size(109, 23);
            label9.TabIndex = 85;
            label9.Text = "Тренд цены";
            // 
            // TrendTxb
            // 
            TrendTxb.BackColor = SystemColors.GrayText;
            TrendTxb.ForeColor = Color.Gold;
            TrendTxb.Location = new Point(428, 492);
            TrendTxb.Name = "TrendTxb";
            TrendTxb.Size = new Size(89, 27);
            TrendTxb.TabIndex = 84;
            TrendTxb.Text = "0,003";
            // 
            // ClearBuyLotsButton
            // 
            ClearBuyLotsButton.BackColor = Color.Transparent;
            ClearBuyLotsButton.FlatStyle = FlatStyle.Popup;
            ClearBuyLotsButton.Location = new Point(537, 399);
            ClearBuyLotsButton.Name = "ClearBuyLotsButton";
            ClearBuyLotsButton.Size = new Size(226, 29);
            ClearBuyLotsButton.TabIndex = 83;
            ClearBuyLotsButton.Text = "Очистить запросы на покупку";
            ClearBuyLotsButton.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.Control;
            label7.Location = new Point(537, 331);
            label7.Name = "label7";
            label7.Size = new Size(142, 23);
            label7.TabIndex = 82;
            label7.Text = "Размер закупки";
            // 
            // OrderVolumeTxb
            // 
            OrderVolumeTxb.BackColor = SystemColors.GrayText;
            OrderVolumeTxb.ForeColor = Color.Gold;
            OrderVolumeTxb.Location = new Point(747, 327);
            OrderVolumeTxb.Name = "OrderVolumeTxb";
            OrderVolumeTxb.Size = new Size(89, 27);
            OrderVolumeTxb.TabIndex = 81;
            OrderVolumeTxb.Text = "3";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.Control;
            label8.Location = new Point(537, 298);
            label8.Name = "label8";
            label8.Size = new Size(203, 23);
            label8.TabIndex = 76;
            label8.Text = "Количество предметов\r\n";
            // 
            // ItemListCountTxb
            // 
            ItemListCountTxb.BackColor = SystemColors.GrayText;
            ItemListCountTxb.ForeColor = Color.Gold;
            ItemListCountTxb.Location = new Point(747, 294);
            ItemListCountTxb.Name = "ItemListCountTxb";
            ItemListCountTxb.Size = new Size(89, 27);
            ItemListCountTxb.TabIndex = 79;
            ItemListCountTxb.Text = "100";
            // 
            // LoadItemListButton
            // 
            LoadItemListButton.BackColor = Color.Transparent;
            LoadItemListButton.FlatStyle = FlatStyle.Popup;
            LoadItemListButton.Location = new Point(537, 364);
            LoadItemListButton.Name = "LoadItemListButton";
            LoadItemListButton.Size = new Size(226, 29);
            LoadItemListButton.TabIndex = 80;
            LoadItemListButton.Text = "Загрузить список предметов";
            LoadItemListButton.UseVisualStyleBackColor = false;
            // 
            // MaxPriceTxb
            // 
            MaxPriceTxb.BackColor = SystemColors.GrayText;
            MaxPriceTxb.ForeColor = Color.Gold;
            MaxPriceTxb.Location = new Point(747, 261);
            MaxPriceTxb.Name = "MaxPriceTxb";
            MaxPriceTxb.Size = new Size(89, 27);
            MaxPriceTxb.TabIndex = 78;
            MaxPriceTxb.Text = "1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.Control;
            label6.Location = new Point(537, 265);
            label6.Name = "label6";
            label6.Size = new Size(179, 23);
            label6.TabIndex = 75;
            label6.Text = "Максимальная цена";
            // 
            // MinPriceTxb
            // 
            MinPriceTxb.BackColor = SystemColors.GrayText;
            MinPriceTxb.ForeColor = Color.Gold;
            MinPriceTxb.Location = new Point(747, 228);
            MinPriceTxb.Name = "MinPriceTxb";
            MinPriceTxb.Size = new Size(89, 27);
            MinPriceTxb.TabIndex = 77;
            MinPriceTxb.Text = "0,05";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(537, 232);
            label5.Name = "label5";
            label5.Size = new Size(173, 23);
            label5.TabIndex = 74;
            label5.Text = "Минимальная цена";
            // 
            // AveragePriceTxb
            // 
            AveragePriceTxb.BackColor = SystemColors.GrayText;
            AveragePriceTxb.ForeColor = Color.Gold;
            AveragePriceTxb.Location = new Point(428, 459);
            AveragePriceTxb.Name = "AveragePriceTxb";
            AveragePriceTxb.Size = new Size(89, 27);
            AveragePriceTxb.TabIndex = 73;
            AveragePriceTxb.Text = "1,1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(228, 463);
            label4.Name = "label4";
            label4.Size = new Size(126, 23);
            label4.TabIndex = 72;
            label4.Text = "Средняя цена";
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.Transparent;
            ResetButton.FlatAppearance.BorderSize = 0;
            ResetButton.FlatStyle = FlatStyle.Popup;
            ResetButton.ForeColor = SystemColors.GradientActiveCaption;
            ResetButton.Location = new Point(450, 565);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(67, 29);
            ResetButton.TabIndex = 71;
            ResetButton.Text = "Сброс";
            ResetButton.UseVisualStyleBackColor = false;
            // 
            // SaveConfigurationButton
            // 
            SaveConfigurationButton.BackColor = Color.Transparent;
            SaveConfigurationButton.FlatAppearance.BorderSize = 0;
            SaveConfigurationButton.FlatStyle = FlatStyle.Popup;
            SaveConfigurationButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SaveConfigurationButton.ForeColor = SystemColors.GradientActiveCaption;
            SaveConfigurationButton.Location = new Point(227, 565);
            SaveConfigurationButton.Name = "SaveConfigurationButton";
            SaveConfigurationButton.Size = new Size(211, 29);
            SaveConfigurationButton.TabIndex = 70;
            SaveConfigurationButton.Text = "Применить конфигурацию ";
            SaveConfigurationButton.UseVisualStyleBackColor = false;
            // 
            // FitPriceIntervalTxb
            // 
            FitPriceIntervalTxb.BackColor = SystemColors.GrayText;
            FitPriceIntervalTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FitPriceIntervalTxb.ForeColor = Color.Gold;
            FitPriceIntervalTxb.Location = new Point(428, 426);
            FitPriceIntervalTxb.Name = "FitPriceIntervalTxb";
            FitPriceIntervalTxb.Size = new Size(89, 27);
            FitPriceIntervalTxb.TabIndex = 68;
            FitPriceIntervalTxb.Text = "0,1";
            // 
            // AvailableBalanceTxb
            // 
            AvailableBalanceTxb.BackColor = SystemColors.GrayText;
            AvailableBalanceTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AvailableBalanceTxb.ForeColor = Color.Gold;
            AvailableBalanceTxb.Location = new Point(428, 393);
            AvailableBalanceTxb.Name = "AvailableBalanceTxb";
            AvailableBalanceTxb.Size = new Size(89, 27);
            AvailableBalanceTxb.TabIndex = 67;
            AvailableBalanceTxb.Text = "1";
            // 
            // MinProfitTxb
            // 
            MinProfitTxb.BackColor = SystemColors.GrayText;
            MinProfitTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MinProfitTxb.ForeColor = Color.Gold;
            MinProfitTxb.Location = new Point(428, 360);
            MinProfitTxb.Name = "MinProfitTxb";
            MinProfitTxb.Size = new Size(89, 27);
            MinProfitTxb.TabIndex = 66;
            MinProfitTxb.Text = "0";
            // 
            // RequredProfitTxb
            // 
            RequredProfitTxb.BackColor = SystemColors.GrayText;
            RequredProfitTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            RequredProfitTxb.ForeColor = Color.Gold;
            RequredProfitTxb.Location = new Point(428, 327);
            RequredProfitTxb.Name = "RequredProfitTxb";
            RequredProfitTxb.Size = new Size(89, 27);
            RequredProfitTxb.TabIndex = 65;
            RequredProfitTxb.Text = "1";
            // 
            // CoefficientOfSalesTxb
            // 
            CoefficientOfSalesTxb.BackColor = SystemColors.GrayText;
            CoefficientOfSalesTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            CoefficientOfSalesTxb.ForeColor = Color.Gold;
            CoefficientOfSalesTxb.Location = new Point(428, 294);
            CoefficientOfSalesTxb.Name = "CoefficientOfSalesTxb";
            CoefficientOfSalesTxb.Size = new Size(89, 27);
            CoefficientOfSalesTxb.TabIndex = 64;
            CoefficientOfSalesTxb.Text = "1";
            // 
            // PlaceOnListingTxb
            // 
            PlaceOnListingTxb.BackColor = SystemColors.GrayText;
            PlaceOnListingTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PlaceOnListingTxb.ForeColor = Color.Gold;
            PlaceOnListingTxb.Location = new Point(428, 261);
            PlaceOnListingTxb.Name = "PlaceOnListingTxb";
            PlaceOnListingTxb.Size = new Size(89, 27);
            PlaceOnListingTxb.TabIndex = 63;
            PlaceOnListingTxb.Text = "1";
            // 
            // SalesPerWeekTxb
            // 
            SalesPerWeekTxb.BackColor = SystemColors.GrayText;
            SalesPerWeekTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SalesPerWeekTxb.ForeColor = Color.Gold;
            SalesPerWeekTxb.Location = new Point(428, 228);
            SalesPerWeekTxb.Name = "SalesPerWeekTxb";
            SalesPerWeekTxb.Size = new Size(89, 27);
            SalesPerWeekTxb.TabIndex = 69;
            SalesPerWeekTxb.Text = "100";
            // 
            // AnalysisInterval
            // 
            AnalysisInterval.BackColor = SystemColors.GrayText;
            AnalysisInterval.FlatStyle = FlatStyle.Popup;
            AnalysisInterval.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AnalysisInterval.ForeColor = Color.Gold;
            AnalysisInterval.FormattingEnabled = true;
            AnalysisInterval.Items.AddRange(new object[] { "Неделя", "Месяц", " " });
            AnalysisInterval.Location = new Point(428, 525);
            AnalysisInterval.Name = "AnalysisInterval";
            AnalysisInterval.Size = new Size(89, 28);
            AnalysisInterval.TabIndex = 62;
            AnalysisInterval.Text = "Неделя";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = SystemColors.Control;
            label11.Location = new Point(228, 530);
            label11.Name = "label11";
            label11.Size = new Size(146, 23);
            label11.TabIndex = 61;
            label11.Text = "Период анализа";
            // 
            // AvailibleBalanceTextBox
            // 
            AvailibleBalanceTextBox.AutoSize = true;
            AvailibleBalanceTextBox.BackColor = Color.Transparent;
            AvailibleBalanceTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            AvailibleBalanceTextBox.ForeColor = SystemColors.Control;
            AvailibleBalanceTextBox.Location = new Point(228, 397);
            AvailibleBalanceTextBox.Name = "AvailibleBalanceTextBox";
            AvailibleBalanceTextBox.Size = new Size(164, 23);
            AvailibleBalanceTextBox.TabIndex = 60;
            AvailibleBalanceTextBox.Text = "Доступный баланс";
            // 
            // RangeOfPriceTextBox
            // 
            RangeOfPriceTextBox.AutoSize = true;
            RangeOfPriceTextBox.BackColor = Color.Transparent;
            RangeOfPriceTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            RangeOfPriceTextBox.ForeColor = SystemColors.Control;
            RangeOfPriceTextBox.Location = new Point(228, 430);
            RangeOfPriceTextBox.Name = "RangeOfPriceTextBox";
            RangeOfPriceTextBox.Size = new Size(139, 23);
            RangeOfPriceTextBox.TabIndex = 59;
            RangeOfPriceTextBox.Text = "Диапазон цены";
            // 
            // DesiredProfitTextBox
            // 
            DesiredProfitTextBox.AutoSize = true;
            DesiredProfitTextBox.BackColor = Color.Transparent;
            DesiredProfitTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            DesiredProfitTextBox.ForeColor = SystemColors.Control;
            DesiredProfitTextBox.Location = new Point(228, 364);
            DesiredProfitTextBox.Name = "DesiredProfitTextBox";
            DesiredProfitTextBox.Size = new Size(170, 23);
            DesiredProfitTextBox.TabIndex = 58;
            DesiredProfitTextBox.Text = "Желаемый профит";
            // 
            // MinProfitTextBox
            // 
            MinProfitTextBox.AutoSize = true;
            MinProfitTextBox.BackColor = Color.Transparent;
            MinProfitTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            MinProfitTextBox.ForeColor = SystemColors.Control;
            MinProfitTextBox.Location = new Point(228, 332);
            MinProfitTextBox.Name = "MinProfitTextBox";
            MinProfitTextBox.Size = new Size(120, 23);
            MinProfitTextBox.TabIndex = 57;
            MinProfitTextBox.Text = "Мин. профит";
            // 
            // PlaceOnListingTextBox
            // 
            PlaceOnListingTextBox.AutoSize = true;
            PlaceOnListingTextBox.BackColor = Color.Transparent;
            PlaceOnListingTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            PlaceOnListingTextBox.ForeColor = SystemColors.Control;
            PlaceOnListingTextBox.Location = new Point(228, 266);
            PlaceOnListingTextBox.Name = "PlaceOnListingTextBox";
            PlaceOnListingTextBox.Size = new Size(155, 23);
            PlaceOnListingTextBox.TabIndex = 56;
            PlaceOnListingTextBox.Text = "Место в листинге";
            // 
            // CoefficientOfSalesTextBox
            // 
            CoefficientOfSalesTextBox.AutoSize = true;
            CoefficientOfSalesTextBox.BackColor = Color.Transparent;
            CoefficientOfSalesTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            CoefficientOfSalesTextBox.ForeColor = SystemColors.Control;
            CoefficientOfSalesTextBox.Location = new Point(228, 299);
            CoefficientOfSalesTextBox.Name = "CoefficientOfSalesTextBox";
            CoefficientOfSalesTextBox.Size = new Size(195, 23);
            CoefficientOfSalesTextBox.TabIndex = 55;
            CoefficientOfSalesTextBox.Text = "Коэффициент продаж";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(228, 233);
            label3.Name = "label3";
            label3.Size = new Size(177, 23);
            label3.TabIndex = 54;
            label3.Text = "Количество продаж";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 773);
            Controls.Add(ExtendedConsoleButton);
            Controls.Add(label12);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(TrendTxb);
            Controls.Add(ClearBuyLotsButton);
            Controls.Add(label7);
            Controls.Add(OrderVolumeTxb);
            Controls.Add(label8);
            Controls.Add(ItemListCountTxb);
            Controls.Add(LoadItemListButton);
            Controls.Add(MaxPriceTxb);
            Controls.Add(label6);
            Controls.Add(MinPriceTxb);
            Controls.Add(label5);
            Controls.Add(AveragePriceTxb);
            Controls.Add(label4);
            Controls.Add(ResetButton);
            Controls.Add(SaveConfigurationButton);
            Controls.Add(FitPriceIntervalTxb);
            Controls.Add(AvailableBalanceTxb);
            Controls.Add(MinProfitTxb);
            Controls.Add(RequredProfitTxb);
            Controls.Add(CoefficientOfSalesTxb);
            Controls.Add(PlaceOnListingTxb);
            Controls.Add(SalesPerWeekTxb);
            Controls.Add(AnalysisInterval);
            Controls.Add(label11);
            Controls.Add(AvailibleBalanceTextBox);
            Controls.Add(RangeOfPriceTextBox);
            Controls.Add(DesiredProfitTextBox);
            Controls.Add(MinProfitTextBox);
            Controls.Add(PlaceOnListingTextBox);
            Controls.Add(CoefficientOfSalesTextBox);
            Controls.Add(label3);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Settings";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ExtendedConsoleButton;
        private Label label12;
        private Label label10;
        private Label label9;
        private TextBox TrendTxb;
        private Button ClearBuyLotsButton;
        private Label label7;
        private TextBox OrderVolumeTxb;
        private Label label8;
        private TextBox ItemListCountTxb;
        private Button LoadItemListButton;
        private TextBox MaxPriceTxb;
        private Label label6;
        private TextBox MinPriceTxb;
        private Label label5;
        private TextBox AveragePriceTxb;
        private Label label4;
        private Button ResetButton;
        private Button SaveConfigurationButton;
        public TextBox FitPriceIntervalTxb;
        public TextBox AvailableBalanceTxb;
        public TextBox MinProfitTxb;
        public TextBox RequredProfitTxb;
        public TextBox CoefficientOfSalesTxb;
        public TextBox PlaceOnListingTxb;
        public TextBox SalesPerWeekTxb;
        private ComboBox AnalysisInterval;
        private Label label11;
        private Label AvailibleBalanceTextBox;
        private Label RangeOfPriceTextBox;
        private Label DesiredProfitTextBox;
        private Label MinProfitTextBox;
        private Label PlaceOnListingTextBox;
        private Label CoefficientOfSalesTextBox;
        private Label label3;
    }
}