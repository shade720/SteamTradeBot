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
            ChooseMaFileButton = new Button();
            LogInButton = new Button();
            textBox2 = new TextBox();
            label5 = new Label();
            label4 = new Label();
            SaveCredentialsCheckBox = new CheckBox();
            TokenTextBox = new TextBox();
            label3 = new Label();
            PasswordTextBox = new TextBox();
            label2 = new Label();
            LogInTextBox = new TextBox();
            label1 = new Label();
            FolderBrowserDialog = new FolderBrowserDialog();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ChooseMaFileButton);
            groupBox1.Controls.Add(LogInButton);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(SaveCredentialsCheckBox);
            groupBox1.Controls.Add(TokenTextBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(PasswordTextBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(LogInTextBox);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.Red;
            groupBox1.Location = new Point(407, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(616, 601);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Log In";
            // 
            // ChooseMaFileButton
            // 
            ChooseMaFileButton.BackColor = Color.Red;
            ChooseMaFileButton.FlatStyle = FlatStyle.Flat;
            ChooseMaFileButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ChooseMaFileButton.ForeColor = Color.White;
            ChooseMaFileButton.Location = new Point(447, 313);
            ChooseMaFileButton.Name = "ChooseMaFileButton";
            ChooseMaFileButton.Size = new Size(111, 31);
            ChooseMaFileButton.TabIndex = 11;
            ChooseMaFileButton.Text = "Path";
            ChooseMaFileButton.UseVisualStyleBackColor = false;
            // 
            // LogInButton
            // 
            LogInButton.BackColor = Color.Red;
            LogInButton.FlatStyle = FlatStyle.Flat;
            LogInButton.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            LogInButton.ForeColor = Color.White;
            LogInButton.Location = new Point(210, 470);
            LogInButton.Name = "LogInButton";
            LogInButton.Size = new Size(200, 60);
            LogInButton.TabIndex = 10;
            LogInButton.Text = "Log In";
            LogInButton.UseVisualStyleBackColor = false;
            LogInButton.Click += LogInButton_Click;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(238, 313);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "____________________";
            textBox2.Size = new Size(203, 31);
            textBox2.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(43, 313);
            label5.Name = "label5";
            label5.Size = new Size(187, 31);
            label5.TabIndex = 8;
            label5.Text = "SDA maFile path:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(289, 260);
            label4.Name = "label4";
            label4.Size = new Size(39, 31);
            label4.TabIndex = 7;
            label4.Text = "Or";
            // 
            // SaveCredentialsCheckBox
            // 
            SaveCredentialsCheckBox.AutoSize = true;
            SaveCredentialsCheckBox.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            SaveCredentialsCheckBox.Location = new Point(415, 560);
            SaveCredentialsCheckBox.Name = "SaveCredentialsCheckBox";
            SaveCredentialsCheckBox.Size = new Size(201, 35);
            SaveCredentialsCheckBox.TabIndex = 6;
            SaveCredentialsCheckBox.Text = "Save credentials";
            SaveCredentialsCheckBox.UseVisualStyleBackColor = true;
            // 
            // TokenTextBox
            // 
            TokenTextBox.BorderStyle = BorderStyle.None;
            TokenTextBox.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            TokenTextBox.Location = new Point(238, 200);
            TokenTextBox.Name = "TokenTextBox";
            TokenTextBox.PlaceholderText = "________________________________";
            TokenTextBox.Size = new Size(320, 31);
            TokenTextBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(138, 200);
            label3.Name = "label3";
            label3.Size = new Size(73, 31);
            label3.TabIndex = 4;
            label3.Text = "Token";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.BorderStyle = BorderStyle.None;
            PasswordTextBox.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordTextBox.Location = new Point(238, 141);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PlaceholderText = "________________________________";
            PasswordTextBox.Size = new Size(320, 31);
            PasswordTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(104, 141);
            label2.Name = "label2";
            label2.Size = new Size(110, 31);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // LogInTextBox
            // 
            LogInTextBox.BorderStyle = BorderStyle.None;
            LogInTextBox.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            LogInTextBox.Location = new Point(238, 83);
            LogInTextBox.Name = "LogInTextBox";
            LogInTextBox.PlaceholderText = "________________________________";
            LogInTextBox.Size = new Size(320, 31);
            LogInTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(143, 83);
            label1.Name = "label1";
            label1.Size = new Size(71, 31);
            label1.TabIndex = 0;
            label1.Text = "Login";
            // 
            // LogInForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1507, 733);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
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
        private TextBox textBox2;
        private Label label5;
        private Label label4;
        private CheckBox SaveCredentialsCheckBox;
        private TextBox TokenTextBox;
        private Label label3;
        private Button LogInButton;
        private Button ChooseMaFileButton;
        private FolderBrowserDialog FolderBrowserDialog;
    }
}