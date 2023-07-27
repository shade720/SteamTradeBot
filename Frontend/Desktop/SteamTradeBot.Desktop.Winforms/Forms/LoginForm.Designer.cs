namespace SteamTradeBot.Desktop.Winforms.Forms
{
    partial class LogInForm
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
            groupBox1 = new GroupBox();
            ResetButton = new Button();
            ChooseMaFileButton = new Button();
            LogInButton = new Button();
            MaFilePathTextBox = new TextBox();
            label5 = new Label();
            label4 = new Label();
            RememberMeCheckBox = new CheckBox();
            TokenTextBox = new TextBox();
            label3 = new Label();
            PasswordTextBox = new TextBox();
            label2 = new Label();
            LogInTextBox = new TextBox();
            label1 = new Label();
            OpenFileDialog = new OpenFileDialog();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ResetButton);
            groupBox1.Controls.Add(ChooseMaFileButton);
            groupBox1.Controls.Add(LogInButton);
            groupBox1.Controls.Add(MaFilePathTextBox);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(RememberMeCheckBox);
            groupBox1.Controls.Add(TokenTextBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(PasswordTextBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(LogInTextBox);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.DodgerBlue;
            groupBox1.Location = new Point(356, 45);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(539, 451);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Log In";
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.DodgerBlue;
            ResetButton.FlatStyle = FlatStyle.Flat;
            ResetButton.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ResetButton.ForeColor = Color.White;
            ResetButton.Location = new Point(442, 13);
            ResetButton.Margin = new Padding(3, 2, 3, 2);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(97, 29);
            ResetButton.TabIndex = 12;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // ChooseMaFileButton
            // 
            ChooseMaFileButton.BackColor = Color.DodgerBlue;
            ChooseMaFileButton.FlatStyle = FlatStyle.Flat;
            ChooseMaFileButton.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ChooseMaFileButton.ForeColor = Color.White;
            ChooseMaFileButton.Location = new Point(441, 231);
            ChooseMaFileButton.Margin = new Padding(3, 2, 3, 2);
            ChooseMaFileButton.Name = "ChooseMaFileButton";
            ChooseMaFileButton.Size = new Size(97, 29);
            ChooseMaFileButton.TabIndex = 11;
            ChooseMaFileButton.Text = "Path";
            ChooseMaFileButton.UseVisualStyleBackColor = false;
            ChooseMaFileButton.Click += ChooseMaFileButton_Click;
            // 
            // LogInButton
            // 
            LogInButton.BackColor = Color.DodgerBlue;
            LogInButton.FlatStyle = FlatStyle.Flat;
            LogInButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            LogInButton.ForeColor = Color.White;
            LogInButton.Location = new Point(184, 352);
            LogInButton.Margin = new Padding(3, 2, 3, 2);
            LogInButton.Name = "LogInButton";
            LogInButton.Size = new Size(175, 45);
            LogInButton.TabIndex = 10;
            LogInButton.Text = "Log In";
            LogInButton.UseVisualStyleBackColor = false;
            LogInButton.Click += LogInButton_Click;
            // 
            // MaFilePathTextBox
            // 
            MaFilePathTextBox.BackColor = SystemColors.ControlLight;
            MaFilePathTextBox.BorderStyle = BorderStyle.FixedSingle;
            MaFilePathTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            MaFilePathTextBox.Location = new Point(184, 231);
            MaFilePathTextBox.Margin = new Padding(3, 2, 3, 2);
            MaFilePathTextBox.Name = "MaFilePathTextBox";
            MaFilePathTextBox.Size = new Size(233, 29);
            MaFilePathTextBox.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(38, 235);
            label5.Name = "label5";
            label5.Size = new Size(128, 21);
            label5.TabIndex = 8;
            label5.Text = "SDA maFile path:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(257, 195);
            label4.Name = "label4";
            label4.Size = new Size(26, 20);
            label4.TabIndex = 7;
            label4.Text = "Or";
            // 
            // RememberMeCheckBox
            // 
            RememberMeCheckBox.AutoSize = true;
            RememberMeCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            RememberMeCheckBox.Location = new Point(202, 402);
            RememberMeCheckBox.Margin = new Padding(3, 2, 3, 2);
            RememberMeCheckBox.Name = "RememberMeCheckBox";
            RememberMeCheckBox.Size = new Size(132, 25);
            RememberMeCheckBox.TabIndex = 6;
            RememberMeCheckBox.Text = "Remember me";
            RememberMeCheckBox.UseVisualStyleBackColor = true;
            // 
            // TokenTextBox
            // 
            TokenTextBox.BackColor = SystemColors.ControlLight;
            TokenTextBox.BorderStyle = BorderStyle.FixedSingle;
            TokenTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TokenTextBox.Location = new Point(184, 148);
            TokenTextBox.Margin = new Padding(3, 2, 3, 2);
            TokenTextBox.Name = "TokenTextBox";
            TokenTextBox.Size = new Size(233, 29);
            TokenTextBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(123, 150);
            label3.Name = "label3";
            label3.Size = new Size(50, 21);
            label3.TabIndex = 4;
            label3.Text = "Token";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.BackColor = SystemColors.ControlLight;
            PasswordTextBox.BorderStyle = BorderStyle.FixedSingle;
            PasswordTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordTextBox.Location = new Point(184, 104);
            PasswordTextBox.Margin = new Padding(3, 2, 3, 2);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(233, 29);
            PasswordTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(97, 106);
            label2.Name = "label2";
            label2.Size = new Size(76, 21);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // LogInTextBox
            // 
            LogInTextBox.BackColor = SystemColors.ControlLight;
            LogInTextBox.BorderStyle = BorderStyle.FixedSingle;
            LogInTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LogInTextBox.Location = new Point(184, 60);
            LogInTextBox.Margin = new Padding(3, 2, 3, 2);
            LogInTextBox.Name = "LogInTextBox";
            LogInTextBox.Size = new Size(233, 29);
            LogInTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(125, 62);
            label1.Name = "label1";
            label1.Size = new Size(49, 21);
            label1.TabIndex = 0;
            label1.Text = "Login";
            // 
            // OpenFileDialog
            // 
            OpenFileDialog.FileName = "openFileDialog1";
            // 
            // LogInForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1319, 550);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "LogInForm";
            Text = "LogInForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox LogInTextBox;
        private Label label1;
        private TextBox PasswordTextBox;
        private Label label2;
        private TextBox MaFilePathTextBox;
        private Label label5;
        private Label label4;
        private CheckBox RememberMeCheckBox;
        private TextBox TokenTextBox;
        private Label label3;
        private Button ChooseMaFileButton;
        private Button ResetButton;
        private OpenFileDialog OpenFileDialog;
        public Button LogInButton;
    }
}