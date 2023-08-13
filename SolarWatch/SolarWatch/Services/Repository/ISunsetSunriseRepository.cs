using SolarWatch.Models;

namespace SolarWatch.Services.Repository
{
    public interface ISunsetSunriseRepository
    {
        IEnumerable<SunsetSunrise> GetAll();
        SunsetSunrise? GetByCityId(int cityId);
        void Add(string city, DateTime[] sunriseSunsetData);
        void Delete(SunsetSunrise sunsetSunrise);
        void Update(SunsetSunrise sunsetSunrise);
    }
}
