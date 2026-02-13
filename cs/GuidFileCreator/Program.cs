namespace GuidFileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a new GUID
            string guid = Guid.NewGuid().ToString();

            // Use the first argument as the base directory path
            string baseDirectory = args.Length > 0 ? args[0] : "C:\\Users\\z004c1aw\\OneDrive - Siemens Healthineers\\Guid";
            // Create a directory with the GUID
            string guidDirectory = Path.Combine(baseDirectory, guid);
            Directory.CreateDirectory(guidDirectory);
            // Define the file path inside the GUID directory
            string filePath = Path.Combine(guidDirectory, $"{guid}.md");

            // Create the file
            File.Create(filePath).Dispose();

            // Open the GUID folder in Visual Studio Code
            Directory.SetCurrentDirectory(guidDirectory);
            System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft VS Code\Code.exe", $".");
        }
    }
}