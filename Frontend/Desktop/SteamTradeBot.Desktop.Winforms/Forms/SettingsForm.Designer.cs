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
            label8 = new Label();
            label6 = new Label();
            label5 = new Label();
            SaveSettingsButton = new Button();
            groupBox2 = new GroupBox();
            ItemListSizeTextBox = new TextBox();
            MaxPriceTextBox = new TextBox();
            MinPriceTextBox = new TextBox();
            ResetSettingsButton = new Button();
            SteamCommissionTextBox = new TextBox();
            SteamUserIdTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            groupBox5 = new GroupBox();
            ChooseMaFileButton = new Button();
            SaveCredentialsButton = new Button();
            MaFilePathTextBox = new TextBox();
            label14 = new Label();
            PasswordTextBox = new TextBox();
            label15 = new Label();
            LogInTextBox = new TextBox();
            label16 = new Label();
            groupBox4 = new GroupBox();
            ApiKeyTextBox = new TextBox();
            label13 = new Label();
            ConnectionAddressTextBox = new TextBox();
            label12 = new Label();
            groupBox1 = new GroupBox();
            label10 = new Label();
            SalesRatio = new TextBox();
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
            SalesPerDayTextBox = new TextBox();
            SellListingFindRangeTextBox = new TextBox();
            AveragePriceRatioTextBox = new TextBox();
            RequiredProfitTextBox = new TextBox();
            label4 = new Label();
            AvailibleBalanceTextBox = new TextBox();
            FitRangePriceTextBox = new TextBox();
            OpenFileDialog = new OpenFileDialog();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.DodgerBlue;
            label8.Location = new Point(122, 173);
            label8.Name = "label8";
            label8.Size = new Size(64, 21);
            label8.TabIndex = 76;
            label8.Text = "List size";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.DodgerBlue;
            label6.Location = new Point(106, 138);
            label6.Name = "label6";
            label6.Size = new Size(80, 21);
            label6.TabIndex = 75;
            label6.Text = "Max. price";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.DodgerBlue;
            label5.Location = new Point(108, 103);
            label5.Name = "label5";
            label5.Size = new Size(78, 21);
            label5.TabIndex = 74;
            label5.Text = "Min. price";
            // 
            // SaveSettingsButton
            // 
            SaveSettingsButton.Anchor = AnchorStyles.Bottom;
            SaveSettingsButton.BackColor = Color.DodgerBlue;
            SaveSettingsButton.FlatStyle = FlatStyle.Flat;
            SaveSettingsButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            SaveSettingsButton.ForeColor = Color.White;
            SaveSettingsButton.Location = new Point(1083, 554);
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
            groupBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.DodgerBlue;
            groupBox2.Location = new Point(423, 32);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(436, 270);
            groupBox2.TabIndex = 92;
            groupBox2.TabStop = false;
            groupBox2.Text = "Items list settings";
            // 
            // ItemListSizeTextBox
            // 
            ItemListSizeTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ItemListSizeTextBox.BackColor = Color.White;
            ItemListSizeTextBox.BorderStyle = BorderStyle.FixedSingle;
            ItemListSizeTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ItemListSizeTextBox.ForeColor = Color.Black;
            ItemListSizeTextBox.Location = new Point(192, 171);
            ItemListSizeTextBox.Margin = new Padding(2);
            ItemListSizeTextBox.Name = "ItemListSizeTextBox";
            ItemListSizeTextBox.Size = new Size(130, 29);
            ItemListSizeTextBox.TabIndex = 89;
            // 
            // MaxPriceTextBox
            // 
            MaxPriceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MaxPriceTextBox.BackColor = Color.White;
            MaxPriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            MaxPriceTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            MaxPriceTextBox.ForeColor = Color.Black;
            MaxPriceTextBox.Location = new Point(192, 136);
            MaxPriceTextBox.Margin = new Padding(2);
            MaxPriceTextBox.Name = "MaxPriceTextBox";
            MaxPriceTextBox.Size = new Size(130, 29);
            MaxPriceTextBox.TabIndex = 88;
            // 
            // MinPriceTextBox
            // 
            MinPriceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MinPriceTextBox.BackColor = Color.White;
            MinPriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            MinPriceTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            MinPriceTextBox.ForeColor = Color.Black;
            MinPriceTextBox.Location = new Point(192, 101);
            MinPriceTextBox.Margin = new Padding(2);
            MinPriceTextBox.Name = "MinPriceTextBox";
            MinPriceTextBox.Size = new Size(130, 29);
            MinPriceTextBox.TabIndex = 87;
            // 
            // ResetSettingsButton
            // 
            ResetSettingsButton.Anchor = AnchorStyles.Bottom;
            ResetSettingsButton.BackColor = Color.DodgerBlue;
            ResetSettingsButton.FlatStyle = FlatStyle.Flat;
            ResetSettingsButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            ResetSettingsButton.ForeColor = Color.White;
            ResetSettingsButton.Location = new Point(37, 554);
            ResetSettingsButton.Margin = new Padding(3, 2, 3, 2);
            ResetSettingsButton.Name = "ResetSettingsButton";
            ResetSettingsButton.Size = new Size(175, 45);
            ResetSettingsButton.TabIndex = 93;
            ResetSettingsButton.Text = "Reset Settings";
            ResetSettingsButton.UseVisualStyleBackColor = false;
            ResetSettingsButton.Click += ResetSettingsButton_Click;
            // 
            // SteamCommissionTextBox
            // 
            SteamCommissionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SteamCommissionTextBox.BackColor = Color.White;
            SteamCommissionTextBox.BorderStyle = BorderStyle.FixedSingle;
            SteamCommissionTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SteamCommissionTextBox.ForeColor = Color.Black;
            SteamCommissionTextBox.Location = new Point(175, 221);
            SteamCommissionTextBox.Margin = new Padding(4);
            SteamCommissionTextBox.Name = "SteamCommissionTextBox";
            SteamCommissionTextBox.Size = new Size(179, 29);
            SteamCommissionTextBox.TabIndex = 88;
            // 
            // SteamUserIdTextBox
            // 
            SteamUserIdTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SteamUserIdTextBox.BackColor = Color.White;
            SteamUserIdTextBox.BorderStyle = BorderStyle.FixedSingle;
            SteamUserIdTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SteamUserIdTextBox.ForeColor = Color.Black;
            SteamUserIdTextBox.Location = new Point(175, 177);
            SteamUserIdTextBox.Margin = new Padding(4);
            SteamUserIdTextBox.Name = "SteamUserIdTextBox";
            SteamUserIdTextBox.Size = new Size(179, 29);
            SteamUserIdTextBox.TabIndex = 87;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.DodgerBlue;
            label1.Location = new Point(55, 179);
            label1.Name = "label1";
            label1.Size = new Size(106, 21);
            label1.TabIndex = 74;
            label1.Text = "Steam user ID";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.DodgerBlue;
            label2.Location = new Point(20, 223);
            label2.Name = "label2";
            label2.Size = new Size(141, 21);
            label2.TabIndex = 75;
            label2.Text = "Steam commission";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(groupBox5);
            panel1.Controls.Add(groupBox4);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(SaveSettingsButton);
            panel1.Controls.Add(ResetSettingsButton);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(50, 50, 50, 0);
            panel1.Size = new Size(1311, 611);
            panel1.TabIndex = 95;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(SteamCommissionTextBox);
            groupBox5.Controls.Add(SteamUserIdTextBox);
            groupBox5.Controls.Add(label1);
            groupBox5.Controls.Add(label2);
            groupBox5.Controls.Add(ChooseMaFileButton);
            groupBox5.Controls.Add(SaveCredentialsButton);
            groupBox5.Controls.Add(MaFilePathTextBox);
            groupBox5.Controls.Add(label14);
            groupBox5.Controls.Add(PasswordTextBox);
            groupBox5.Controls.Add(label15);
            groupBox5.Controls.Add(LogInTextBox);
            groupBox5.Controls.Add(label16);
            groupBox5.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox5.ForeColor = Color.DodgerBlue;
            groupBox5.Location = new Point(865, 31);
            groupBox5.Margin = new Padding(3, 2, 3, 2);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(3, 2, 3, 2);
            groupBox5.Size = new Size(393, 464);
            groupBox5.TabIndex = 97;
            groupBox5.TabStop = false;
            groupBox5.Text = "Steam settings";
            // 
            // ChooseMaFileButton
            // 
            ChooseMaFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ChooseMaFileButton.BackColor = Color.DodgerBlue;
            ChooseMaFileButton.FlatStyle = FlatStyle.Flat;
            ChooseMaFileButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ChooseMaFileButton.ForeColor = Color.White;
            ChooseMaFileButton.Location = new Point(134, 419);
            ChooseMaFileButton.Margin = new Padding(3, 2, 3, 2);
            ChooseMaFileButton.Name = "ChooseMaFileButton";
            ChooseMaFileButton.Size = new Size(142, 41);
            ChooseMaFileButton.TabIndex = 11;
            ChooseMaFileButton.Text = "Set MaFile Path";
            ChooseMaFileButton.UseVisualStyleBackColor = false;
            ChooseMaFileButton.Click += ChooseMaFileButton_Click;
            // 
            // SaveCredentialsButton
            // 
            SaveCredentialsButton.Anchor = AnchorStyles.Bottom;
            SaveCredentialsButton.BackColor = Color.DodgerBlue;
            SaveCredentialsButton.FlatStyle = FlatStyle.Flat;
            SaveCredentialsButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            SaveCredentialsButton.ForeColor = Color.White;
            SaveCredentialsButton.Location = new Point(280, 717);
            SaveCredentialsButton.Margin = new Padding(3, 2, 3, 2);
            SaveCredentialsButton.Name = "SaveCredentialsButton";
            SaveCredentialsButton.Size = new Size(175, 45);
            SaveCredentialsButton.TabIndex = 10;
            SaveCredentialsButton.Text = "Save";
            SaveCredentialsButton.UseVisualStyleBackColor = false;
            // 
            // MaFilePathTextBox
            // 
            MaFilePathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MaFilePathTextBox.BackColor = SystemColors.ControlLightLight;
            MaFilePathTextBox.BorderStyle = BorderStyle.FixedSingle;
            MaFilePathTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            MaFilePathTextBox.Location = new Point(175, 128);
            MaFilePathTextBox.Margin = new Padding(3, 2, 3, 2);
            MaFilePathTextBox.Name = "MaFilePathTextBox";
            MaFilePathTextBox.Size = new Size(179, 29);
            MaFilePathTextBox.TabIndex = 9;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(36, 130);
            label14.Name = "label14";
            label14.Size = new Size(125, 21);
            label14.TabIndex = 8;
            label14.Text = "SDA maFile path";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PasswordTextBox.BackColor = SystemColors.ControlLightLight;
            PasswordTextBox.BorderStyle = BorderStyle.FixedSingle;
            PasswordTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordTextBox.Location = new Point(175, 83);
            PasswordTextBox.Margin = new Padding(3, 2, 3, 2);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(179, 29);
            PasswordTextBox.TabIndex = 3;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(37, 85);
            label15.Name = "label15";
            label15.Size = new Size(124, 21);
            label15.TabIndex = 2;
            label15.Text = "Steam password";
            // 
            // LogInTextBox
            // 
            LogInTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LogInTextBox.BackColor = SystemColors.ControlLightLight;
            LogInTextBox.BorderStyle = BorderStyle.FixedSingle;
            LogInTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LogInTextBox.Location = new Point(175, 39);
            LogInTextBox.Margin = new Padding(3, 2, 3, 2);
            LogInTextBox.Name = "LogInTextBox";
            LogInTextBox.Size = new Size(179, 29);
            LogInTextBox.TabIndex = 1;
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(69, 41);
            label16.Name = "label16";
            label16.Size = new Size(92, 21);
            label16.TabIndex = 0;
            label16.Text = "Steam login";
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox4.Controls.Add(ApiKeyTextBox);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(ConnectionAddressTextBox);
            groupBox4.Controls.Add(label12);
            groupBox4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox4.ForeColor = Color.DodgerBlue;
            groupBox4.Location = new Point(423, 306);
            groupBox4.Margin = new Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 2, 3, 2);
            groupBox4.Size = new Size(436, 189);
            groupBox4.TabIndex = 96;
            groupBox4.TabStop = false;
            groupBox4.Text = "Connection settings";
            // 
            // ApiKeyTextBox
            // 
            ApiKeyTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ApiKeyTextBox.BackColor = SystemColors.ControlLightLight;
            ApiKeyTextBox.BorderStyle = BorderStyle.FixedSingle;
            ApiKeyTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ApiKeyTextBox.Location = new Point(133, 109);
            ApiKeyTextBox.Margin = new Padding(3, 2, 3, 2);
            ApiKeyTextBox.Name = "ApiKeyTextBox";
            ApiKeyTextBox.Size = new Size(285, 29);
            ApiKeyTextBox.TabIndex = 97;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(65, 111);
            label13.Name = "label13";
            label13.Size = new Size(61, 21);
            label13.TabIndex = 96;
            label13.Text = "API key";
            // 
            // ConnectionAddressTextBox
            // 
            ConnectionAddressTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ConnectionAddressTextBox.BackColor = Color.White;
            ConnectionAddressTextBox.BorderStyle = BorderStyle.FixedSingle;
            ConnectionAddressTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ConnectionAddressTextBox.ForeColor = Color.Black;
            ConnectionAddressTextBox.Location = new Point(133, 74);
            ConnectionAddressTextBox.Margin = new Padding(4);
            ConnectionAddressTextBox.Name = "ConnectionAddressTextBox";
            ConnectionAddressTextBox.Size = new Size(285, 29);
            ConnectionAddressTextBox.TabIndex = 90;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = Color.DodgerBlue;
            label12.Location = new Point(13, 76);
            label12.Name = "label12";
            label12.Size = new Size(113, 21);
            label12.TabIndex = 89;
            label12.Text = "Server address";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(SalesRatio);
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
            groupBox1.Controls.Add(SalesPerDayTextBox);
            groupBox1.Controls.Add(SellListingFindRangeTextBox);
            groupBox1.Controls.Add(AveragePriceRatioTextBox);
            groupBox1.Controls.Add(RequiredProfitTextBox);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(AvailibleBalanceTextBox);
            groupBox1.Controls.Add(FitRangePriceTextBox);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.DodgerBlue;
            groupBox1.Location = new Point(37, 32);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(379, 463);
            groupBox1.TabIndex = 95;
            groupBox1.TabStop = false;
            groupBox1.Text = "Core settings";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = Color.DodgerBlue;
            label10.Location = new Point(123, 331);
            label10.Name = "label10";
            label10.Size = new Size(82, 21);
            label10.TabIndex = 89;
            label10.Text = "Sales ratio";
            // 
            // SalesRatio
            // 
            SalesRatio.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SalesRatio.BackColor = Color.White;
            SalesRatio.BorderStyle = BorderStyle.FixedSingle;
            SalesRatio.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SalesRatio.ForeColor = Color.Black;
            SalesRatio.Location = new Point(210, 329);
            SalesRatio.Margin = new Padding(2);
            SalesRatio.Name = "SalesRatio";
            SalesRatio.Size = new Size(85, 29);
            SalesRatio.TabIndex = 88;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.DodgerBlue;
            label7.Location = new Point(93, 296);
            label7.Name = "label7";
            label7.Size = new Size(112, 21);
            label7.TabIndex = 87;
            label7.Text = "Order quantity";
            // 
            // OrderQuantityTextBox
            // 
            OrderQuantityTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OrderQuantityTextBox.BackColor = Color.White;
            OrderQuantityTextBox.BorderStyle = BorderStyle.FixedSingle;
            OrderQuantityTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            OrderQuantityTextBox.ForeColor = Color.Black;
            OrderQuantityTextBox.Location = new Point(210, 294);
            OrderQuantityTextBox.Margin = new Padding(2);
            OrderQuantityTextBox.Name = "OrderQuantityTextBox";
            OrderQuantityTextBox.Size = new Size(85, 29);
            OrderQuantityTextBox.TabIndex = 86;
            // 
            // label5235262
            // 
            label5235262.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5235262.AutoSize = true;
            label5235262.BackColor = Color.Transparent;
            label5235262.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5235262.ForeColor = Color.DodgerBlue;
            label5235262.Location = new Point(99, 84);
            label5235262.Name = "label5235262";
            label5235262.Size = new Size(106, 21);
            label5235262.TabIndex = 58;
            label5235262.Text = "Require profit";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.DodgerBlue;
            label3.Location = new Point(103, 49);
            label3.Name = "label3";
            label3.Size = new Size(102, 21);
            label3.TabIndex = 54;
            label3.Text = "Sales per day";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.DodgerBlue;
            label9.Location = new Point(156, 224);
            label9.Name = "label9";
            label9.Size = new Size(49, 21);
            label9.TabIndex = 85;
            label9.Text = "Trend";
            // 
            // label1525
            // 
            label1525.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1525.AutoSize = true;
            label1525.BackColor = Color.Transparent;
            label1525.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1525.ForeColor = Color.DodgerBlue;
            label1525.Location = new Point(49, 366);
            label1525.Name = "label1525";
            label1525.Size = new Size(156, 21);
            label1525.TabIndex = 56;
            label1525.Text = "Sell listing find range";
            // 
            // TrendTextBox
            // 
            TrendTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TrendTextBox.BackColor = Color.White;
            TrendTextBox.BorderStyle = BorderStyle.FixedSingle;
            TrendTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TrendTextBox.ForeColor = Color.Black;
            TrendTextBox.Location = new Point(210, 222);
            TrendTextBox.Margin = new Padding(2);
            TrendTextBox.Name = "TrendTextBox";
            TrendTextBox.Size = new Size(85, 29);
            TrendTextBox.TabIndex = 84;
            // 
            // label163463
            // 
            label163463.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label163463.AutoSize = true;
            label163463.BackColor = Color.Transparent;
            label163463.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label163463.ForeColor = Color.DodgerBlue;
            label163463.Location = new Point(96, 154);
            label163463.Name = "label163463";
            label163463.Size = new Size(109, 21);
            label163463.TabIndex = 59;
            label163463.Text = "Fit price range";
            // 
            // label1515236
            // 
            label1515236.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1515236.AutoSize = true;
            label1515236.BackColor = Color.Transparent;
            label1515236.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1515236.ForeColor = Color.DodgerBlue;
            label1515236.Location = new Point(39, 119);
            label1515236.Name = "label1515236";
            label1515236.Size = new Size(166, 21);
            label1515236.TabIndex = 60;
            label1515236.Text = "Available balance ratio";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.DodgerBlue;
            label11.Location = new Point(89, 260);
            label11.Name = "label11";
            label11.Size = new Size(116, 21);
            label11.TabIndex = 61;
            label11.Text = "Analisys period";
            // 
            // AnalysisIntervalComboBox
            // 
            AnalysisIntervalComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AnalysisIntervalComboBox.BackColor = Color.White;
            AnalysisIntervalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            AnalysisIntervalComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            AnalysisIntervalComboBox.ForeColor = Color.Black;
            AnalysisIntervalComboBox.FormattingEnabled = true;
            AnalysisIntervalComboBox.Items.AddRange(new object[] { "7", "30" });
            AnalysisIntervalComboBox.Location = new Point(210, 257);
            AnalysisIntervalComboBox.Margin = new Padding(2);
            AnalysisIntervalComboBox.Name = "AnalysisIntervalComboBox";
            AnalysisIntervalComboBox.Size = new Size(85, 29);
            AnalysisIntervalComboBox.TabIndex = 62;
            // 
            // SalesPerDayTextBox
            // 
            SalesPerDayTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SalesPerDayTextBox.BackColor = Color.White;
            SalesPerDayTextBox.BorderStyle = BorderStyle.FixedSingle;
            SalesPerDayTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SalesPerDayTextBox.ForeColor = Color.Black;
            SalesPerDayTextBox.Location = new Point(210, 47);
            SalesPerDayTextBox.Margin = new Padding(2);
            SalesPerDayTextBox.Name = "SalesPerDayTextBox";
            SalesPerDayTextBox.Size = new Size(85, 29);
            SalesPerDayTextBox.TabIndex = 69;
            // 
            // SellListingFindRangeTextBox
            // 
            SellListingFindRangeTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SellListingFindRangeTextBox.BackColor = Color.White;
            SellListingFindRangeTextBox.BorderStyle = BorderStyle.FixedSingle;
            SellListingFindRangeTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SellListingFindRangeTextBox.ForeColor = Color.Black;
            SellListingFindRangeTextBox.Location = new Point(210, 364);
            SellListingFindRangeTextBox.Margin = new Padding(2);
            SellListingFindRangeTextBox.Name = "SellListingFindRangeTextBox";
            SellListingFindRangeTextBox.Size = new Size(85, 29);
            SellListingFindRangeTextBox.TabIndex = 63;
            // 
            // AveragePriceRatioTextBox
            // 
            AveragePriceRatioTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AveragePriceRatioTextBox.BackColor = Color.White;
            AveragePriceRatioTextBox.BorderStyle = BorderStyle.FixedSingle;
            AveragePriceRatioTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            AveragePriceRatioTextBox.ForeColor = Color.Black;
            AveragePriceRatioTextBox.Location = new Point(210, 187);
            AveragePriceRatioTextBox.Margin = new Padding(2);
            AveragePriceRatioTextBox.Name = "AveragePriceRatioTextBox";
            AveragePriceRatioTextBox.Size = new Size(85, 29);
            AveragePriceRatioTextBox.TabIndex = 73;
            // 
            // RequiredProfitTextBox
            // 
            RequiredProfitTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RequiredProfitTextBox.BackColor = Color.White;
            RequiredProfitTextBox.BorderStyle = BorderStyle.FixedSingle;
            RequiredProfitTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            RequiredProfitTextBox.ForeColor = Color.Black;
            RequiredProfitTextBox.Location = new Point(210, 82);
            RequiredProfitTextBox.Margin = new Padding(2);
            RequiredProfitTextBox.Name = "RequiredProfitTextBox";
            RequiredProfitTextBox.Size = new Size(85, 29);
            RequiredProfitTextBox.TabIndex = 66;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.DodgerBlue;
            label4.Location = new Point(64, 189);
            label4.Name = "label4";
            label4.Size = new Size(141, 21);
            label4.TabIndex = 72;
            label4.Text = "Average price ratio";
            // 
            // AvailibleBalanceTextBox
            // 
            AvailibleBalanceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AvailibleBalanceTextBox.BackColor = Color.White;
            AvailibleBalanceTextBox.BorderStyle = BorderStyle.FixedSingle;
            AvailibleBalanceTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            AvailibleBalanceTextBox.ForeColor = Color.Black;
            AvailibleBalanceTextBox.Location = new Point(210, 117);
            AvailibleBalanceTextBox.Margin = new Padding(2);
            AvailibleBalanceTextBox.Name = "AvailibleBalanceTextBox";
            AvailibleBalanceTextBox.Size = new Size(85, 29);
            AvailibleBalanceTextBox.TabIndex = 67;
            // 
            // FitRangePriceTextBox
            // 
            FitRangePriceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FitRangePriceTextBox.BackColor = Color.White;
            FitRangePriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            FitRangePriceTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FitRangePriceTextBox.ForeColor = Color.Black;
            FitRangePriceTextBox.Location = new Point(210, 152);
            FitRangePriceTextBox.Margin = new Padding(2);
            FitRangePriceTextBox.Name = "FitRangePriceTextBox";
            FitRangePriceTextBox.Size = new Size(85, 29);
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
            panel1.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button ExtendedConsoleButton;
        private Button ClearBuyLotsButton;
        private TextBox OrderVolumeTxb;
        private Label label8;
        private TextBox ItemListCountTxb;
        private Button LoadItemListButton;
        private TextBox MaxPriceTxb;
        private Label label6;
        private TextBox MinPriceTxb;
        private Label label5;
        private Button SaveConfigurationButton;
        public TextBox MinProfitTextBox;
        private Label label46346;
        private Button SaveSettingsButton;
        private GroupBox groupBox2;
        private Button ResetSettingsButton;
        private TextBox ItemListSizeTextBox;
        private TextBox MaxPriceTextBox;
        private TextBox MinPriceTextBox;
        private TextBox SteamCommissionTextBox;
        private TextBox SteamUserIdTextBox;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private GroupBox groupBox1;
        private Label label10;
        private TextBox SalesRatio;
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
        public TextBox SalesPerDayTextBox;
        public TextBox SellListingFindRangeTextBox;
        private TextBox AveragePriceRatioTextBox;
        public TextBox RequiredProfitTextBox;
        private Label label4;
        public TextBox AvailibleBalanceTextBox;
        public TextBox FitRangePriceTextBox;
        private GroupBox groupBox4;
        private TextBox ConnectionAddressTextBox;
        private Label label12;
        private GroupBox groupBox5;
        private Button ChooseMaFileButton;
        public Button SaveCredentialsButton;
        private TextBox MaFilePathTextBox;
        private Label label14;
        private TextBox PasswordTextBox;
        private Label label15;
        private TextBox LogInTextBox;
        private Label label16;
        private TextBox ApiKeyTextBox;
        private Label label13;
        private OpenFileDialog OpenFileDialog;
    }
}