namespace SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;

public static class ThreadHelperClass
{
    public static void ExecOnForm(Form form, Action action, bool parentForm = true)
    {
        try
        {
            if (parentForm)
                form.BeginInvoke(new MethodInvoker(action));
            else
                action();
        }
        catch { }
    }
}