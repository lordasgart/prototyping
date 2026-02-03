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
            var sb = new System.Text.StringBuilder();

            if (isCtrlPressed)
            {
                sb.Append('^');
                sb.Append(key);
                core.Send(sb.ToString());
                return;
            }

            foreach (char c in key)
            {
                sb.Append("{U+");
                sb.Append(((int)c).ToString("X4")); // Unicode-Codepunkt in 4-stelligem Hex
                sb.Append('}');
            }

            core.Send(sb.ToString());
        }


        private void SendKey(string key)
        {
            core.Send($"{{{key}}}");
        }

        bool isCtrlPressed = false;

        private void Ctrl()
        {
            isCtrlPressed = !isCtrlPressed;
        }
    }
}