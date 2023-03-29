
namespace Interface.Forms
{
    partial class ExtendedConsoleForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.ExtendedEventConsole = new System.Windows.Forms.RichTextBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.SiftedItemsTextBox = new System.Windows.Forms.RichTextBox();
            this.HideButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPage1);
            this.tabControl1.Controls.Add(this.TabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 35);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(780, 753);
            this.tabControl1.TabIndex = 52;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Transparent;
            this.TabPage1.Controls.Add(this.ExtendedEventConsole);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TabPage1.Size = new System.Drawing.Size(772, 720);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Расширенная консоль";
            // 
            // ExtendedEventConsole
            // 
            this.ExtendedEventConsole.BackColor = System.Drawing.SystemColors.GrayText;
            this.ExtendedEventConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ExtendedEventConsole.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ExtendedEventConsole.ForeColor = System.Drawing.Color.Gold;
            this.ExtendedEventConsole.Location = new System.Drawing.Point(3, 3);
            this.ExtendedEventConsole.Name = "ExtendedEventConsole";
            this.ExtendedEventConsole.ReadOnly = true;
            this.ExtendedEventConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ExtendedEventConsole.Size = new System.Drawing.Size(769, 714);
            this.ExtendedEventConsole.TabIndex = 46;
            this.ExtendedEventConsole.Text = "";
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.SiftedItemsTextBox);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(772, 720);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Отсеяные предметы";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // SiftedItemsTextBox
            // 
            this.SiftedItemsTextBox.BackColor = System.Drawing.SystemColors.GrayText;
            this.SiftedItemsTextBox.ForeColor = System.Drawing.Color.Gold;
            this.SiftedItemsTextBox.Location = new System.Drawing.Point(3, 3);
            this.SiftedItemsTextBox.Name = "SiftedItemsTextBox";
            this.SiftedItemsTextBox.Size = new System.Drawing.Size(766, 715);
            this.SiftedItemsTextBox.TabIndex = 0;
            this.SiftedItemsTextBox.Text = "";
            // 
            // HideButton
            // 
            this.HideButton.Location = new System.Drawing.Point(650, 12);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(131, 29);
            this.HideButton.TabIndex = 53;
            this.HideButton.Text = "Скрыть это окно";
            this.HideButton.UseVisualStyleBackColor = true;
            this.HideButton.Click += new System.EventHandler(this.HideButton_Click);
            // 
            // ExtendedConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(this.HideButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ExtendedConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExtendedConsole";
            this.tabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPage1;
        public System.Windows.Forms.RichTextBox ExtendedEventConsole;
        private System.Windows.Forms.TabPage TabPage2;
        public System.Windows.Forms.RichTextBox SiftedItemsTextBox;
        private System.Windows.Forms.Button HideButton;
    }
}