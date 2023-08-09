using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Text.Json;

namespace SolarWatch.Services
{
    public class SolarWatchProvider : ISolarWatchProvider
    {
        private readonly ILogger<SolarWatchProvider> _logger;
        public SolarWatchProvider(ILogger<SolarWatchProvider> logger)
        {
            _logger = logger;
        }
        public Twilight Process(string data, string city)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(data);
            JsonElement results = jsonDocument.RootElement.GetProperty("results");

            string sunrise = results.GetProperty("sunrise").GetString();
            string sunset = results.GetProperty("sunset").GetString();

            Twilight twilight = new Twilight
            {
                City = city,
                Sunrise = sunrise,
                Sunset = sunset
            };

            return twilight;
        }
    }
}
