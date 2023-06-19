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
            panel5 = new Panel();
            panel4 = new Panel();
            StatsNavButton = new Button();
            SettingsNavButton = new Button();
            panel6 = new Panel();
            WorkerNavButton = new Button();
            panel3 = new Panel();
            panel8 = new Panel();
            label13 = new Label();
            panel2 = new Panel();
            LogInButton = new Button();
            panel7 = new Panel();
            Frame = new Panel();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 880);
            panel1.TabIndex = 54;
            // 
            // panel5
            // 
            panel5.BackColor = Color.PaleGreen;
            panel5.Location = new Point(0, 829);
            panel5.Name = "panel5";
            panel5.Size = new Size(250, 51);
            panel5.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Controls.Add(StatsNavButton);
            panel4.Controls.Add(SettingsNavButton);
            panel4.Controls.Add(panel6);
            panel4.Controls.Add(WorkerNavButton);
            panel4.Location = new Point(0, 129);
            panel4.Name = "panel4";
            panel4.Size = new Size(250, 751);
            panel4.TabIndex = 1;
            // 
            // StatsNavButton
            // 
            StatsNavButton.FlatAppearance.BorderSize = 0;
            StatsNavButton.FlatStyle = FlatStyle.Flat;
            StatsNavButton.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            StatsNavButton.ForeColor = Color.White;
            StatsNavButton.Location = new Point(0, 297);
            StatsNavButton.Name = "StatsNavButton";
            StatsNavButton.Size = new Size(250, 69);
            StatsNavButton.TabIndex = 3;
            StatsNavButton.Text = "Stats";
            StatsNavButton.UseVisualStyleBackColor = true;
            // 
            // SettingsNavButton
            // 
            SettingsNavButton.FlatAppearance.BorderSize = 0;
            SettingsNavButton.FlatStyle = FlatStyle.Flat;
            SettingsNavButton.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            SettingsNavButton.ForeColor = Color.White;
            SettingsNavButton.Location = new Point(0, 222);
            SettingsNavButton.Name = "SettingsNavButton";
            SettingsNavButton.Size = new Size(250, 69);
            SettingsNavButton.TabIndex = 2;
            SettingsNavButton.Text = "Settings";
            SettingsNavButton.UseVisualStyleBackColor = true;
            SettingsNavButton.Click += SettingsNavButton_Click;
            // 
            // panel6
            // 
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(250, 148);
            panel6.TabIndex = 1;
            // 
            // WorkerNavButton
            // 
            WorkerNavButton.FlatAppearance.BorderSize = 0;
            WorkerNavButton.FlatStyle = FlatStyle.Flat;
            WorkerNavButton.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            WorkerNavButton.ForeColor = Color.White;
            WorkerNavButton.Location = new Point(0, 147);
            WorkerNavButton.Name = "WorkerNavButton";
            WorkerNavButton.Size = new Size(250, 69);
            WorkerNavButton.TabIndex = 0;
            WorkerNavButton.Text = "Worker";
            WorkerNavButton.UseVisualStyleBackColor = true;
            WorkerNavButton.Click += WorkerNavButton_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel8);
            panel3.Controls.Add(label13);
            panel3.Location = new Point(0, 1);
            panel3.Name = "panel3";
            panel3.Size = new Size(250, 125);
            panel3.TabIndex = 0;
            // 
            // panel8
            // 
            panel8.Location = new Point(252, 49);
            panel8.Name = "panel8";
            panel8.Size = new Size(1524, 941);
            panel8.TabIndex = 57;
            // 
            // label13
            // 
            label13.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label13.ForeColor = SystemColors.ButtonFace;
            label13.Location = new Point(0, 0);
            label13.Name = "label13";
            label13.Size = new Size(250, 125);
            label13.TabIndex = 0;
            label13.Text = "SteamTradeBot";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkRed;
            panel2.Controls.Add(LogInButton);
            panel2.ForeColor = Color.DarkRed;
            panel2.Location = new Point(250, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1526, 50);
            panel2.TabIndex = 55;
            // 
            // LogInButton
            // 
            LogInButton.FlatAppearance.BorderSize = 0;
            LogInButton.FlatStyle = FlatStyle.Flat;
            LogInButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            LogInButton.ForeColor = Color.White;
            LogInButton.Location = new Point(1417, 0);
            LogInButton.Name = "LogInButton";
            LogInButton.Size = new Size(108, 50);
            LogInButton.TabIndex = 0;
            LogInButton.Text = "Log In";
            LogInButton.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            panel7.BackColor = Color.DarkRed;
            panel7.ForeColor = Color.DarkRed;
            panel7.Location = new Point(250, 829);
            panel7.Name = "panel7";
            panel7.Size = new Size(1526, 51);
            panel7.TabIndex = 56;
            // 
            // Frame
            // 
            Frame.Location = new Point(249, 49);
            Frame.Name = "Frame";
            Frame.Size = new Size(1525, 780);
            Frame.TabIndex = 57;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1776, 881);
            Controls.Add(Frame);
            Controls.Add(panel7);
            Controls.Add(panel2);
            Controls.Add(panel1);
            DoubleBuffered = true;
            ForeColor = SystemColors.GradientActiveCaption;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TradeBot";
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void EventConsole_TextChanged1(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private Panel panel1;
        private Panel panel2;
        private Panel panel4;
        private Panel panel3;
        private Label label13;
        private Panel panel5;
        private Button StatsNavButton;
        private Button SettingsNavButton;
        private Panel panel6;
        private Button WorkerNavButton;
        private Panel panel7;
        private Panel panel8;
        private Button LogInButton;
        private Panel Frame;
    }
}

