namespace Noventis.ActionDispatcher.Web.Components.Pages
{
    public partial class Keyboard(ICoreComponent core)
    {
        private void Tab()
        {
            core.Send("{TAB}");
        }

        private void Space()
        {
            core.Send("{SPACE}");
        }

        private void Enter()
        {
            core.Send("{ENTER}");
        }
    }
}