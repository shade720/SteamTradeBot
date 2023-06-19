namespace SteamTradeBotClient.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            sighUp = new Button();
            label1 = new Label();
            AccountNameLabel = new Label();
            label3 = new Label();
            CoefficientOfSalesTextBox = new Label();
            PlaceOnListingTextBox = new Label();
            MinProfitTextBox = new Label();
            DesiredProfitTextBox = new Label();
            RangeOfPriceTextBox = new Label();
            AvailibleBalanceTextBox = new Label();
            label11 = new Label();
            AnalysisInterval = new ComboBox();
            SalesPerWeekTxb = new TextBox();
            PlaceOnListingTxb = new TextBox();
            CoefficientOfSalesTxb = new TextBox();
            RequredProfitTxb = new TextBox();
            MinProfitTxb = new TextBox();
            AvailableBalanceTxb = new TextBox();
            FitPriceIntervalTxb = new TextBox();
            StartButton = new Button();
            ChangeAccountButton = new Button();
            IsLoggedLabel = new Label();
            label2 = new Label();
            StopButton = new Button();
            MyTimer = new System.Windows.Forms.Timer(components);
            UptimeLabel = new Label();
            TimeOfUptime = new Label();
            SaveConfigurationButton = new Button();
            ResetButton = new Button();
            Restart = new Button();
            Annotation = new ToolTip(components);
            AveragePriceTxb = new TextBox();
            MinPriceTxb = new TextBox();
            MaxPriceTxb = new TextBox();
            ItemListCountTxb = new TextBox();
            OrderVolumeTxb = new TextBox();
            TrendTxb = new TextBox();
            BalanceLabel = new Label();
            Tray = new NotifyIcon(components);
            ToTrayButton = new Button();
            label4 = new Label();
            ClearLogButton = new Button();
            label5 = new Label();
            label6 = new Label();
            LoadItemListButton = new Button();
            EventConsole = new RichTextBox();
            label8 = new Label();
            BalLabel = new Label();
            EntireLabel = new Label();
            EntireBalanceLabel = new Label();
            label7 = new Label();
            ErrorProvider = new ErrorProvider(components);
            ClearBuyLotsButton = new Button();
            IsSDADisabledLabel = new Label();
            label9 = new Label();
            label10 = new Label();
            label12 = new Label();
            ExtendedConsoleButton = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // sighUp
            // 
            sighUp.BackColor = Color.Transparent;
            sighUp.FlatAppearance.BorderSize = 0;
            sighUp.FlatStyle = FlatStyle.Popup;
            sighUp.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            sighUp.ForeColor = SystemColors.GradientInactiveCaption;
            sighUp.Location = new Point(571, 929);
            sighUp.Name = "sighUp";
            sighUp.Size = new Size(86, 32);
            sighUp.TabIndex = 1;
            sighUp.Text = "Войти";
            sighUp.UseVisualStyleBackColor = false;
            sighUp.Click += SighUpButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(568, 852);
            label1.Name = "label1";
            label1.Size = new Size(75, 23);
            label1.TabIndex = 5;
            label1.Text = "Аккаунт:";
            // 
            // AccountNameLabel
            // 
            AccountNameLabel.AutoSize = true;
            AccountNameLabel.BackColor = Color.Transparent;
            AccountNameLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            AccountNameLabel.ForeColor = SystemColors.GradientActiveCaption;
            AccountNameLabel.Location = new Point(639, 852);
            AccountNameLabel.Name = "AccountNameLabel";
            AccountNameLabel.Size = new Size(49, 23);
            AccountNameLabel.TabIndex = 6;
            AccountNameLabel.Text = "none";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(571, 345);
            label3.Name = "label3";
            label3.Size = new Size(177, 23);
            label3.TabIndex = 9;
            label3.Text = "Количество продаж";
            // 
            // CoefficientOfSalesTextBox
            // 
            CoefficientOfSalesTextBox.AutoSize = true;
            CoefficientOfSalesTextBox.BackColor = Color.Transparent;
            CoefficientOfSalesTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            CoefficientOfSalesTextBox.ForeColor = SystemColors.Control;
            CoefficientOfSalesTextBox.Location = new Point(571, 411);
            CoefficientOfSalesTextBox.Name = "CoefficientOfSalesTextBox";
            CoefficientOfSalesTextBox.Size = new Size(195, 23);
            CoefficientOfSalesTextBox.TabIndex = 10;
            CoefficientOfSalesTextBox.Text = "Коэффициент продаж";
            // 
            // PlaceOnListingTextBox
            // 
            PlaceOnListingTextBox.AutoSize = true;
            PlaceOnListingTextBox.BackColor = Color.Transparent;
            PlaceOnListingTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            PlaceOnListingTextBox.ForeColor = SystemColors.Control;
            PlaceOnListingTextBox.Location = new Point(571, 378);
            PlaceOnListingTextBox.Name = "PlaceOnListingTextBox";
            PlaceOnListingTextBox.Size = new Size(155, 23);
            PlaceOnListingTextBox.TabIndex = 11;
            PlaceOnListingTextBox.Text = "Место в листинге";
            // 
            // MinProfitTextBox
            // 
            MinProfitTextBox.AutoSize = true;
            MinProfitTextBox.BackColor = Color.Transparent;
            MinProfitTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            MinProfitTextBox.ForeColor = SystemColors.Control;
            MinProfitTextBox.Location = new Point(571, 444);
            MinProfitTextBox.Name = "MinProfitTextBox";
            MinProfitTextBox.Size = new Size(120, 23);
            MinProfitTextBox.TabIndex = 12;
            MinProfitTextBox.Text = "Мин. профит";
            // 
            // DesiredProfitTextBox
            // 
            DesiredProfitTextBox.AutoSize = true;
            DesiredProfitTextBox.BackColor = Color.Transparent;
            DesiredProfitTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            DesiredProfitTextBox.ForeColor = SystemColors.Control;
            DesiredProfitTextBox.Location = new Point(571, 476);
            DesiredProfitTextBox.Name = "DesiredProfitTextBox";
            DesiredProfitTextBox.Size = new Size(170, 23);
            DesiredProfitTextBox.TabIndex = 13;
            DesiredProfitTextBox.Text = "Желаемый профит";
            // 
            // RangeOfPriceTextBox
            // 
            RangeOfPriceTextBox.AutoSize = true;
            RangeOfPriceTextBox.BackColor = Color.Transparent;
            RangeOfPriceTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            RangeOfPriceTextBox.ForeColor = SystemColors.Control;
            RangeOfPriceTextBox.Location = new Point(571, 542);
            RangeOfPriceTextBox.Name = "RangeOfPriceTextBox";
            RangeOfPriceTextBox.Size = new Size(139, 23);
            RangeOfPriceTextBox.TabIndex = 14;
            RangeOfPriceTextBox.Text = "Диапазон цены";
            // 
            // AvailibleBalanceTextBox
            // 
            AvailibleBalanceTextBox.AutoSize = true;
            AvailibleBalanceTextBox.BackColor = Color.Transparent;
            AvailibleBalanceTextBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            AvailibleBalanceTextBox.ForeColor = SystemColors.Control;
            AvailibleBalanceTextBox.Location = new Point(571, 509);
            AvailibleBalanceTextBox.Name = "AvailibleBalanceTextBox";
            AvailibleBalanceTextBox.Size = new Size(164, 23);
            AvailibleBalanceTextBox.TabIndex = 15;
            AvailibleBalanceTextBox.Text = "Доступный баланс";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = SystemColors.Control;
            label11.Location = new Point(571, 642);
            label11.Name = "label11";
            label11.Size = new Size(146, 23);
            label11.TabIndex = 17;
            label11.Text = "Период анализа";
            // 
            // AnalysisInterval
            // 
            AnalysisInterval.BackColor = SystemColors.GrayText;
            AnalysisInterval.FlatStyle = FlatStyle.Popup;
            AnalysisInterval.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AnalysisInterval.ForeColor = Color.Gold;
            AnalysisInterval.FormattingEnabled = true;
            AnalysisInterval.Items.AddRange(new object[] { "Неделя", "Месяц", " " });
            AnalysisInterval.Location = new Point(771, 637);
            AnalysisInterval.Name = "AnalysisInterval";
            AnalysisInterval.Size = new Size(89, 28);
            AnalysisInterval.TabIndex = 18;
            AnalysisInterval.Text = "Неделя";
            Annotation.SetToolTip(AnalysisInterval, "Приближение графика цены при анализе");
            // 
            // SalesPerWeekTxb
            // 
            SalesPerWeekTxb.BackColor = SystemColors.GrayText;
            SalesPerWeekTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SalesPerWeekTxb.ForeColor = Color.Gold;
            SalesPerWeekTxb.Location = new Point(771, 340);
            SalesPerWeekTxb.Name = "SalesPerWeekTxb";
            SalesPerWeekTxb.Size = new Size(89, 27);
            SalesPerWeekTxb.TabIndex = 19;
            SalesPerWeekTxb.Text = "100";
            Annotation.SetToolTip(SalesPerWeekTxb, "Количество продаж за день\r\n\r\n");
            // 
            // PlaceOnListingTxb
            // 
            PlaceOnListingTxb.BackColor = SystemColors.GrayText;
            PlaceOnListingTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PlaceOnListingTxb.ForeColor = Color.Gold;
            PlaceOnListingTxb.Location = new Point(771, 373);
            PlaceOnListingTxb.Name = "PlaceOnListingTxb";
            PlaceOnListingTxb.Size = new Size(89, 27);
            PlaceOnListingTxb.TabIndex = 19;
            PlaceOnListingTxb.Text = "1";
            Annotation.SetToolTip(PlaceOnListingTxb, "Место предмета в листинге (для выбора цены продажи )");
            // 
            // CoefficientOfSalesTxb
            // 
            CoefficientOfSalesTxb.BackColor = SystemColors.GrayText;
            CoefficientOfSalesTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            CoefficientOfSalesTxb.ForeColor = Color.Gold;
            CoefficientOfSalesTxb.Location = new Point(771, 406);
            CoefficientOfSalesTxb.Name = "CoefficientOfSalesTxb";
            CoefficientOfSalesTxb.Size = new Size(89, 27);
            CoefficientOfSalesTxb.TabIndex = 19;
            CoefficientOfSalesTxb.Text = "1";
            Annotation.SetToolTip(CoefficientOfSalesTxb, "Место в списке цена-продажи.\r\n(чем больше коэффициент, тем ниже будет цена и тем дольше будут покупаться предметы)");
            // 
            // RequredProfitTxb
            // 
            RequredProfitTxb.BackColor = SystemColors.GrayText;
            RequredProfitTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            RequredProfitTxb.ForeColor = Color.Gold;
            RequredProfitTxb.Location = new Point(771, 439);
            RequredProfitTxb.Name = "RequredProfitTxb";
            RequredProfitTxb.Size = new Size(89, 27);
            RequredProfitTxb.TabIndex = 19;
            RequredProfitTxb.Text = "1";
            Annotation.SetToolTip(RequredProfitTxb, "Профит при покупке предмета\r\n");
            // 
            // MinProfitTxb
            // 
            MinProfitTxb.BackColor = SystemColors.GrayText;
            MinProfitTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MinProfitTxb.ForeColor = Color.Gold;
            MinProfitTxb.Location = new Point(771, 472);
            MinProfitTxb.Name = "MinProfitTxb";
            MinProfitTxb.Size = new Size(89, 27);
            MinProfitTxb.TabIndex = 19;
            MinProfitTxb.Text = "0";
            Annotation.SetToolTip(MinProfitTxb, "Добавочная величина к цене продажи предмета\r\n");
            // 
            // AvailableBalanceTxb
            // 
            AvailableBalanceTxb.BackColor = SystemColors.GrayText;
            AvailableBalanceTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AvailableBalanceTxb.ForeColor = Color.Gold;
            AvailableBalanceTxb.Location = new Point(771, 505);
            AvailableBalanceTxb.Name = "AvailableBalanceTxb";
            AvailableBalanceTxb.Size = new Size(89, 27);
            AvailableBalanceTxb.TabIndex = 19;
            AvailableBalanceTxb.Text = "1";
            Annotation.SetToolTip(AvailableBalanceTxb, "Используемый баланс в процентах");
            // 
            // FitPriceIntervalTxb
            // 
            FitPriceIntervalTxb.BackColor = SystemColors.GrayText;
            FitPriceIntervalTxb.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FitPriceIntervalTxb.ForeColor = Color.Gold;
            FitPriceIntervalTxb.Location = new Point(771, 538);
            FitPriceIntervalTxb.Name = "FitPriceIntervalTxb";
            FitPriceIntervalTxb.Size = new Size(89, 27);
            FitPriceIntervalTxb.TabIndex = 19;
            FitPriceIntervalTxb.Text = "0,1";
            Annotation.SetToolTip(FitPriceIntervalTxb, "Диапазон цены, при выходе из которого ордер будет снят");
            // 
            // StartButton
            // 
            StartButton.BackColor = Color.Transparent;
            StartButton.FlatAppearance.BorderSize = 0;
            StartButton.FlatStyle = FlatStyle.Popup;
            StartButton.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            StartButton.ForeColor = Color.Chartreuse;
            StartButton.Location = new Point(1594, 874);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(127, 91);
            StartButton.TabIndex = 20;
            StartButton.Text = "Начать";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartButton_Click;
            // 
            // ChangeAccountButton
            // 
            ChangeAccountButton.BackColor = Color.Transparent;
            ChangeAccountButton.Enabled = false;
            ChangeAccountButton.FlatAppearance.BorderSize = 0;
            ChangeAccountButton.FlatStyle = FlatStyle.Popup;
            ChangeAccountButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ChangeAccountButton.ForeColor = SystemColors.GradientActiveCaption;
            ChangeAccountButton.Location = new Point(571, 929);
            ChangeAccountButton.Name = "ChangeAccountButton";
            ChangeAccountButton.Size = new Size(163, 32);
            ChangeAccountButton.TabIndex = 22;
            ChangeAccountButton.Text = "Выйти из аккаунта";
            ChangeAccountButton.UseVisualStyleBackColor = false;
            ChangeAccountButton.Visible = false;
            ChangeAccountButton.Click += ChangeAccountButton_Click;
            // 
            // IsLoggedLabel
            // 
            IsLoggedLabel.AutoSize = true;
            IsLoggedLabel.BackColor = Color.Transparent;
            IsLoggedLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            IsLoggedLabel.ForeColor = Color.Red;
            IsLoggedLabel.Location = new Point(566, 900);
            IsLoggedLabel.Name = "IsLoggedLabel";
            IsLoggedLabel.Size = new Size(163, 23);
            IsLoggedLabel.TabIndex = 23;
            IsLoggedLabel.Text = "Вход не выполнен";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.GradientActiveCaption;
            label2.Location = new Point(1191, 325);
            label2.Name = "label2";
            label2.Size = new Size(239, 38);
            label2.TabIndex = 24;
            label2.Text = "Консоль событий";
            // 
            // StopButton
            // 
            StopButton.BackColor = Color.Transparent;
            StopButton.Enabled = false;
            StopButton.FlatStyle = FlatStyle.Popup;
            StopButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            StopButton.ForeColor = Color.Red;
            StopButton.Location = new Point(1594, 874);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(127, 51);
            StopButton.TabIndex = 25;
            StopButton.Text = "Остановить\r\n";
            StopButton.UseVisualStyleBackColor = false;
            StopButton.Visible = false;
            StopButton.Click += StopButton_Click;
            // 
            // MyTimer
            // 
            MyTimer.Enabled = true;
            MyTimer.Interval = 1000;
            MyTimer.Tick += MyTimer_Tick;
            // 
            // UptimeLabel
            // 
            UptimeLabel.AutoSize = true;
            UptimeLabel.BackColor = Color.Transparent;
            UptimeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            UptimeLabel.ForeColor = SystemColors.GradientActiveCaption;
            UptimeLabel.Location = new Point(1590, 927);
            UptimeLabel.Name = "UptimeLabel";
            UptimeLabel.Size = new Size(61, 20);
            UptimeLabel.TabIndex = 27;
            UptimeLabel.Text = "Uptime:";
            UptimeLabel.Visible = false;
            // 
            // TimeOfUptime
            // 
            TimeOfUptime.AutoSize = true;
            TimeOfUptime.BackColor = Color.Transparent;
            TimeOfUptime.ForeColor = SystemColors.GradientActiveCaption;
            TimeOfUptime.Location = new Point(1645, 927);
            TimeOfUptime.Name = "TimeOfUptime";
            TimeOfUptime.Size = new Size(0, 20);
            TimeOfUptime.TabIndex = 28;
            // 
            // SaveConfigurationButton
            // 
            SaveConfigurationButton.BackColor = Color.Transparent;
            SaveConfigurationButton.FlatAppearance.BorderSize = 0;
            SaveConfigurationButton.FlatStyle = FlatStyle.Popup;
            SaveConfigurationButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SaveConfigurationButton.ForeColor = SystemColors.GradientActiveCaption;
            SaveConfigurationButton.Location = new Point(570, 677);
            SaveConfigurationButton.Name = "SaveConfigurationButton";
            SaveConfigurationButton.Size = new Size(211, 29);
            SaveConfigurationButton.TabIndex = 29;
            SaveConfigurationButton.Text = "Применить конфигурацию ";
            SaveConfigurationButton.UseVisualStyleBackColor = false;
            SaveConfigurationButton.Click += SaveConfigurationButton_Click;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.Transparent;
            ResetButton.FlatAppearance.BorderSize = 0;
            ResetButton.FlatStyle = FlatStyle.Popup;
            ResetButton.ForeColor = SystemColors.GradientActiveCaption;
            ResetButton.Location = new Point(793, 677);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(67, 29);
            ResetButton.TabIndex = 30;
            ResetButton.Text = "Сброс";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // Restart
            // 
            Restart.BackColor = Color.Transparent;
            Restart.FlatAppearance.BorderSize = 0;
            Restart.FlatStyle = FlatStyle.Popup;
            Restart.ForeColor = SystemColors.GradientActiveCaption;
            Restart.Location = new Point(1564, 310);
            Restart.Name = "Restart";
            Restart.Size = new Size(158, 29);
            Restart.TabIndex = 31;
            Restart.Text = "Перезагрузить";
            Restart.UseVisualStyleBackColor = false;
            // 
            // Annotation
            // 
            Annotation.AutomaticDelay = 100;
            Annotation.AutoPopDelay = 5000;
            Annotation.BackColor = SystemColors.GrayText;
            Annotation.ForeColor = Color.Gold;
            Annotation.InitialDelay = 100;
            Annotation.ReshowDelay = 20;
            Annotation.UseAnimation = false;
            Annotation.UseFading = false;
            // 
            // AveragePriceTxb
            // 
            AveragePriceTxb.BackColor = SystemColors.GrayText;
            AveragePriceTxb.ForeColor = Color.Gold;
            AveragePriceTxb.Location = new Point(771, 571);
            AveragePriceTxb.Name = "AveragePriceTxb";
            AveragePriceTxb.Size = new Size(89, 27);
            AveragePriceTxb.TabIndex = 38;
            AveragePriceTxb.Text = "1,1";
            Annotation.SetToolTip(AveragePriceTxb, "Значимость средней цены при принятии решения покупки\r\n");
            // 
            // MinPriceTxb
            // 
            MinPriceTxb.BackColor = SystemColors.GrayText;
            MinPriceTxb.ForeColor = Color.Gold;
            MinPriceTxb.Location = new Point(1090, 340);
            MinPriceTxb.Name = "MinPriceTxb";
            MinPriceTxb.Size = new Size(89, 27);
            MinPriceTxb.TabIndex = 42;
            MinPriceTxb.Text = "0,05";
            Annotation.SetToolTip(MinPriceTxb, "Минимальная цена в списке предметов (в долларах)");
            // 
            // MaxPriceTxb
            // 
            MaxPriceTxb.BackColor = SystemColors.GrayText;
            MaxPriceTxb.ForeColor = Color.Gold;
            MaxPriceTxb.Location = new Point(1090, 373);
            MaxPriceTxb.Name = "MaxPriceTxb";
            MaxPriceTxb.Size = new Size(89, 27);
            MaxPriceTxb.TabIndex = 42;
            MaxPriceTxb.Text = "1";
            Annotation.SetToolTip(MaxPriceTxb, "Максимальная цена в списке предметов (в долларах)");
            // 
            // ItemListCountTxb
            // 
            ItemListCountTxb.BackColor = SystemColors.GrayText;
            ItemListCountTxb.ForeColor = Color.Gold;
            ItemListCountTxb.Location = new Point(1090, 406);
            ItemListCountTxb.Name = "ItemListCountTxb";
            ItemListCountTxb.Size = new Size(89, 27);
            ItemListCountTxb.TabIndex = 42;
            ItemListCountTxb.Text = "100";
            Annotation.SetToolTip(ItemListCountTxb, "Число предметов в списке");
            // 
            // OrderVolumeTxb
            // 
            OrderVolumeTxb.BackColor = SystemColors.GrayText;
            OrderVolumeTxb.ForeColor = Color.Gold;
            OrderVolumeTxb.Location = new Point(1090, 439);
            OrderVolumeTxb.Name = "OrderVolumeTxb";
            OrderVolumeTxb.Size = new Size(89, 27);
            OrderVolumeTxb.TabIndex = 45;
            OrderVolumeTxb.Text = "3";
            Annotation.SetToolTip(OrderVolumeTxb, "Количество каждого закупаемого предмета");
            // 
            // TrendTxb
            // 
            TrendTxb.BackColor = SystemColors.GrayText;
            TrendTxb.ForeColor = Color.Gold;
            TrendTxb.Location = new Point(771, 604);
            TrendTxb.Name = "TrendTxb";
            TrendTxb.Size = new Size(89, 27);
            TrendTxb.TabIndex = 49;
            TrendTxb.Text = "0,003";
            Annotation.SetToolTip(TrendTxb, "Минимальный возможный коэффициент прямой тренда (тангенс её угла). ");
            // 
            // BalanceLabel
            // 
            BalanceLabel.AutoSize = true;
            BalanceLabel.BackColor = Color.Transparent;
            BalanceLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            BalanceLabel.ForeColor = SystemColors.GradientActiveCaption;
            BalanceLabel.Location = new Point(719, 802);
            BalanceLabel.Name = "BalanceLabel";
            BalanceLabel.Size = new Size(0, 23);
            BalanceLabel.TabIndex = 35;
            // 
            // Tray
            // 
            Tray.Icon = (Icon)resources.GetObject("Tray.Icon");
            Tray.Text = "TradeBot";
            Tray.Visible = true;
            Tray.MouseDoubleClick += Tray_MouseDoubleClick;
            // 
            // ToTrayButton
            // 
            ToTrayButton.BackColor = Color.Transparent;
            ToTrayButton.FlatStyle = FlatStyle.Popup;
            ToTrayButton.ForeColor = SystemColors.GradientActiveCaption;
            ToTrayButton.Location = new Point(1564, 281);
            ToTrayButton.Name = "ToTrayButton";
            ToTrayButton.Size = new Size(158, 29);
            ToTrayButton.TabIndex = 36;
            ToTrayButton.Text = "Свернуть в трей";
            ToTrayButton.UseVisualStyleBackColor = false;
            ToTrayButton.Click += ToTrayButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(571, 575);
            label4.Name = "label4";
            label4.Size = new Size(126, 23);
            label4.TabIndex = 37;
            label4.Text = "Средняя цена";
            // 
            // ClearLogButton
            // 
            ClearLogButton.BackColor = Color.Transparent;
            ClearLogButton.FlatStyle = FlatStyle.Popup;
            ClearLogButton.ForeColor = SystemColors.GradientActiveCaption;
            ClearLogButton.Location = new Point(1564, 339);
            ClearLogButton.Name = "ClearLogButton";
            ClearLogButton.Size = new Size(158, 29);
            ClearLogButton.TabIndex = 39;
            ClearLogButton.Text = "Очистить консоль";
            ClearLogButton.UseVisualStyleBackColor = false;
            ClearLogButton.Click += ClearLogButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(880, 344);
            label5.Name = "label5";
            label5.Size = new Size(173, 23);
            label5.TabIndex = 41;
            label5.Text = "Минимальная цена";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.Control;
            label6.Location = new Point(880, 377);
            label6.Name = "label6";
            label6.Size = new Size(179, 23);
            label6.TabIndex = 41;
            label6.Text = "Максимальная цена";
            // 
            // LoadItemListButton
            // 
            LoadItemListButton.BackColor = Color.Transparent;
            LoadItemListButton.FlatStyle = FlatStyle.Popup;
            LoadItemListButton.Location = new Point(880, 476);
            LoadItemListButton.Name = "LoadItemListButton";
            LoadItemListButton.Size = new Size(226, 29);
            LoadItemListButton.TabIndex = 43;
            LoadItemListButton.Text = "Загрузить список предметов";
            LoadItemListButton.UseVisualStyleBackColor = false;
            LoadItemListButton.Click += LoadItemListButton_Click;
            // 
            // EventConsole
            // 
            EventConsole.BackColor = SystemColors.GrayText;
            EventConsole.BorderStyle = BorderStyle.FixedSingle;
            EventConsole.Cursor = Cursors.IBeam;
            EventConsole.ForeColor = Color.Gold;
            EventConsole.Location = new Point(1200, 373);
            EventConsole.Name = "EventConsole";
            EventConsole.ReadOnly = true;
            EventConsole.ScrollBars = RichTextBoxScrollBars.Vertical;
            EventConsole.Size = new Size(522, 440);
            EventConsole.TabIndex = 44;
            EventConsole.Text = "";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.Control;
            label8.Location = new Point(880, 410);
            label8.Name = "label8";
            label8.Size = new Size(203, 23);
            label8.TabIndex = 41;
            label8.Text = "Количество предметов\r\n";
            // 
            // BalLabel
            // 
            BalLabel.AutoSize = true;
            BalLabel.BackColor = Color.Transparent;
            BalLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            BalLabel.ForeColor = SystemColors.GradientActiveCaption;
            BalLabel.Location = new Point(568, 802);
            BalLabel.Name = "BalLabel";
            BalLabel.Size = new Size(150, 23);
            BalLabel.TabIndex = 6;
            BalLabel.Text = "Баланс кошелька:";
            BalLabel.Visible = false;
            // 
            // EntireLabel
            // 
            EntireLabel.AutoSize = true;
            EntireLabel.BackColor = Color.Transparent;
            EntireLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            EntireLabel.ForeColor = SystemColors.GradientActiveCaption;
            EntireLabel.Location = new Point(568, 826);
            EntireLabel.Name = "EntireLabel";
            EntireLabel.Size = new Size(131, 23);
            EntireLabel.TabIndex = 6;
            EntireLabel.Text = "Общий баланс:";
            EntireLabel.Visible = false;
            // 
            // EntireBalanceLabel
            // 
            EntireBalanceLabel.AutoSize = true;
            EntireBalanceLabel.BackColor = Color.Transparent;
            EntireBalanceLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            EntireBalanceLabel.ForeColor = SystemColors.GradientActiveCaption;
            EntireBalanceLabel.Location = new Point(719, 826);
            EntireBalanceLabel.Name = "EntireBalanceLabel";
            EntireBalanceLabel.Size = new Size(0, 23);
            EntireBalanceLabel.TabIndex = 35;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.Control;
            label7.Location = new Point(880, 443);
            label7.Name = "label7";
            label7.Size = new Size(142, 23);
            label7.TabIndex = 46;
            label7.Text = "Размер закупки";
            // 
            // ErrorProvider
            // 
            ErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            ErrorProvider.ContainerControl = this;
            // 
            // ClearBuyLotsButton
            // 
            ClearBuyLotsButton.BackColor = Color.Transparent;
            ClearBuyLotsButton.FlatStyle = FlatStyle.Popup;
            ClearBuyLotsButton.Location = new Point(880, 511);
            ClearBuyLotsButton.Name = "ClearBuyLotsButton";
            ClearBuyLotsButton.Size = new Size(226, 29);
            ClearBuyLotsButton.TabIndex = 47;
            ClearBuyLotsButton.Text = "Очистить запросы на покупку";
            ClearBuyLotsButton.UseVisualStyleBackColor = false;
            ClearBuyLotsButton.Click += ClearBuyLotsButton_Click;
            // 
            // IsSDADisabledLabel
            // 
            IsSDADisabledLabel.AutoSize = true;
            IsSDADisabledLabel.BackColor = Color.Transparent;
            IsSDADisabledLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            IsSDADisabledLabel.ForeColor = Color.Red;
            IsSDADisabledLabel.Location = new Point(567, 877);
            IsSDADisabledLabel.Name = "IsSDADisabledLabel";
            IsSDADisabledLabel.Size = new Size(138, 23);
            IsSDADisabledLabel.TabIndex = 48;
            IsSDADisabledLabel.Text = "SDA выключен";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.Control;
            label9.Location = new Point(571, 608);
            label9.Name = "label9";
            label9.Size = new Size(109, 23);
            label9.TabIndex = 50;
            label9.Text = "Тренд цены";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = SystemColors.ControlLight;
            label10.Location = new Point(569, 291);
            label10.Name = "label10";
            label10.Size = new Size(193, 28);
            label10.TabIndex = 51;
            label10.Text = "Настройки логики";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label12.ForeColor = SystemColors.ControlLight;
            label12.Location = new Point(878, 291);
            label12.Name = "label12";
            label12.Size = new Size(238, 28);
            label12.TabIndex = 52;
            label12.Text = "Формирование списка";
            // 
            // ExtendedConsoleButton
            // 
            ExtendedConsoleButton.BackColor = Color.Transparent;
            ExtendedConsoleButton.FlatStyle = FlatStyle.Popup;
            ExtendedConsoleButton.Location = new Point(880, 546);
            ExtendedConsoleButton.Name = "ExtendedConsoleButton";
            ExtendedConsoleButton.Size = new Size(173, 29);
            ExtendedConsoleButton.TabIndex = 53;
            ExtendedConsoleButton.Text = "Расширенная консоль";
            ExtendedConsoleButton.UseVisualStyleBackColor = false;
            ExtendedConsoleButton.Click += ExtendedConsoleButton_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(269, 1037);
            panel1.TabIndex = 54;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1797, 1036);
            Controls.Add(panel1);
            Controls.Add(ExtendedConsoleButton);
            Controls.Add(label12);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(TrendTxb);
            Controls.Add(IsSDADisabledLabel);
            Controls.Add(ClearBuyLotsButton);
            Controls.Add(label7);
            Controls.Add(OrderVolumeTxb);
            Controls.Add(EntireBalanceLabel);
            Controls.Add(EntireLabel);
            Controls.Add(BalLabel);
            Controls.Add(label8);
            Controls.Add(ItemListCountTxb);
            Controls.Add(EventConsole);
            Controls.Add(LoadItemListButton);
            Controls.Add(MaxPriceTxb);
            Controls.Add(label6);
            Controls.Add(MinPriceTxb);
            Controls.Add(label5);
            Controls.Add(ClearLogButton);
            Controls.Add(AveragePriceTxb);
            Controls.Add(label4);
            Controls.Add(ToTrayButton);
            Controls.Add(BalanceLabel);
            Controls.Add(Restart);
            Controls.Add(ResetButton);
            Controls.Add(SaveConfigurationButton);
            Controls.Add(TimeOfUptime);
            Controls.Add(UptimeLabel);
            Controls.Add(StopButton);
            Controls.Add(label2);
            Controls.Add(IsLoggedLabel);
            Controls.Add(ChangeAccountButton);
            Controls.Add(StartButton);
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
            Controls.Add(AccountNameLabel);
            Controls.Add(label1);
            Controls.Add(sighUp);
            DoubleBuffered = true;
            ForeColor = SystemColors.GradientActiveCaption;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TradeBot";
            Load += TradeBotAPI_Load;
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void EventConsole_TextChanged1(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CoefficientOfSalesTextBox;
        private System.Windows.Forms.Label PlaceOnListingTextBox;
        private System.Windows.Forms.Label MinProfitTextBox;
        private System.Windows.Forms.Label DesiredProfitTextBox;
        private System.Windows.Forms.Label RangeOfPriceTextBox;
        private System.Windows.Forms.Label AvailibleBalanceTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox AnalysisInterval;
        private System.Windows.Forms.Button StartButton;
        public System.Windows.Forms.Button sighUp;
        public System.Windows.Forms.Label AccountNameLabel;
        public System.Windows.Forms.Button ChangeAccountButton;
        public System.Windows.Forms.Label IsLoggedLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label UptimeLabel;
        public System.Windows.Forms.TextBox PlaceOnListingTxb;
        public System.Windows.Forms.TextBox CoefficientOfSalesTxb;
        public System.Windows.Forms.TextBox RequredProfitTxb;
        public System.Windows.Forms.TextBox MinProfitTxb;
        public System.Windows.Forms.TextBox AvailableBalanceTxb;
        public System.Windows.Forms.TextBox FitPriceIntervalTxb;
        private System.Windows.Forms.Label TimeOfUptime;
        private System.Windows.Forms.Button SaveConfigurationButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.ToolTip Annotation;
        public System.Windows.Forms.TextBox SalesPerWeekTxb;
        public System.Windows.Forms.Label BalanceLabel;
        private System.Windows.Forms.NotifyIcon Tray;
        private System.Windows.Forms.Button ToTrayButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AveragePriceTxb;
        private System.Windows.Forms.Button ClearLogButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MinPriceTxb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MaxPriceTxb;
        private System.Windows.Forms.Button LoadItemListButton;
        public System.Windows.Forms.RichTextBox EventConsole;
        private System.Windows.Forms.TextBox ItemListCountTxb;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label EntireBalanceLabel;
        public System.Windows.Forms.Label BalLabel;
        public System.Windows.Forms.Label EntireLabel;
        private System.Windows.Forms.TextBox OrderVolumeTxb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.Button ClearBuyLotsButton;
        private System.Windows.Forms.Label IsSDADisabledLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TrendTxb;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button ExtendedConsoleButton;
        private System.Windows.Forms.Timer MyTimer;
        private Panel panel1;
    }
}

