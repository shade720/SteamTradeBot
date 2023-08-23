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
            SaveCredentialsButton = new Button();
            MaFilePathTextBox = new TextBox();
            label5 = new Label();
            PasswordTextBox = new TextBox();
            label2 = new Label();
            LogInTextBox = new TextBox();
            label1 = new Label();
            OpenFileDialog = new OpenFileDialog();
            ApiKeyTextBox = new TextBox();
            label3 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(ApiKeyTextBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(ResetButton);
            groupBox1.Controls.Add(ChooseMaFileButton);
            groupBox1.Controls.Add(SaveCredentialsButton);
            groupBox1.Controls.Add(MaFilePathTextBox);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(PasswordTextBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(LogInTextBox);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.DodgerBlue;
            groupBox1.Location = new Point(369, 86);
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
            ResetButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
            ChooseMaFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ChooseMaFileButton.BackColor = Color.DodgerBlue;
            ChooseMaFileButton.FlatStyle = FlatStyle.Flat;
            ChooseMaFileButton.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ChooseMaFileButton.ForeColor = Color.White;
            ChooseMaFileButton.Location = new Point(441, 223);
            ChooseMaFileButton.Margin = new Padding(3, 2, 3, 2);
            ChooseMaFileButton.Name = "ChooseMaFileButton";
            ChooseMaFileButton.Size = new Size(97, 29);
            ChooseMaFileButton.TabIndex = 11;
            ChooseMaFileButton.Text = "Path";
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
            SaveCredentialsButton.Location = new Point(184, 352);
            SaveCredentialsButton.Margin = new Padding(3, 2, 3, 2);
            SaveCredentialsButton.Name = "SaveCredentialsButton";
            SaveCredentialsButton.Size = new Size(175, 45);
            SaveCredentialsButton.TabIndex = 10;
            SaveCredentialsButton.Text = "Save";
            SaveCredentialsButton.UseVisualStyleBackColor = false;
            SaveCredentialsButton.Click += LogInButton_Click;
            // 
            // MaFilePathTextBox
            // 
            MaFilePathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MaFilePathTextBox.BackColor = SystemColors.ControlLight;
            MaFilePathTextBox.BorderStyle = BorderStyle.FixedSingle;
            MaFilePathTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            MaFilePathTextBox.Location = new Point(184, 223);
            MaFilePathTextBox.Margin = new Padding(3, 2, 3, 2);
            MaFilePathTextBox.Name = "MaFilePathTextBox";
            MaFilePathTextBox.Size = new Size(233, 29);
            MaFilePathTextBox.TabIndex = 9;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(47, 225);
            label5.Name = "label5";
            label5.Size = new Size(129, 21);
            label5.TabIndex = 8;
            label5.Text = "SDA maFile path";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PasswordTextBox.BackColor = SystemColors.ControlLight;
            PasswordTextBox.BorderStyle = BorderStyle.FixedSingle;
            PasswordTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordTextBox.Location = new Point(184, 178);
            PasswordTextBox.Margin = new Padding(3, 2, 3, 2);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(233, 29);
            PasswordTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(97, 180);
            label2.Name = "label2";
            label2.Size = new Size(79, 21);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // LogInTextBox
            // 
            LogInTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LogInTextBox.BackColor = SystemColors.ControlLight;
            LogInTextBox.BorderStyle = BorderStyle.FixedSingle;
            LogInTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LogInTextBox.Location = new Point(184, 134);
            LogInTextBox.Margin = new Padding(3, 2, 3, 2);
            LogInTextBox.Name = "LogInTextBox";
            LogInTextBox.Size = new Size(233, 29);
            LogInTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(125, 136);
            label1.Name = "label1";
            label1.Size = new Size(51, 21);
            label1.TabIndex = 0;
            label1.Text = "Login";
            // 
            // OpenFileDialog
            // 
            OpenFileDialog.FileName = "openFileDialog1";
            // 
            // ApiKeyTextBox
            // 
            ApiKeyTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ApiKeyTextBox.BackColor = SystemColors.ControlLight;
            ApiKeyTextBox.BorderStyle = BorderStyle.FixedSingle;
            ApiKeyTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ApiKeyTextBox.Location = new Point(184, 268);
            ApiKeyTextBox.Margin = new Padding(3, 2, 3, 2);
            ApiKeyTextBox.Name = "ApiKeyTextBox";
            ApiKeyTextBox.Size = new Size(233, 29);
            ApiKeyTextBox.TabIndex = 14;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(112, 270);
            label3.Name = "label3";
            label3.Size = new Size(64, 21);
            label3.TabIndex = 13;
            label3.Text = "API key";
            // 
            // LogInForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1335, 635);
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
        private Button ChooseMaFileButton;
        private Button ResetButton;
        private OpenFileDialog OpenFileDialog;
        public Button SaveCredentialsButton;
        private TextBox ApiKeyTextBox;
        private Label label3;
    }
}