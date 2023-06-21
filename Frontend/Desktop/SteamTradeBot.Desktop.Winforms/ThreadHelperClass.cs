namespace SteamTradeBot.Desktop.Winforms;

public static class ThreadHelperClass
{
    private delegate void SetTextCallback(Form f, Control ctrl, string text);
    /// <summary>
    /// Set text property of various controls
    /// </summary>
    /// <param name="form">The calling form</param>
    /// <param name="ctrl"></param>
    /// <param name="text"></param>
    public static void SetText(Form form, Control ctrl, string text)
    {
        // InvokeRequired required compares the thread ID of the 
        // calling thread to the thread ID of the creating thread. 
        // If these threads are different, it returns true. 
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
}