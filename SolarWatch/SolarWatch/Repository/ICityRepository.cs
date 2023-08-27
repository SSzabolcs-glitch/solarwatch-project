using SolarWatch.Models;

namespace SolarWatch.Repository
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAll();
        City? GetById(int id);
        void Add(City city);
        void Delete(City city);
        void Update(City city);
    }

}
