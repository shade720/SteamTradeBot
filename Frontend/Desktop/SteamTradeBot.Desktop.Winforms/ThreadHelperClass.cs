namespace SteamTradeBot.Desktop.Winforms;

public static class ThreadHelperClass
{
    public static void ExecOnForm(Form form, Action action, bool parentForm = true)
    {
        if (parentForm)
            form.BeginInvoke(new MethodInvoker(action));
        else
            action();
    }
}