namespace SolarWatch.Services
{
    public interface IOpenSunsetAndSunriseApi
    {
        Task<string> GetSunsetAndSunriseDataAsync(double[] data, DateOnly date);
    }
}
