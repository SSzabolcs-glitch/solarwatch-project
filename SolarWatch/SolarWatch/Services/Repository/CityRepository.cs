using SolarWatch.Models;

namespace SolarWatch.Services.Repository
{
    public class CityRepository : ICityRepository
    {
        public IEnumerable<City> GetAll()
        {
            using var dbContext = new SolarWatchDbContext();
            return dbContext.Cities.ToList();
        }

        public City? GetByName(string name)
        {
            using var dbContext = new SolarWatchDbContext();
            return dbContext.Cities.FirstOrDefault(c => c.Name == name);
        }

        public void Add(City city)
        {
            using var dbContext = new SolarWatchDbContext();
            dbContext.Add(city);
            dbContext.SaveChanges();
        }

        public void Delete(City city)
        {
            using var dbContext = new SolarWatchDbContext();
            dbContext.Remove(city);
            dbContext.SaveChanges();
        }

        public void Update(City city)
        {
            using var dbContext = new SolarWatchDbContext();
            dbContext.Update(city);
            dbContext.SaveChanges();
        }
    }
}
