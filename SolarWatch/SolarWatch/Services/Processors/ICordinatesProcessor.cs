using SolarWatch.Models;

namespace SolarWatch.Services.Processors
{
    public interface ICordinatesProcessor
    {
        public double[] ProcessCordinates(string data);
        City ProvideCity(string data);
    }
}
