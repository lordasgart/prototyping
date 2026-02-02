namespace Noventis.ActionDispatcher.Web.Components
{
    public interface ICoreComponent
    {
        public int CurrentCount { get; set; }

        void MouseClick();
        void MouseMove(int x, int y);
        void MouseMoveRelative(int deltaX, int deltaY);
        void MouseRightClick();
        void Send(string sendText);
    }
}