using SolarWatch.Models;

namespace SolarWatch.Services.CityUpdater
{
    public interface ICityUpdater
    {
        City UpdateCityData(int id, string cityName, double longitude, double latitude, string state, string country);
    }
}
