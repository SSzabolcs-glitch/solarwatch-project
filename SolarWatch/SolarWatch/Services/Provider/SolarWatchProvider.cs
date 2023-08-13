using System.Text.Json;
using System.Globalization;

namespace SolarWatch.Services.Provider
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

            DateTime sunrise = DateTime.Parse(results.GetProperty("sunrise").GetString());
            DateTime sunset = DateTime.Parse(results.GetProperty("sunset").GetString());

            string formattedSunrise = sunrise.ToString("h:mm:ss tt", CultureInfo.InvariantCulture);
            string formattedSunset = sunset.ToString("h:mm:ss tt", CultureInfo.InvariantCulture);

            Twilight twilight = new Twilight
            {
                City = city,
                Sunrise = formattedSunrise,
                Sunset = formattedSunset
            };

            return twilight;
        }

        public DateTime[] ProvideSunriseSunset(string data)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(data);
            JsonElement results = jsonDocument.RootElement.GetProperty("results");

            DateTime sunrise = DateTime.Parse(results.GetProperty("sunrise").GetString());
            DateTime sunset = DateTime.Parse(results.GetProperty("sunset").GetString());

            return new DateTime[] { sunrise, sunset };
        }
    }
}
