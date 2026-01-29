namespace Noventis.ActionDispatcher.Web.Components
{
    public interface ICoreComponent
    {
        public int CurrentCount { get; set; }

        void MouseClick();
        void MouseMove(int x, int y);
        void Send(string sendText);
    }
}