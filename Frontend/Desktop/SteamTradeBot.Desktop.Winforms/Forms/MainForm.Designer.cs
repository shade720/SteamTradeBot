namespace SteamTradeBot.Desktop.Winforms.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panel1 = new Panel();
            panel4 = new Panel();
            StatsNavButton = new Button();
            SettingsNavButton = new Button();
            WorkerNavButton = new Button();
            panel6 = new Panel();
            ServiceStatePanel = new Panel();
            SteamLogoPictureBox = new PictureBox();
            panel3 = new Panel();
            label13 = new Label();
            panel8 = new Panel();
            LoadingPictureBox = new PictureBox();
            Frame = new Panel();
            CurrentWorkLabel = new Label();
            panel2 = new Panel();
            panel5 = new Panel();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            ServiceStatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SteamLogoPictureBox).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LoadingPictureBox).BeginInit();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.BackColor = Color.DodgerBlue;
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(ServiceStatePanel);
            panel1.Controls.Add(panel3);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(219, 711);
            panel1.TabIndex = 54;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel4.BackColor = Color.DodgerBlue;
            panel4.Controls.Add(StatsNavButton);
            panel4.Controls.Add(SettingsNavButton);
            panel4.Controls.Add(WorkerNavButton);
            panel4.Controls.Add(panel6);
            panel4.Location = new Point(0, 97);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(219, 170);
            panel4.TabIndex = 1;
            // 
            // StatsNavButton
            // 
            StatsNavButton.Dock = DockStyle.Top;
            StatsNavButton.FlatAppearance.BorderSize = 0;
            StatsNavButton.FlatStyle = FlatStyle.Flat;
            StatsNavButton.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            StatsNavButton.ForeColor = Color.White;
            StatsNavButton.Location = new Point(0, 178);
            StatsNavButton.Margin = new Padding(3, 2, 3, 2);
            StatsNavButton.Name = "StatsNavButton";
            StatsNavButton.Size = new Size(219, 52);
            StatsNavButton.TabIndex = 11;
            StatsNavButton.Text = "Stats";
            StatsNavButton.UseVisualStyleBackColor = true;
            StatsNavButton.Click += StatsNavButton_Click;
            // 
            // SettingsNavButton
            // 
            SettingsNavButton.Dock = DockStyle.Top;
            SettingsNavButton.FlatAppearance.BorderSize = 0;
            SettingsNavButton.FlatStyle = FlatStyle.Flat;
            SettingsNavButton.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            SettingsNavButton.ForeColor = Color.White;
            SettingsNavButton.Location = new Point(0, 126);
            SettingsNavButton.Margin = new Padding(3, 2, 3, 2);
            SettingsNavButton.Name = "SettingsNavButton";
            SettingsNavButton.Size = new Size(219, 52);
            SettingsNavButton.TabIndex = 10;
            SettingsNavButton.Text = "Settings";
            SettingsNavButton.UseVisualStyleBackColor = true;
            SettingsNavButton.Click += SettingsNavButton_Click;
            // 
            // WorkerNavButton
            // 
            WorkerNavButton.Dock = DockStyle.Top;
            WorkerNavButton.FlatAppearance.BorderSize = 0;
            WorkerNavButton.FlatStyle = FlatStyle.Flat;
            WorkerNavButton.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            WorkerNavButton.ForeColor = Color.White;
            WorkerNavButton.Location = new Point(0, 74);
            WorkerNavButton.Margin = new Padding(3, 2, 3, 2);
            WorkerNavButton.Name = "WorkerNavButton";
            WorkerNavButton.Size = new Size(219, 52);
            WorkerNavButton.TabIndex = 8;
            WorkerNavButton.Text = "Worker";
            WorkerNavButton.UseVisualStyleBackColor = true;
            WorkerNavButton.Click += WorkerNavButton_Click;
            // 
            // panel6
            // 
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Margin = new Padding(3, 2, 3, 2);
            panel6.Name = "panel6";
            panel6.Size = new Size(219, 74);
            panel6.TabIndex = 9;
            // 
            // ServiceStatePanel
            // 
            ServiceStatePanel.BackColor = Color.Orange;
            ServiceStatePanel.Controls.Add(SteamLogoPictureBox);
            ServiceStatePanel.Dock = DockStyle.Bottom;
            ServiceStatePanel.Location = new Point(0, 672);
            ServiceStatePanel.Margin = new Padding(3, 2, 3, 2);
            ServiceStatePanel.Name = "ServiceStatePanel";
            ServiceStatePanel.Size = new Size(219, 39);
            ServiceStatePanel.TabIndex = 2;
            // 
            // SteamLogoPictureBox
            // 
            SteamLogoPictureBox.Image = Properties.Resources.SteamLogo;
            SteamLogoPictureBox.Location = new Point(94, 8);
            SteamLogoPictureBox.Margin = new Padding(3, 2, 3, 2);
            SteamLogoPictureBox.Name = "SteamLogoPictureBox";
            SteamLogoPictureBox.Size = new Size(26, 22);
            SteamLogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            SteamLogoPictureBox.TabIndex = 0;
            SteamLogoPictureBox.TabStop = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(label13);
            panel3.Controls.Add(panel8);
            panel3.Location = new Point(0, 1);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(219, 94);
            panel3.TabIndex = 0;
            // 
            // label13
            // 
            label13.BackColor = Color.DodgerBlue;
            label13.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label13.ForeColor = Color.White;
            label13.Location = new Point(0, 0);
            label13.Name = "label13";
            label13.Size = new Size(219, 94);
            label13.TabIndex = 0;
            label13.Text = "SteamTradeBot";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            panel8.Location = new Point(220, 37);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(1334, 706);
            panel8.TabIndex = 57;
            // 
            // LoadingPictureBox
            // 
            LoadingPictureBox.BackgroundImageLayout = ImageLayout.Zoom;
            LoadingPictureBox.Image = (Image)resources.GetObject("LoadingPictureBox.Image");
            LoadingPictureBox.Location = new Point(1290, 1);
            LoadingPictureBox.Margin = new Padding(3, 2, 3, 2);
            LoadingPictureBox.Name = "LoadingPictureBox";
            LoadingPictureBox.Size = new Size(44, 38);
            LoadingPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            LoadingPictureBox.TabIndex = 0;
            LoadingPictureBox.TabStop = false;
            LoadingPictureBox.Visible = false;
            // 
            // Frame
            // 
            Frame.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Frame.BackColor = Color.White;
            Frame.Location = new Point(219, 37);
            Frame.Margin = new Padding(3, 2, 3, 2);
            Frame.Name = "Frame";
            Frame.Size = new Size(1335, 635);
            Frame.TabIndex = 57;
            // 
            // CurrentWorkLabel
            // 
            CurrentWorkLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentWorkLabel.ForeColor = Color.Black;
            CurrentWorkLabel.Location = new Point(809, 1);
            CurrentWorkLabel.Name = "CurrentWorkLabel";
            CurrentWorkLabel.RightToLeft = RightToLeft.No;
            CurrentWorkLabel.Size = new Size(476, 38);
            CurrentWorkLabel.TabIndex = 58;
            CurrentWorkLabel.TextAlign = ContentAlignment.MiddleRight;
            CurrentWorkLabel.Visible = false;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.RoyalBlue;
            panel2.ForeColor = Color.LightSkyBlue;
            panel2.Location = new Point(219, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1335, 38);
            panel2.TabIndex = 55;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Azure;
            panel5.Controls.Add(CurrentWorkLabel);
            panel5.Controls.Add(LoadingPictureBox);
            panel5.Location = new Point(218, 671);
            panel5.Name = "panel5";
            panel5.Size = new Size(1336, 40);
            panel5.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1554, 711);
            Controls.Add(panel5);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(Frame);
            DoubleBuffered = true;
            ForeColor = SystemColors.GradientActiveCaption;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(1570, 750);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TradeBot";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ServiceStatePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SteamLogoPictureBox).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LoadingPictureBox).EndInit();
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void EventConsole_TextChanged1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private Panel panel1;
        private Panel panel3;
        private Label label13;
        private Panel ServiceStatePanel;
        private Panel panel8;
        private Panel Frame;
        private PictureBox LoadingPictureBox;
        private PictureBox SteamLogoPictureBox;
        private Label CurrentWorkLabel;
        private Panel panel4;
        private Button SettingsNavButton;
        private Button StatsNavButton;
        private Button WorkerNavButton;
        private Panel panel6;
        private Panel panel2;
        private Panel panel5;
    }
}

