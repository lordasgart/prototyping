namespace Noventis.ActionDispatcher.Web.Components.Pages
{
    public partial class Keys
    {
        private int currentCount = 0;

        private readonly Queue<string> commandQueue = new();

        //https://www.autohotkey.com/docs/v1/lib/Send.htm
        private readonly string ahkFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ActionDispatcher.ahk");

        private string sendText = string.Empty;

        private void SendAction()
        {
            Send(sendText);
        }

        private void Send(string textToSend)
        {
            GenericAction($"Send, {textToSend}");
        }

        private void GenericAction(string genericActionText)
        {
            currentCount++;
            var ahkCommand = $"{genericActionText} ; {Guid.NewGuid()}";
            commandQueue.Enqueue(ahkCommand);
            PublishEventsAction();
            sendText = string.Empty;
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
    }
}
