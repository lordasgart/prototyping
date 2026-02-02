namespace Noventis.ActionDispatcher.Web.Components
{
    public class CoreComponent : ICoreComponent
    {
        public int CurrentCount { get; set; }

        //https://www.autohotkey.com/docs/v1/lib/Send.htm
        private readonly Queue<string> commandQueue = new();

        private readonly string ahkFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ActionDispatcher.ahk");

        public void Send(string textToSend)
        {
            GenericAction($"Send, {textToSend}");
        }

        public void MouseMove(int x, int y)
        {
            GenericAction($"MouseMove, {x}, {y}");
        }

        //MouseMoveRelative
        public void MouseMoveRelative(int deltaX, int deltaY)
        {
            GenericAction($"MouseMove, {deltaX}, {deltaY}, 0, R");
        }

        private void GenericAction(string genericActionText)
        {
            CurrentCount++;
            var ahkCommand = $"{genericActionText} ; {Guid.NewGuid()}";
            commandQueue.Enqueue(ahkCommand);
            PublishEventsAction();
        }

        /// <summary>
        /// Write all commands from the queue to the AHK file
        /// </summary>
        private void PublishEventsAction()
        {
            using var writer = new StreamWriter(ahkFilePath, append: true);

            while (commandQueue.Count > 0)
            {
                var command = commandQueue.Dequeue();
                writer.WriteLine(command);
            }
        }

        public void MouseClick()
        {
            GenericAction($"MouseClick");
        }

        //right click
        public void MouseRightClick()
        {
            GenericAction($"MouseClick, right");
        }
    }
}
