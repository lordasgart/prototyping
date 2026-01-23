using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Noventis.ActionFetcher;

public class Class1
{
    public void RunLoop()
    {
        //Create periodic timer that calls Fetch every second
        var timer = new System.Threading.Timer(_ => Fetch(), null, 0, 1000);

        //Prevent the application from exiting
        Console.ReadLine();
    }

    public void Fetch()
    {
      var client = new HttpClient();
        client.BaseAddress = new Uri("http://192.168.178.174:5026");
        var response = client.GetAsync("/events").Result;
        if (response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var events = lines
                .Select(line => {
                    var parts = line.Split(';');
                    var commandText = parts[0].Trim();
            var guidPart = parts.Length > 1 ? parts[1].Trim() : null;
            return new { commandText, guidPart };
          })
          .Where(e => Guid.TryParse(e.guidPart, out _))
          .Select(e => new { e.commandText, Guid = Guid.Parse(e.guidPart) })
          .ToList();

        //events.Dump("Fetched events (commandText + GUID) from Action Dispatcher");

        Debug.WriteLine($"Fetched {events.Count} events from Action Dispatcher");
      }
      else
      {
        //$"Failed to fetch events: {response.StatusCode}".Dump();
        Debug.WriteLine($"Failed to fetch events: {response.StatusCode}");
      }
    }
}
