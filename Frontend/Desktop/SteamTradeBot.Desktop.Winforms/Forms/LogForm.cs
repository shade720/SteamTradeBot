﻿using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.Models;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class LogForm : Form
{
    private readonly IEnumerable<LogInfo> _logs;
    public LogForm(string logFile)
    {
        InitializeComponent();
        try
        {
            _logs = LogFileParser(logFile);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static IEnumerable<LogInfo> LogFileParser(string logFile)
    {
        if (string.IsNullOrEmpty(logFile)) yield break;
        var logRecords = logFile.Split("`~");
        foreach (var log in logRecords)
        {
            if (string.IsNullOrEmpty(log) || log.Length < 10)
                continue;
            var date = log[..(log.IndexOf('+') - 2)];
            var level = log[log.IndexOf('[')..(log.IndexOf(']') + 1)];
            var logMessage = log[(log.IndexOf(']') + 1)..].Replace(@"\n", "");
            yield return new LogInfo
            {
                Date = DateTime.Parse(date),
                Level = level switch
                {
                    "[Fatal]" => BusinessLogicLayer.Models.LogLevel.Fatal,
                    "[Error]" => BusinessLogicLayer.Models.LogLevel.Error,
                    "[Warning]" => BusinessLogicLayer.Models.LogLevel.Warning,
                    "[Information]" => BusinessLogicLayer.Models.LogLevel.Information,
                    _ => BusinessLogicLayer.Models.LogLevel.Information
                },
                Message = logMessage
            };
        }
    }

    private void FillLogDataGrid(IEnumerable<LogInfo> logs)
    {
        LogDataGrid.Rows.Clear();
        foreach (var log in logs)
        {
            LogDataGrid.Rows.Add(log.Date, log.Level, log.Message);
        }
        for (var i = 0; i < LogDataGrid.Rows.Count; i++)
        {
            if (LogDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString().Contains("Error") ||
                LogDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString().Contains("Fatal"))
                LogDataGrid.Rows[i].Cells[1].Style.BackColor = Color.Red;
        }
    }

    private void LogDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1) return;
        MessageTextbox.Text = LogDataGrid.Rows[e.RowIndex].Cells[2].EditedFormattedValue.ToString();
    }

    private void LogLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillLogDataGrid(_logs.Where(x => x.Level.ToString() == LogLevel.Text || LogLevel.Text == "All"));
    }


    private void SearchButton_Click(object sender, EventArgs e)
    {
        FillLogDataGrid(_logs.Where(x => x.Message.Contains(SearchTextbox.Text)));
    }

    private void LogViewerWindow_Load(object sender, EventArgs e)
    {
        LogLevel.SelectedIndex = 0;
    }
}