namespace Noventis.ActionDispatcher.Web.Components.Pages;

public partial class Mouse
{
    private readonly ICoreComponent core;

    public Mouse(ICoreComponent core)
    {
        this.core = core;
    }

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
