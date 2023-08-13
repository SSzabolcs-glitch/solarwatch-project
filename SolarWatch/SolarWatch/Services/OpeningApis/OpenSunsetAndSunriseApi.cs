using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

namespace SolarWatch.Services.OpeningApis
{
    public class OpenSunsetAndSunriseApi : IOpenSunsetAndSunriseApi
    {
        private readonly ILogger<OpenSunsetAndSunriseApi> _logger;
        public OpenSunsetAndSunriseApi(ILogger<OpenSunsetAndSunriseApi> logger)
        {
            _logger = logger;
        }
        public async Task<string> GetSunsetAndSunriseDataAsync(double[] data, DateOnly date)
        {
            var lat = data[0];
            var lon = data[1];

            DateOnly dateTime = date;
            string formattedDate = $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}";

            string url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={formattedDate}";

            using var client = new HttpClient();
            _logger.LogInformation("Calling SunsetAndSunrise API with url: {url}", url);

            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
