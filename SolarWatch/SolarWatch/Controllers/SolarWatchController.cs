using Microsoft.AspNetCore.Mvc;
using SolarWatch.Models;
using SolarWatch.Services.OpeningApis;
using SolarWatch.Services.Processors;
using SolarWatch.Services.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using SolarWatch.Repository;
using SolarWatch.Services.CityUpdater;
using static SolarWatch.Controllers.AuthController;
using System.ComponentModel.DataAnnotations;

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
        private readonly ICityRepository _cityRepository;
        private readonly ISunsetSunriseRepository _sunsetSunriseRepository;
        private readonly ICityUpdater _cityUpdater;

        public SolarWatchController(ILogger<SolarWatchController> logger, ICordinatesProcessor cordinatesProcessor, IOpenGeocodingApi openCordinatesApi, 
            ISolarWatchProvider solarWatchProvider, IOpenSunsetAndSunriseApi openSunsetAndSunriseApi, ICityRepository cityRepository, ISunsetSunriseRepository sunsetSunriseRepository, ICityUpdater cityUpdater)
        {
            _logger = logger;
            _openGeocodingApi = openCordinatesApi;
            _openSunsetAndSunriseApi = openSunsetAndSunriseApi;
            _cordinatesProcessor = cordinatesProcessor;
            _solarWatchProvider = solarWatchProvider;
            _cityRepository = cityRepository;
            _sunsetSunriseRepository = sunsetSunriseRepository;
            _cityUpdater = cityUpdater;
        }

        public record UpdateRequest([Required]int Id, string cityName, double Latitude, double Longitude, string State, string Country);

        [HttpGet(), Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<Twilight>> GetTwilight(string city, DateOnly date)
        {
            try
            {
                var provideGeocodingData = await _openGeocodingApi.GetGeocodingDataAsync(city);
                var processCodrinates = _cordinatesProcessor.ProcessCordinates(provideGeocodingData);
                var provideSunriseAndSunsetData = await _openSunsetAndSunriseApi.GetSunsetAndSunriseDataAsync(processCodrinates, date);

                // Fill the DB With Data
                var cityData = _cordinatesProcessor.ProvideCity(provideGeocodingData);
                var sunsetSunrise = _solarWatchProvider.ProvideSunriseSunset(provideSunriseAndSunsetData);

                _cityRepository.Add(cityData);
                _sunsetSunriseRepository.Add(city, sunsetSunrise);

                return Ok(_solarWatchProvider.Process(provideSunriseAndSunsetData, city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting twilight data.");
                return NotFound("Error getting twilight data.");
            }
        }

        [HttpGet("GetAll"), Authorize(Roles = "User, Admin")]
        public async Task<IEnumerable<City>> GetAllCities()
        {
            try
            {
                var cities = await _cityRepository.GetAll();
                return cities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting twilight data.");
                throw;
            }
        }

        [HttpGet("Update"), Authorize(Roles = "Admin")]
        public City UpdateCity([Required] int Id, string CityName, double Latitude, double Longitude, string State, string Country)
        {
            try
            {
                var updatedCity = _cityUpdater.UpdateCityData(Id, CityName, Latitude, Longitude, State, Country);
                return updatedCity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting twilight data.");
                throw;
            }
        }
    }
}
