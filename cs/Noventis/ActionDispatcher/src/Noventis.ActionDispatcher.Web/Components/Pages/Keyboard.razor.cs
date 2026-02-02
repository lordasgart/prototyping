namespace Noventis.ActionDispatcher.Web.Components.Pages
{
    public partial class Keyboard(ICoreComponent core)
    {
        private void Tab()
        {
            SendKey($"TAB");
        }

        private void Send(string key)
        {
            core.Send($"{key}");
        }

        private void SendKey(string key)
        {
            core.Send($"{{{key}}}");
        }
    }
}