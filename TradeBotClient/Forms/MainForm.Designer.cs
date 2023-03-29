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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sighUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AccountNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CoefficientOfSalesTextBox = new System.Windows.Forms.Label();
            this.PlaceOnListingTextBox = new System.Windows.Forms.Label();
            this.MinProfitTextBox = new System.Windows.Forms.Label();
            this.DesiredProfitTextBox = new System.Windows.Forms.Label();
            this.RangeOfPriceTextBox = new System.Windows.Forms.Label();
            this.AvailibleBalanceTextBox = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.AnalysisInterval = new System.Windows.Forms.ComboBox();
            this.SalesPerWeekTxb = new System.Windows.Forms.TextBox();
            this.PlaceOnListingTxb = new System.Windows.Forms.TextBox();
            this.CoefficientOfSalesTxb = new System.Windows.Forms.TextBox();
            this.RequredProfitTxb = new System.Windows.Forms.TextBox();
            this.MinProfitTxb = new System.Windows.Forms.TextBox();
            this.AvailableBalanceTxb = new System.Windows.Forms.TextBox();
            this.FitPriceIntervalTxb = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.ChangeAccountButton = new System.Windows.Forms.Button();
            this.IsLoggedLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.MyTimer = new System.Windows.Forms.Timer(this.components);
            this.UptimeLabel = new System.Windows.Forms.Label();
            this.TimeOfUptime = new System.Windows.Forms.Label();
            this.SaveConfigurationButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.Restart = new System.Windows.Forms.Button();
            this.Annotation = new System.Windows.Forms.ToolTip(this.components);
            this.AveragePriceTxb = new System.Windows.Forms.TextBox();
            this.MinPriceTxb = new System.Windows.Forms.TextBox();
            this.MaxPriceTxb = new System.Windows.Forms.TextBox();
            this.ItemListCountTxb = new System.Windows.Forms.TextBox();
            this.OrderVolumeTxb = new System.Windows.Forms.TextBox();
            this.TrendTxb = new System.Windows.Forms.TextBox();
            this.BalanceLabel = new System.Windows.Forms.Label();
            this.Tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ToTrayButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LoadItemListButton = new System.Windows.Forms.Button();
            this.EventConsole = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BalLabel = new System.Windows.Forms.Label();
            this.EntireLabel = new System.Windows.Forms.Label();
            this.EntireBalanceLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ClearBuyLotsButton = new System.Windows.Forms.Button();
            this.IsSDADisabledLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ExtendedConsoleButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // sighUp
            // 
            this.sighUp.BackColor = System.Drawing.Color.Transparent;
            this.sighUp.FlatAppearance.BorderSize = 0;
            this.sighUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sighUp.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sighUp.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.sighUp.Location = new System.Drawing.Point(19, 647);
            this.sighUp.Name = "sighUp";
            this.sighUp.Size = new System.Drawing.Size(86, 32);
            this.sighUp.TabIndex = 1;
            this.sighUp.Text = "Войти";
            this.sighUp.UseVisualStyleBackColor = false;
            this.sighUp.Click += new System.EventHandler(this.SighUpButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(16, 570);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Аккаунт:";
            // 
            // AccountNameLabel
            // 
            this.AccountNameLabel.AutoSize = true;
            this.AccountNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.AccountNameLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AccountNameLabel.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.AccountNameLabel.Location = new System.Drawing.Point(87, 570);
            this.AccountNameLabel.Name = "AccountNameLabel";
            this.AccountNameLabel.Size = new System.Drawing.Size(49, 23);
            this.AccountNameLabel.TabIndex = 6;
            this.AccountNameLabel.Text = "none";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(19, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Количество продаж";
            // 
            // CoefficientOfSalesTextBox
            // 
            this.CoefficientOfSalesTextBox.AutoSize = true;
            this.CoefficientOfSalesTextBox.BackColor = System.Drawing.Color.Transparent;
            this.CoefficientOfSalesTextBox.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CoefficientOfSalesTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.CoefficientOfSalesTextBox.Location = new System.Drawing.Point(19, 129);
            this.CoefficientOfSalesTextBox.Name = "CoefficientOfSalesTextBox";
            this.CoefficientOfSalesTextBox.Size = new System.Drawing.Size(195, 23);
            this.CoefficientOfSalesTextBox.TabIndex = 10;
            this.CoefficientOfSalesTextBox.Text = "Коэффициент продаж";
            // 
            // PlaceOnListingTextBox
            // 
            this.PlaceOnListingTextBox.AutoSize = true;
            this.PlaceOnListingTextBox.BackColor = System.Drawing.Color.Transparent;
            this.PlaceOnListingTextBox.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PlaceOnListingTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.PlaceOnListingTextBox.Location = new System.Drawing.Point(19, 96);
            this.PlaceOnListingTextBox.Name = "PlaceOnListingTextBox";
            this.PlaceOnListingTextBox.Size = new System.Drawing.Size(155, 23);
            this.PlaceOnListingTextBox.TabIndex = 11;
            this.PlaceOnListingTextBox.Text = "Место в листинге";
            // 
            // MinProfitTextBox
            // 
            this.MinProfitTextBox.AutoSize = true;
            this.MinProfitTextBox.BackColor = System.Drawing.Color.Transparent;
            this.MinProfitTextBox.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MinProfitTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.MinProfitTextBox.Location = new System.Drawing.Point(19, 162);
            this.MinProfitTextBox.Name = "MinProfitTextBox";
            this.MinProfitTextBox.Size = new System.Drawing.Size(120, 23);
            this.MinProfitTextBox.TabIndex = 12;
            this.MinProfitTextBox.Text = "Мин. профит";
            // 
            // DesiredProfitTextBox
            // 
            this.DesiredProfitTextBox.AutoSize = true;
            this.DesiredProfitTextBox.BackColor = System.Drawing.Color.Transparent;
            this.DesiredProfitTextBox.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DesiredProfitTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.DesiredProfitTextBox.Location = new System.Drawing.Point(19, 194);
            this.DesiredProfitTextBox.Name = "DesiredProfitTextBox";
            this.DesiredProfitTextBox.Size = new System.Drawing.Size(170, 23);
            this.DesiredProfitTextBox.TabIndex = 13;
            this.DesiredProfitTextBox.Text = "Желаемый профит";
            // 
            // RangeOfPriceTextBox
            // 
            this.RangeOfPriceTextBox.AutoSize = true;
            this.RangeOfPriceTextBox.BackColor = System.Drawing.Color.Transparent;
            this.RangeOfPriceTextBox.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RangeOfPriceTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.RangeOfPriceTextBox.Location = new System.Drawing.Point(19, 260);
            this.RangeOfPriceTextBox.Name = "RangeOfPriceTextBox";
            this.RangeOfPriceTextBox.Size = new System.Drawing.Size(139, 23);
            this.RangeOfPriceTextBox.TabIndex = 14;
            this.RangeOfPriceTextBox.Text = "Диапазон цены";
            // 
            // AvailibleBalanceTextBox
            // 
            this.AvailibleBalanceTextBox.AutoSize = true;
            this.AvailibleBalanceTextBox.BackColor = System.Drawing.Color.Transparent;
            this.AvailibleBalanceTextBox.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AvailibleBalanceTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.AvailibleBalanceTextBox.Location = new System.Drawing.Point(19, 227);
            this.AvailibleBalanceTextBox.Name = "AvailibleBalanceTextBox";
            this.AvailibleBalanceTextBox.Size = new System.Drawing.Size(164, 23);
            this.AvailibleBalanceTextBox.TabIndex = 15;
            this.AvailibleBalanceTextBox.Text = "Доступный баланс";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(19, 360);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 23);
            this.label11.TabIndex = 17;
            this.label11.Text = "Период анализа";
            // 
            // AnalysisInterval
            // 
            this.AnalysisInterval.BackColor = System.Drawing.SystemColors.GrayText;
            this.AnalysisInterval.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AnalysisInterval.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AnalysisInterval.ForeColor = System.Drawing.Color.Gold;
            this.AnalysisInterval.FormattingEnabled = true;
            this.AnalysisInterval.Items.AddRange(new object[] {
            "Неделя",
            "Месяц",
            " "});
            this.AnalysisInterval.Location = new System.Drawing.Point(219, 355);
            this.AnalysisInterval.Name = "AnalysisInterval";
            this.AnalysisInterval.Size = new System.Drawing.Size(89, 28);
            this.AnalysisInterval.TabIndex = 18;
            this.AnalysisInterval.Text = "Неделя";
            this.Annotation.SetToolTip(this.AnalysisInterval, "Приближение графика цены при анализе");
            // 
            // SalesPerWeekTxb
            // 
            this.SalesPerWeekTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.SalesPerWeekTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.SalesPerWeekTxb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SalesPerWeekTxb.ForeColor = System.Drawing.Color.Gold;
            this.SalesPerWeekTxb.Location = new System.Drawing.Point(219, 58);
            this.SalesPerWeekTxb.Name = "SalesPerWeekTxb";
            this.SalesPerWeekTxb.Size = new System.Drawing.Size(89, 27);
            this.SalesPerWeekTxb.TabIndex = 19;
            this.SalesPerWeekTxb.Text = "100";
            this.Annotation.SetToolTip(this.SalesPerWeekTxb, "Количество продаж за день\r\n\r\n");
            // 
            // PlaceOnListingTxb
            // 
            this.PlaceOnListingTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.PlaceOnListingTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.PlaceOnListingTxb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlaceOnListingTxb.ForeColor = System.Drawing.Color.Gold;
            this.PlaceOnListingTxb.Location = new System.Drawing.Point(219, 91);
            this.PlaceOnListingTxb.Name = "PlaceOnListingTxb";
            this.PlaceOnListingTxb.Size = new System.Drawing.Size(89, 27);
            this.PlaceOnListingTxb.TabIndex = 19;
            this.PlaceOnListingTxb.Text = "1";
            this.Annotation.SetToolTip(this.PlaceOnListingTxb, "Место предмета в листинге (для выбора цены продажи )");
            // 
            // CoefficientOfSalesTxb
            // 
            this.CoefficientOfSalesTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.CoefficientOfSalesTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.CoefficientOfSalesTxb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CoefficientOfSalesTxb.ForeColor = System.Drawing.Color.Gold;
            this.CoefficientOfSalesTxb.Location = new System.Drawing.Point(219, 124);
            this.CoefficientOfSalesTxb.Name = "CoefficientOfSalesTxb";
            this.CoefficientOfSalesTxb.Size = new System.Drawing.Size(89, 27);
            this.CoefficientOfSalesTxb.TabIndex = 19;
            this.CoefficientOfSalesTxb.Text = "1";
            this.Annotation.SetToolTip(this.CoefficientOfSalesTxb, "Место в списке цена-продажи.\r\n(чем больше коэффициент, тем ниже будет цена и тем " +
        "дольше будут покупаться предметы)");
            // 
            // RequredProfitTxb
            // 
            this.RequredProfitTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.RequredProfitTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.RequredProfitTxb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RequredProfitTxb.ForeColor = System.Drawing.Color.Gold;
            this.RequredProfitTxb.Location = new System.Drawing.Point(219, 157);
            this.RequredProfitTxb.Name = "RequredProfitTxb";
            this.RequredProfitTxb.Size = new System.Drawing.Size(89, 27);
            this.RequredProfitTxb.TabIndex = 19;
            this.RequredProfitTxb.Text = "1";
            this.Annotation.SetToolTip(this.RequredProfitTxb, "Профит при покупке предмета\r\n");
            // 
            // MinProfitTxb
            // 
            this.MinProfitTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.MinProfitTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.MinProfitTxb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinProfitTxb.ForeColor = System.Drawing.Color.Gold;
            this.MinProfitTxb.Location = new System.Drawing.Point(219, 190);
            this.MinProfitTxb.Name = "MinProfitTxb";
            this.MinProfitTxb.Size = new System.Drawing.Size(89, 27);
            this.MinProfitTxb.TabIndex = 19;
            this.MinProfitTxb.Text = "0";
            this.Annotation.SetToolTip(this.MinProfitTxb, "Добавочная величина к цене продажи предмета\r\n");
            // 
            // AvailableBalanceTxb
            // 
            this.AvailableBalanceTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.AvailableBalanceTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.AvailableBalanceTxb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AvailableBalanceTxb.ForeColor = System.Drawing.Color.Gold;
            this.AvailableBalanceTxb.Location = new System.Drawing.Point(219, 223);
            this.AvailableBalanceTxb.Name = "AvailableBalanceTxb";
            this.AvailableBalanceTxb.Size = new System.Drawing.Size(89, 27);
            this.AvailableBalanceTxb.TabIndex = 19;
            this.AvailableBalanceTxb.Text = "1";
            this.Annotation.SetToolTip(this.AvailableBalanceTxb, "Используемый баланс в процентах");
            // 
            // FitPriceIntervalTxb
            // 
            this.FitPriceIntervalTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.FitPriceIntervalTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.FitPriceIntervalTxb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FitPriceIntervalTxb.ForeColor = System.Drawing.Color.Gold;
            this.FitPriceIntervalTxb.Location = new System.Drawing.Point(219, 256);
            this.FitPriceIntervalTxb.Name = "FitPriceIntervalTxb";
            this.FitPriceIntervalTxb.Size = new System.Drawing.Size(89, 27);
            this.FitPriceIntervalTxb.TabIndex = 19;
            this.FitPriceIntervalTxb.Text = "0,1";
            this.Annotation.SetToolTip(this.FitPriceIntervalTxb, "Диапазон цены, при выходе из которого ордер будет снят");
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.Transparent;
            this.StartButton.FlatAppearance.BorderSize = 0;
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartButton.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StartButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.StartButton.Location = new System.Drawing.Point(1042, 592);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(127, 91);
            this.StartButton.TabIndex = 20;
            this.StartButton.Text = "Начать";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ChangeAccountButton
            // 
            this.ChangeAccountButton.BackColor = System.Drawing.Color.Transparent;
            this.ChangeAccountButton.Enabled = false;
            this.ChangeAccountButton.FlatAppearance.BorderSize = 0;
            this.ChangeAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ChangeAccountButton.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ChangeAccountButton.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ChangeAccountButton.Location = new System.Drawing.Point(19, 647);
            this.ChangeAccountButton.Name = "ChangeAccountButton";
            this.ChangeAccountButton.Size = new System.Drawing.Size(163, 32);
            this.ChangeAccountButton.TabIndex = 22;
            this.ChangeAccountButton.Text = "Выйти из аккаунта";
            this.ChangeAccountButton.UseVisualStyleBackColor = false;
            this.ChangeAccountButton.Visible = false;
            this.ChangeAccountButton.Click += new System.EventHandler(this.ChangeAccountButton_Click);
            // 
            // IsLoggedLabel
            // 
            this.IsLoggedLabel.AutoSize = true;
            this.IsLoggedLabel.BackColor = System.Drawing.Color.Transparent;
            this.IsLoggedLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.IsLoggedLabel.ForeColor = System.Drawing.Color.Red;
            this.IsLoggedLabel.Location = new System.Drawing.Point(14, 618);
            this.IsLoggedLabel.Name = "IsLoggedLabel";
            this.IsLoggedLabel.Size = new System.Drawing.Size(163, 23);
            this.IsLoggedLabel.TabIndex = 23;
            this.IsLoggedLabel.Text = "Вход не выполнен";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label2.Location = new System.Drawing.Point(639, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(239, 38);
            this.label2.TabIndex = 24;
            this.label2.Text = "Консоль событий";
            // 
            // StopButton
            // 
            this.StopButton.BackColor = System.Drawing.Color.Transparent;
            this.StopButton.Enabled = false;
            this.StopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StopButton.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StopButton.ForeColor = System.Drawing.Color.Red;
            this.StopButton.Location = new System.Drawing.Point(1042, 592);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(127, 51);
            this.StopButton.TabIndex = 25;
            this.StopButton.Text = "Остановить\r\n";
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Visible = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // MyTimer
            // 
            this.MyTimer.Enabled = true;
            this.MyTimer.Interval = 1000;
            this.MyTimer.Tick += new System.EventHandler(this.MyTimer_Tick);
            // 
            // UptimeLabel
            // 
            this.UptimeLabel.AutoSize = true;
            this.UptimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.UptimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UptimeLabel.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.UptimeLabel.Location = new System.Drawing.Point(1038, 645);
            this.UptimeLabel.Name = "UptimeLabel";
            this.UptimeLabel.Size = new System.Drawing.Size(61, 20);
            this.UptimeLabel.TabIndex = 27;
            this.UptimeLabel.Text = "Uptime:";
            this.UptimeLabel.Visible = false;
            // 
            // TimeOfUptime
            // 
            this.TimeOfUptime.AutoSize = true;
            this.TimeOfUptime.BackColor = System.Drawing.Color.Transparent;
            this.TimeOfUptime.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.TimeOfUptime.Location = new System.Drawing.Point(1093, 645);
            this.TimeOfUptime.Name = "TimeOfUptime";
            this.TimeOfUptime.Size = new System.Drawing.Size(0, 20);
            this.TimeOfUptime.TabIndex = 28;
            // 
            // SaveConfigurationButton
            // 
            this.SaveConfigurationButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveConfigurationButton.FlatAppearance.BorderSize = 0;
            this.SaveConfigurationButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveConfigurationButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveConfigurationButton.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.SaveConfigurationButton.Location = new System.Drawing.Point(18, 395);
            this.SaveConfigurationButton.Name = "SaveConfigurationButton";
            this.SaveConfigurationButton.Size = new System.Drawing.Size(211, 29);
            this.SaveConfigurationButton.TabIndex = 29;
            this.SaveConfigurationButton.Text = "Применить конфигурацию ";
            this.SaveConfigurationButton.UseVisualStyleBackColor = false;
            this.SaveConfigurationButton.Click += new System.EventHandler(this.SaveConfigurationButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.Transparent;
            this.ResetButton.FlatAppearance.BorderSize = 0;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ResetButton.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ResetButton.Location = new System.Drawing.Point(241, 395);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(67, 29);
            this.ResetButton.TabIndex = 30;
            this.ResetButton.Text = "Сброс";
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // Restart
            // 
            this.Restart.BackColor = System.Drawing.Color.Transparent;
            this.Restart.FlatAppearance.BorderSize = 0;
            this.Restart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Restart.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Restart.Location = new System.Drawing.Point(1012, 28);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(158, 29);
            this.Restart.TabIndex = 31;
            this.Restart.Text = "Перезагрузить";
            this.Restart.UseVisualStyleBackColor = false;
            // 
            // Annotation
            // 
            this.Annotation.AutomaticDelay = 100;
            this.Annotation.AutoPopDelay = 5000;
            this.Annotation.BackColor = System.Drawing.SystemColors.GrayText;
            this.Annotation.ForeColor = System.Drawing.Color.Gold;
            this.Annotation.InitialDelay = 100;
            this.Annotation.ReshowDelay = 20;
            this.Annotation.UseAnimation = false;
            this.Annotation.UseFading = false;
            // 
            // AveragePriceTxb
            // 
            this.AveragePriceTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.AveragePriceTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.AveragePriceTxb.ForeColor = System.Drawing.Color.Gold;
            this.AveragePriceTxb.Location = new System.Drawing.Point(219, 289);
            this.AveragePriceTxb.Name = "AveragePriceTxb";
            this.AveragePriceTxb.Size = new System.Drawing.Size(89, 27);
            this.AveragePriceTxb.TabIndex = 38;
            this.AveragePriceTxb.Text = "1,1";
            this.Annotation.SetToolTip(this.AveragePriceTxb, "Значимость средней цены при принятии решения покупки\r\n");
            // 
            // MinPriceTxb
            // 
            this.MinPriceTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.MinPriceTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.MinPriceTxb.ForeColor = System.Drawing.Color.Gold;
            this.MinPriceTxb.Location = new System.Drawing.Point(538, 58);
            this.MinPriceTxb.Name = "MinPriceTxb";
            this.MinPriceTxb.Size = new System.Drawing.Size(89, 27);
            this.MinPriceTxb.TabIndex = 42;
            this.MinPriceTxb.Text = "0,05";
            this.Annotation.SetToolTip(this.MinPriceTxb, "Минимальная цена в списке предметов (в долларах)");
            // 
            // MaxPriceTxb
            // 
            this.MaxPriceTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.MaxPriceTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaxPriceTxb.ForeColor = System.Drawing.Color.Gold;
            this.MaxPriceTxb.Location = new System.Drawing.Point(538, 91);
            this.MaxPriceTxb.Name = "MaxPriceTxb";
            this.MaxPriceTxb.Size = new System.Drawing.Size(89, 27);
            this.MaxPriceTxb.TabIndex = 42;
            this.MaxPriceTxb.Text = "1";
            this.Annotation.SetToolTip(this.MaxPriceTxb, "Максимальная цена в списке предметов (в долларах)");
            // 
            // ItemListCountTxb
            // 
            this.ItemListCountTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.ItemListCountTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.ItemListCountTxb.ForeColor = System.Drawing.Color.Gold;
            this.ItemListCountTxb.Location = new System.Drawing.Point(538, 124);
            this.ItemListCountTxb.Name = "ItemListCountTxb";
            this.ItemListCountTxb.Size = new System.Drawing.Size(89, 27);
            this.ItemListCountTxb.TabIndex = 42;
            this.ItemListCountTxb.Text = "100";
            this.Annotation.SetToolTip(this.ItemListCountTxb, "Число предметов в списке");
            // 
            // OrderVolumeTxb
            // 
            this.OrderVolumeTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.OrderVolumeTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.OrderVolumeTxb.ForeColor = System.Drawing.Color.Gold;
            this.OrderVolumeTxb.Location = new System.Drawing.Point(538, 157);
            this.OrderVolumeTxb.Name = "OrderVolumeTxb";
            this.OrderVolumeTxb.Size = new System.Drawing.Size(89, 27);
            this.OrderVolumeTxb.TabIndex = 45;
            this.OrderVolumeTxb.Text = "3";
            this.Annotation.SetToolTip(this.OrderVolumeTxb, "Количество каждого закупаемого предмета");
            // 
            // TrendTxb
            // 
            this.TrendTxb.BackColor = System.Drawing.SystemColors.GrayText;
            this.TrendTxb.Cursor = System.Windows.Forms.Cursors.Default;
            this.TrendTxb.ForeColor = System.Drawing.Color.Gold;
            this.TrendTxb.Location = new System.Drawing.Point(219, 322);
            this.TrendTxb.Name = "TrendTxb";
            this.TrendTxb.Size = new System.Drawing.Size(89, 27);
            this.TrendTxb.TabIndex = 49;
            this.TrendTxb.Text = "0,003";
            this.Annotation.SetToolTip(this.TrendTxb, "Минимальный возможный коэффициент прямой тренда (тангенс её угла). ");
            // 
            // BalanceLabel
            // 
            this.BalanceLabel.AutoSize = true;
            this.BalanceLabel.BackColor = System.Drawing.Color.Transparent;
            this.BalanceLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BalanceLabel.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BalanceLabel.Location = new System.Drawing.Point(167, 520);
            this.BalanceLabel.Name = "BalanceLabel";
            this.BalanceLabel.Size = new System.Drawing.Size(0, 23);
            this.BalanceLabel.TabIndex = 35;
            // 
            // Tray
            // 
            this.Tray.Icon = ((System.Drawing.Icon)(resources.GetObject("Tray.Icon")));
            this.Tray.Text = "TradeBot";
            this.Tray.Visible = true;
            this.Tray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Tray_MouseDoubleClick);
            // 
            // ToTrayButton
            // 
            this.ToTrayButton.BackColor = System.Drawing.Color.Transparent;
            this.ToTrayButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ToTrayButton.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ToTrayButton.Location = new System.Drawing.Point(1012, -1);
            this.ToTrayButton.Name = "ToTrayButton";
            this.ToTrayButton.Size = new System.Drawing.Size(158, 29);
            this.ToTrayButton.TabIndex = 36;
            this.ToTrayButton.Text = "Свернуть в трей";
            this.ToTrayButton.UseVisualStyleBackColor = false;
            this.ToTrayButton.Click += new System.EventHandler(this.ToTrayButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(19, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 23);
            this.label4.TabIndex = 37;
            this.label4.Text = "Средняя цена";
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.BackColor = System.Drawing.Color.Transparent;
            this.ClearLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearLogButton.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClearLogButton.Location = new System.Drawing.Point(1012, 57);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(158, 29);
            this.ClearLogButton.TabIndex = 39;
            this.ClearLogButton.Text = "Очистить консоль";
            this.ClearLogButton.UseVisualStyleBackColor = false;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(328, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 23);
            this.label5.TabIndex = 41;
            this.label5.Text = "Минимальная цена";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(328, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 23);
            this.label6.TabIndex = 41;
            this.label6.Text = "Максимальная цена";
            // 
            // LoadItemListButton
            // 
            this.LoadItemListButton.BackColor = System.Drawing.Color.Transparent;
            this.LoadItemListButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoadItemListButton.Location = new System.Drawing.Point(328, 194);
            this.LoadItemListButton.Name = "LoadItemListButton";
            this.LoadItemListButton.Size = new System.Drawing.Size(226, 29);
            this.LoadItemListButton.TabIndex = 43;
            this.LoadItemListButton.Text = "Загрузить список предметов";
            this.LoadItemListButton.UseVisualStyleBackColor = false;
            this.LoadItemListButton.Click += new System.EventHandler(this.LoadItemListButton_Click);
            // 
            // EventConsole
            // 
            this.EventConsole.BackColor = System.Drawing.SystemColors.GrayText;
            this.EventConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EventConsole.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.EventConsole.ForeColor = System.Drawing.Color.Gold;
            this.EventConsole.Location = new System.Drawing.Point(648, 91);
            this.EventConsole.Name = "EventConsole";
            this.EventConsole.ReadOnly = true;
            this.EventConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.EventConsole.Size = new System.Drawing.Size(522, 440);
            this.EventConsole.TabIndex = 44;
            this.EventConsole.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(328, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(203, 23);
            this.label8.TabIndex = 41;
            this.label8.Text = "Количество предметов\r\n";
            // 
            // BalLabel
            // 
            this.BalLabel.AutoSize = true;
            this.BalLabel.BackColor = System.Drawing.Color.Transparent;
            this.BalLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BalLabel.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BalLabel.Location = new System.Drawing.Point(16, 520);
            this.BalLabel.Name = "BalLabel";
            this.BalLabel.Size = new System.Drawing.Size(150, 23);
            this.BalLabel.TabIndex = 6;
            this.BalLabel.Text = "Баланс кошелька:";
            this.BalLabel.Visible = false;
            // 
            // EntireLabel
            // 
            this.EntireLabel.AutoSize = true;
            this.EntireLabel.BackColor = System.Drawing.Color.Transparent;
            this.EntireLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EntireLabel.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.EntireLabel.Location = new System.Drawing.Point(16, 544);
            this.EntireLabel.Name = "EntireLabel";
            this.EntireLabel.Size = new System.Drawing.Size(131, 23);
            this.EntireLabel.TabIndex = 6;
            this.EntireLabel.Text = "Общий баланс:";
            this.EntireLabel.Visible = false;
            // 
            // EntireBalanceLabel
            // 
            this.EntireBalanceLabel.AutoSize = true;
            this.EntireBalanceLabel.BackColor = System.Drawing.Color.Transparent;
            this.EntireBalanceLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EntireBalanceLabel.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.EntireBalanceLabel.Location = new System.Drawing.Point(167, 544);
            this.EntireBalanceLabel.Name = "EntireBalanceLabel";
            this.EntireBalanceLabel.Size = new System.Drawing.Size(0, 23);
            this.EntireBalanceLabel.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(328, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 23);
            this.label7.TabIndex = 46;
            this.label7.Text = "Размер закупки";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // ClearBuyLotsButton
            // 
            this.ClearBuyLotsButton.BackColor = System.Drawing.Color.Transparent;
            this.ClearBuyLotsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearBuyLotsButton.Location = new System.Drawing.Point(328, 229);
            this.ClearBuyLotsButton.Name = "ClearBuyLotsButton";
            this.ClearBuyLotsButton.Size = new System.Drawing.Size(226, 29);
            this.ClearBuyLotsButton.TabIndex = 47;
            this.ClearBuyLotsButton.Text = "Очистить запросы на покупку";
            this.ClearBuyLotsButton.UseVisualStyleBackColor = false;
            this.ClearBuyLotsButton.Click += new System.EventHandler(this.ClearBuyLotsButton_Click);
            // 
            // IsSDADisabledLabel
            // 
            this.IsSDADisabledLabel.AutoSize = true;
            this.IsSDADisabledLabel.BackColor = System.Drawing.Color.Transparent;
            this.IsSDADisabledLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.IsSDADisabledLabel.ForeColor = System.Drawing.Color.Red;
            this.IsSDADisabledLabel.Location = new System.Drawing.Point(15, 595);
            this.IsSDADisabledLabel.Name = "IsSDADisabledLabel";
            this.IsSDADisabledLabel.Size = new System.Drawing.Size(138, 23);
            this.IsSDADisabledLabel.TabIndex = 48;
            this.IsSDADisabledLabel.Text = "SDA выключен";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(19, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 23);
            this.label9.TabIndex = 50;
            this.label9.Text = "Тренд цены";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(17, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 28);
            this.label10.TabIndex = 51;
            this.label10.Text = "Настройки логики";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label12.Location = new System.Drawing.Point(326, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(238, 28);
            this.label12.TabIndex = 52;
            this.label12.Text = "Формирование списка";
            // 
            // ExtendedConsoleButton
            // 
            this.ExtendedConsoleButton.BackColor = System.Drawing.Color.Transparent;
            this.ExtendedConsoleButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExtendedConsoleButton.Location = new System.Drawing.Point(328, 264);
            this.ExtendedConsoleButton.Name = "ExtendedConsoleButton";
            this.ExtendedConsoleButton.Size = new System.Drawing.Size(173, 29);
            this.ExtendedConsoleButton.TabIndex = 53;
            this.ExtendedConsoleButton.Text = "Расширенная консоль";
            this.ExtendedConsoleButton.UseVisualStyleBackColor = false;
            this.ExtendedConsoleButton.Click += new System.EventHandler(this.ExtendedConsoleButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1182, 699);
            this.Controls.Add(this.ExtendedConsoleButton);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TrendTxb);
            this.Controls.Add(this.IsSDADisabledLabel);
            this.Controls.Add(this.ClearBuyLotsButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.OrderVolumeTxb);
            this.Controls.Add(this.EntireBalanceLabel);
            this.Controls.Add(this.EntireLabel);
            this.Controls.Add(this.BalLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ItemListCountTxb);
            this.Controls.Add(this.EventConsole);
            this.Controls.Add(this.LoadItemListButton);
            this.Controls.Add(this.MaxPriceTxb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MinPriceTxb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ClearLogButton);
            this.Controls.Add(this.AveragePriceTxb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ToTrayButton);
            this.Controls.Add(this.BalanceLabel);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.SaveConfigurationButton);
            this.Controls.Add(this.TimeOfUptime);
            this.Controls.Add(this.UptimeLabel);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IsLoggedLabel);
            this.Controls.Add(this.ChangeAccountButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.FitPriceIntervalTxb);
            this.Controls.Add(this.AvailableBalanceTxb);
            this.Controls.Add(this.MinProfitTxb);
            this.Controls.Add(this.RequredProfitTxb);
            this.Controls.Add(this.CoefficientOfSalesTxb);
            this.Controls.Add(this.PlaceOnListingTxb);
            this.Controls.Add(this.SalesPerWeekTxb);
            this.Controls.Add(this.AnalysisInterval);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.AvailibleBalanceTextBox);
            this.Controls.Add(this.RangeOfPriceTextBox);
            this.Controls.Add(this.DesiredProfitTextBox);
            this.Controls.Add(this.MinProfitTextBox);
            this.Controls.Add(this.PlaceOnListingTextBox);
            this.Controls.Add(this.CoefficientOfSalesTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AccountNameLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sighUp);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TradeBot";
            this.Load += new System.EventHandler(this.TradeBotAPI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

