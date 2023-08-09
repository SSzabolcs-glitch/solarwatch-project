using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarWatch.Services;
using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SolarWatch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolarWatchController : ControllerBase
    {
        private readonly ILogger<SolarWatchController> _logger;
        private readonly ISolarWatchProvider _solarWatchProvider;
        private readonly ICordinatesProcessor _cordinatesProcessor;
        private readonly IOpenGeocodingApi _openGeocodingApi;
        private readonly IOpenSunsetAndSunriseApi _openSunsetAndSunriseApi;

        public SolarWatchController(ILogger<SolarWatchController> logger, ICordinatesProcessor cordinatesProcessor, IOpenGeocodingApi openCordinatesApi, ISolarWatchProvider solarWatchProvider, IOpenSunsetAndSunriseApi openSunsetAndSunriseApi)
        {
            _logger = logger;
            _openGeocodingApi = openCordinatesApi;
            _openSunsetAndSunriseApi = openSunsetAndSunriseApi;
            _cordinatesProcessor = cordinatesProcessor;
            _solarWatchProvider = solarWatchProvider;
        }

        [HttpGet()]
        public async Task<ActionResult<Twilight>> GetTwilight(string city, DateOnly date)
        {
            try
            {
                var provideGeocodingData = await _openGeocodingApi.GetGeocodingDataAsync(city);
                var processCodrinates = _cordinatesProcessor.ProcessCordinates(provideGeocodingData);
                var provideSunriseAndSunsetData = await _openSunsetAndSunriseApi.GetSunsetAndSunriseDataAsync(processCodrinates, date);
                return Ok(_solarWatchProvider.Process(provideSunriseAndSunsetData, city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting twilight data.");
                return NotFound("Error getting twilight data.");
            }
        }
    }
}
