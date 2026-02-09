namespace Noventis.ActionDispatcher.Web.Components.Pages;

public partial class Text(ICoreComponent core)
{
    private string text;

    private void SendText()
    {
        foreach (var c in text)
        {
            if (c == '\n')
            {
                core.SendKey("ENTER");
                continue;
            }

            core.Send($"{c}");
        }
    }
}
