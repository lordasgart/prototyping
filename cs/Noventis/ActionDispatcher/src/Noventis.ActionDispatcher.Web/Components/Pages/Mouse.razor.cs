namespace Noventis.ActionDispatcher.Web.Components.Pages;

public partial class Mouse(ICoreComponent core)
{
    private int x;
    private int y;

    private void MouseMoveAction()
    {
        core.MouseMove(x,y);
    }

    private void MouseClickAction()
    {
        core.MouseClick();
    }
}
