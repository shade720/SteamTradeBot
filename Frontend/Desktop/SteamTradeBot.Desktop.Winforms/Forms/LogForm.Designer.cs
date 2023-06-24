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
            Date = new DataGridViewTextBoxColumn();
            Level = new DataGridViewTextBoxColumn();
            Message = new DataGridViewTextBoxColumn();
            SearchTextbox = new TextBox();
            LogLevel = new ComboBox();
            MessageTextbox = new RichTextBox();
            SearchButton = new Button();
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
            LogDataGrid.MultiSelect = false;
            LogDataGrid.Name = "LogDataGrid";
            LogDataGrid.ReadOnly = true;
            LogDataGrid.RowHeadersVisible = false;
            LogDataGrid.RowHeadersWidth = 51;
            LogDataGrid.RowTemplate.Height = 29;
            LogDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LogDataGrid.Size = new Size(1209, 445);
            LogDataGrid.TabIndex = 0;
            LogDataGrid.CellClick += LogDataGrid_CellClick;
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
            Message.Width = 880;
            // 
            // SearchTextbox
            // 
            SearchTextbox.Location = new Point(152, 2);
            SearchTextbox.Name = "SearchTextbox";
            SearchTextbox.PlaceholderText = "Search for...";
            SearchTextbox.Size = new Size(958, 27);
            SearchTextbox.TabIndex = 1;
            // 
            // LogLevel
            // 
            LogLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            LogLevel.FormattingEnabled = true;
            LogLevel.Items.AddRange(new object[] { "All", "Error", "Warning", "Information" });
            LogLevel.Location = new Point(1, 1);
            LogLevel.Name = "LogLevel";
            LogLevel.Size = new Size(151, 28);
            LogLevel.TabIndex = 2;
            LogLevel.SelectedIndexChanged += LogLevel_SelectedIndexChanged;
            // 
            // MessageTextbox
            // 
            MessageTextbox.Location = new Point(1, 480);
            MessageTextbox.Name = "MessageTextbox";
            MessageTextbox.RightMargin = 600;
            MessageTextbox.Size = new Size(1209, 320);
            MessageTextbox.TabIndex = 3;
            MessageTextbox.Text = "";
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(1116, 1);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(94, 29);
            SearchButton.TabIndex = 4;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // LogsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1211, 802);
            Controls.Add(SearchTextbox);
            Controls.Add(SearchButton);
            Controls.Add(MessageTextbox);
            Controls.Add(LogLevel);
            Controls.Add(LogDataGrid);
            Name = "LogsForm";
            Text = "LogViewerWindow";
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