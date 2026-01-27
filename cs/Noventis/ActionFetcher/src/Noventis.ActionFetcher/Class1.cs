using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Noventis.ActionFetcher;

public class ActionFetcherEvent
{
    public string CommandText { get; set; }
    public Guid Guid { get; set; }
}

public class Class1
{
    List<ActionFetcherEvent> processedEvents = new List<ActionFetcherEvent>();

    public static bool firstLoop = true;

    public void RunLoop()
    {
        //Not needed, as we do not process existing events, but only new
        //Thread.Sleep(10000); //Initial delay to allow other services to start

        //Create periodic timer that calls Fetch every second
        //var timer = new System.Threading.Timer(_ => Fetch(), null, 0, 5000);
        while (true)
        {
            Fetch();
            System.Threading.Thread.Sleep(5000); // wait 5000 ms synchronously
        }

        //Prevent the application from exiting
        //Console.ReadLine();
    }

    public void Fetch()
    {
        try
        {
            List<ActionFetcherEvent> actionFetcherEvents;
            actionFetcherEvents = new List<ActionFetcherEvent>();

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.178.174:5026");
            //client.BaseAddress = new Uri("http://192.168.178.85:5026");
            var response = client.GetAsync("/events").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var events = lines
                    .Select(line =>
                    {
                        var parts = line.Split(';');
                        var commandText = parts[0].Trim();
                        var guidPart = parts.Length > 1 ? parts[1].Trim() : null;
                        return new { commandText, guidPart };
                    })
                    .Where(e => Guid.TryParse(e.guidPart, out _))
                    .Select(e => new { e.commandText, Guid = Guid.Parse(e.guidPart) })
                    .ToList();

                //events.Dump("Fetched events (commandText + GUID) from Action Dispatcher");

                ActionFetcherEvent[] fetchedEvents = events
                    .Select(e => new ActionFetcherEvent
                    {
                        CommandText = e.commandText,
                        Guid = e.Guid
                    })
                    .ToArray();

                Debug.WriteLine($"Fetched {events.Count} events from Action Dispatcher");

                if (fetchedEvents.Length == 0)
                {
                    //clear processed events if no events are fetched too
                    processedEvents.Clear();
                }
                else
                {
                    if (firstLoop)
                    {
                        //On first loop, mark all fetched events as processed to avoid executing old events
                        processedEvents.AddRange(fetchedEvents);
                        firstLoop = false;
                    }
                }

                foreach (var fetchedEvent in fetchedEvents)
                {
                    //Check if event was already processed
                    if (!processedEvents.Any(e => e.Guid == fetchedEvent.Guid))
                    {
                        //Process event
                        Debug.WriteLine($"Processing event: {fetchedEvent.CommandText} with GUID: {fetchedEvent.Guid}");
                        //Execute AHK command
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            //Create a temporary AHK file
                            var tempAhkFilePath = Path.Combine(Path.GetTempPath(), $"temp_{Guid.NewGuid()}.ahk");
                            File.WriteAllText(tempAhkFilePath, fetchedEvent.CommandText);

                            //Start AHK process
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "explorer.exe",
                                Arguments = $"\"{tempAhkFilePath}\"",
                                UseShellExecute = true
                            });

                            //Optionally delete the temporary file after some time
                            //File.Delete(tempAhkFilePath);
                        }
                        else
                        {
                            Debug.WriteLine("AutoHotkey execution is only supported on Windows.");
                        }

                        //Mark event as processed
                        processedEvents.Add(fetchedEvent);
                    }
                }
            }
            else
            {
                //$"Failed to fetch events: {response.StatusCode}".Dump();
                Debug.WriteLine($"Failed to fetch events: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in Fetch: {ex.Message}");
        }
    }
}
