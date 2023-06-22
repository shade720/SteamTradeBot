namespace SteamTradeBot.Desktop.Winforms;

public static class ThreadHelperClass
{
    private delegate void SetTextCallback(Form f, Control ctrl, string text);
    private delegate void SetGridCallback(Form f, DataGridView ctrl, params object[] row);

    public static void SetText(Form form, Control ctrl, string text)
    {
        if (ctrl.InvokeRequired)
        {
            var d = new SetTextCallback(SetText);
            form.Invoke(d, form, ctrl, text);
        }
        else
        {
            ctrl.Text = text;
        }
    }

    public static void AddRow(Form form, DataGridView ctrl, params object[] row)
    {
        if (ctrl.InvokeRequired)
        {
            var d = new SetGridCallback(AddRow);
            form.Invoke(d, form, ctrl, row);
        }
        else
        {
            ctrl.Rows.Add(row);
        }
    }
}