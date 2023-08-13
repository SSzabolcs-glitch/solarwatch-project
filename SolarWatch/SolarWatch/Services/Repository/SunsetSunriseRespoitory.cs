using Microsoft.EntityFrameworkCore;
using SolarWatch.Models;

namespace SolarWatch.Services.Repository
{
    public class SunsetSunriseRespoitory : ISunsetSunriseRepository
    {
        public IEnumerable<SunsetSunrise> GetAll()
        {
            using var dbContext = new SolarWatchDbContext();
            return dbContext.SunsetSunrises.ToList();
        }

        public SunsetSunrise? GetByCityId(int cityId)
        {
            using var dbContext = new SolarWatchDbContext();
            return dbContext.SunsetSunrises.FirstOrDefault(c => c.CityId == cityId);
        }

        public void Add(string city, DateTime[] sunriseSunsetData)
        {
            using var dbContext = new SolarWatchDbContext();
            var cityData = dbContext.Cities.FirstOrDefault(c => c.Name == city);
            var newSunsetSunrise = new SunsetSunrise { Sunrise = sunriseSunsetData[0], Sunset = sunriseSunsetData[1], City = cityData };
            dbContext.Add(newSunsetSunrise);
            dbContext.SaveChanges();
        }

        public void Delete(SunsetSunrise sunsetSunrise)
        {
            using var dbContext = new SolarWatchDbContext();
            dbContext.Remove(sunsetSunrise);
            dbContext.SaveChanges();
        }

        public void Update(SunsetSunrise sunsetSunrise)
        {
            using var dbContext = new SolarWatchDbContext();
            dbContext.Update(sunsetSunrise);
            dbContext.SaveChanges();
        }
    }
}
