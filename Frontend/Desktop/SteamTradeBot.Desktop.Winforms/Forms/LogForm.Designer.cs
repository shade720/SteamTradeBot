namespace SteamTradeBot.Desktop.Winforms.Forms
{
    partial class LogForm
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
            LogDataGrid = new DataGridView();
            SearchTextbox = new TextBox();
            LogLevel = new ComboBox();
            MessageTextbox = new RichTextBox();
            SearchButton = new Button();
            Date = new DataGridViewTextBoxColumn();
            Level = new DataGridViewTextBoxColumn();
            Message = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)LogDataGrid).BeginInit();
            SuspendLayout();
            // 
            // LogDataGrid
            // 
            LogDataGrid.AllowUserToAddRows = false;
            LogDataGrid.AllowUserToDeleteRows = false;
            LogDataGrid.BackgroundColor = SystemColors.ControlLightLight;
            LogDataGrid.BorderStyle = BorderStyle.Fixed3D;
            LogDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            LogDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogDataGrid.Columns.AddRange(new DataGridViewColumn[] { Date, Level, Message });
            LogDataGrid.GridColor = SystemColors.ControlLightLight;
            LogDataGrid.Location = new Point(1, 29);
            LogDataGrid.Margin = new Padding(3, 2, 3, 2);
            LogDataGrid.MultiSelect = false;
            LogDataGrid.Name = "LogDataGrid";
            LogDataGrid.ReadOnly = true;
            LogDataGrid.RowHeadersVisible = false;
            LogDataGrid.RowHeadersWidth = 51;
            LogDataGrid.RowTemplate.Height = 29;
            LogDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LogDataGrid.Size = new Size(1460, 534);
            LogDataGrid.TabIndex = 0;
            LogDataGrid.CellClick += LogDataGrid_CellClick;
            // 
            // SearchTextbox
            // 
            SearchTextbox.Location = new Point(266, 1);
            SearchTextbox.Margin = new Padding(0);
            SearchTextbox.Multiline = true;
            SearchTextbox.Name = "SearchTextbox";
            SearchTextbox.PlaceholderText = "Search for...";
            SearchTextbox.Size = new Size(1117, 30);
            SearchTextbox.TabIndex = 1;
            // 
            // LogLevel
            // 
            LogLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            LogLevel.FlatStyle = FlatStyle.Flat;
            LogLevel.FormattingEnabled = true;
            LogLevel.Items.AddRange(new object[] { "All", "Error", "Warning", "Information" });
            LogLevel.Location = new Point(151, 4);
            LogLevel.Margin = new Padding(0);
            LogLevel.Name = "LogLevel";
            LogLevel.Size = new Size(115, 23);
            LogLevel.TabIndex = 2;
            LogLevel.SelectedIndexChanged += LogLevel_SelectedIndexChanged;
            // 
            // MessageTextbox
            // 
            MessageTextbox.Location = new Point(1, 567);
            MessageTextbox.Margin = new Padding(3, 2, 3, 2);
            MessageTextbox.Name = "MessageTextbox";
            MessageTextbox.RightMargin = 600;
            MessageTextbox.Size = new Size(1456, 238);
            MessageTextbox.TabIndex = 3;
            MessageTextbox.Text = "";
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(1381, 0);
            SearchButton.Margin = new Padding(0);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(80, 32);
            SearchButton.TabIndex = 4;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // Date
            // 
            Date.HeaderText = "Date";
            Date.MinimumWidth = 6;
            Date.Name = "Date";
            Date.ReadOnly = true;
            Date.Width = 150;
            // 
            // Level
            // 
            Level.HeaderText = "Level";
            Level.MinimumWidth = 6;
            Level.Name = "Level";
            Level.ReadOnly = true;
            Level.Width = 115;
            // 
            // Message
            // 
            Message.HeaderText = "Message";
            Message.MinimumWidth = 6;
            Message.Name = "Message";
            Message.ReadOnly = true;
            Message.Width = 1180;
            // 
            // LogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1460, 807);
            Controls.Add(SearchTextbox);
            Controls.Add(SearchButton);
            Controls.Add(MessageTextbox);
            Controls.Add(LogLevel);
            Controls.Add(LogDataGrid);
            Margin = new Padding(3, 2, 3, 2);
            Name = "LogForm";
            ShowIcon = false;
            Text = "Log";
            Load += LogViewerWindow_Load;
            ((System.ComponentModel.ISupportInitialize)LogDataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView LogDataGrid;
        private TextBox SearchTextbox;
        private ComboBox LogLevel;
        private RichTextBox MessageTextbox;
        private Button SearchButton;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Level;
        private DataGridViewTextBoxColumn Message;
    }
}