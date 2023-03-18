namespace TradeBotClient.Forms
{
    partial class LoginForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Observe = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.PathTextBox = new System.Windows.Forms.Label();
            this.SaveLoginAndPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.SteamGuardTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ClearSavedButton = new System.Windows.Forms.Button();
            this.HideBrowserCheckBox = new System.Windows.Forms.CheckBox();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Путь к maFile";
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.LoginTextBox.Location = new System.Drawing.Point(142, 53);
            this.LoginTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(178, 27);
            this.LoginTextBox.TabIndex = 3;
            this.LoginTextBox.TabStop = false;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PasswordTextBox.Location = new System.Drawing.Point(142, 86);
            this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(178, 27);
            this.PasswordTextBox.TabIndex = 4;
            this.PasswordTextBox.TabStop = false;
            this.PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.Transparent;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoginButton.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LoginButton.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.LoginButton.Location = new System.Drawing.Point(83, 335);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LoginButton.Size = new System.Drawing.Size(183, 46);
            this.LoginButton.TabIndex = 6;
            this.LoginButton.Text = "Войти";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(83, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 31);
            this.label4.TabIndex = 8;
            this.label4.Text = "Авторизация";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Observe
            // 
            this.Observe.BackColor = System.Drawing.Color.Transparent;
            this.Observe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Observe.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Observe.Location = new System.Drawing.Point(141, 171);
            this.Observe.Name = "Observe";
            this.Observe.Size = new System.Drawing.Size(94, 30);
            this.Observe.TabIndex = 9;
            this.Observe.Text = "Обзор";
            this.Observe.UseVisualStyleBackColor = false;
            this.Observe.Click += new System.EventHandler(this.Observe_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "OpenFileDialog";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label5.Location = new System.Drawing.Point(17, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Путь:\r\n";
            // 
            // PathTextBox
            // 
            this.PathTextBox.BackColor = System.Drawing.Color.Transparent;
            this.PathTextBox.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PathTextBox.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.PathTextBox.Location = new System.Drawing.Point(67, 208);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(271, 65);
            this.PathTextBox.TabIndex = 2;
            // 
            // SaveLoginAndPasswordCheckBox
            // 
            this.SaveLoginAndPasswordCheckBox.AutoSize = true;
            this.SaveLoginAndPasswordCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.SaveLoginAndPasswordCheckBox.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.SaveLoginAndPasswordCheckBox.Location = new System.Drawing.Point(17, 305);
            this.SaveLoginAndPasswordCheckBox.Name = "SaveLoginAndPasswordCheckBox";
            this.SaveLoginAndPasswordCheckBox.Size = new System.Drawing.Size(162, 24);
            this.SaveLoginAndPasswordCheckBox.TabIndex = 12;
            this.SaveLoginAndPasswordCheckBox.Text = "Сохранить данные";
            this.SaveLoginAndPasswordCheckBox.UseVisualStyleBackColor = false;
            this.SaveLoginAndPasswordCheckBox.CheckedChanged += new System.EventHandler(this.SaveLoginAndPasswordCheckBox_CheckedChanged);
            // 
            // SteamGuardTextBox
            // 
            this.SteamGuardTextBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.SteamGuardTextBox.Location = new System.Drawing.Point(142, 119);
            this.SteamGuardTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.SteamGuardTextBox.Name = "SteamGuardTextBox";
            this.SteamGuardTextBox.Size = new System.Drawing.Size(93, 27);
            this.SteamGuardTextBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 23);
            this.label6.TabIndex = 15;
            this.label6.Text = "Steam Guard";
            // 
            // ClearSavedButton
            // 
            this.ClearSavedButton.BackColor = System.Drawing.Color.Transparent;
            this.ClearSavedButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearSavedButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClearSavedButton.Location = new System.Drawing.Point(280, -1);
            this.ClearSavedButton.Name = "ClearSavedButton";
            this.ClearSavedButton.Size = new System.Drawing.Size(67, 27);
            this.ClearSavedButton.TabIndex = 17;
            this.ClearSavedButton.Text = "Сброс";
            this.ClearSavedButton.UseVisualStyleBackColor = false;
            this.ClearSavedButton.Click += new System.EventHandler(this.ClearSavedButton_Click);
            // 
            // HideBrowserCheckBox
            // 
            this.HideBrowserCheckBox.AutoSize = true;
            this.HideBrowserCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.HideBrowserCheckBox.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.HideBrowserCheckBox.Location = new System.Drawing.Point(17, 276);
            this.HideBrowserCheckBox.Name = "HideBrowserCheckBox";
            this.HideBrowserCheckBox.Size = new System.Drawing.Size(142, 24);
            this.HideBrowserCheckBox.TabIndex = 40;
            this.HideBrowserCheckBox.Text = "Скрыть браузер";
            this.HideBrowserCheckBox.UseVisualStyleBackColor = false;
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(346, 402);
            this.Controls.Add(this.HideBrowserCheckBox);
            this.Controls.Add(this.ClearSavedButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SteamGuardTextBox);
            this.Controls.Add(this.SaveLoginAndPasswordCheckBox);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Observe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Observe;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label PathTextBox;
        private System.Windows.Forms.CheckBox SaveLoginAndPasswordCheckBox;
        private System.Windows.Forms.TextBox SteamGuardTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ClearSavedButton;
        private System.Windows.Forms.CheckBox HideBrowserCheckBox;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
    }
}
