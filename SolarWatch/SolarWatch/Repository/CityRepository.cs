using SolarWatch.Data;
using SolarWatch.Models;

namespace SolarWatch.Repository
{
    public class CityRepository : ICityRepository
    {
        public async Task<IEnumerable<City>> GetAll()
        {
            using var dbContext = new SolarWatchContext();
            return dbContext.Cities.ToList();
        }

        public City? GetById(int id)
        {
            using var dbContext = new SolarWatchContext();
            return dbContext.Cities.FirstOrDefault(c => c.Id == id);
        }

        public void Add(City city)
        {
            using var dbContext = new SolarWatchContext();
            dbContext.Add(city);
            dbContext.SaveChanges();
        }

        public void Delete(City city)
        {
            using var dbContext = new SolarWatchContext();
            dbContext.Remove(city);
            dbContext.SaveChanges();
        }

        public void Update(City city)
        {
            using var dbContext = new SolarWatchContext();
            dbContext.Update(city);
            dbContext.SaveChanges();
        }
    }
}
