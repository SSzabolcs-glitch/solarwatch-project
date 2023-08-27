using SolarWatch.Models;
using SolarWatch.Repository;

namespace SolarWatch.Services.CityUpdater
{
    public class CityUpdater : ICityUpdater
    {
        public readonly ICityRepository _cityRepository;

        public CityUpdater(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public City UpdateCityData(int id, string cityName, double longitude, double latitude, string state, string country)
        {
            var existingCity = _cityRepository.GetById(id);

            if (existingCity != null)
            {
                // Update only non-null properties
                if (cityName != null)
                    existingCity.Name = cityName;
                if (longitude != 0)
                    existingCity.Longitude = longitude;
                if (latitude != 0)
                    existingCity.Latitude = latitude;
                if (state != null)
                    existingCity.State = state;
                if (country != null)
                    existingCity.Country = country;

                _cityRepository.Update(existingCity);

                return existingCity; // Return the updated city entity if needed
            }
            throw new Exception($"No city found with id {id}");
        }
    }
}
