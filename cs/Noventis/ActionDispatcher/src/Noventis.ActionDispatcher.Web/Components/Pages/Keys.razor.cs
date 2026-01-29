namespace Noventis.ActionDispatcher.Web.Components.Pages
{
    public partial class Keys(ICoreComponent core)
    {
        private string sendText = string.Empty;

        private void SendAction()
        {
            core.Send(sendText);
        }
    }
}
