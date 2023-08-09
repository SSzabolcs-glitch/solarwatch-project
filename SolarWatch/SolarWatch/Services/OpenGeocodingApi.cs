using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

namespace SolarWatch.Services
{
    public class OpenGeocodingApi : IOpenGeocodingApi
    {
        private readonly ILogger<OpenGeocodingApi> _logger;
        public OpenGeocodingApi(ILogger<OpenGeocodingApi> logger)
        {
            _logger = logger;
        }
        public async Task<string> GetGeocodingDataAsync(string city)
        {
            string apiKey = "e5554c2009676b2c4ec4e8a68e48858b";
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=5&appid={apiKey}";

            using var client = new HttpClient();
            _logger.LogInformation("Calling Geocoding API with url: {url}", url);

            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
