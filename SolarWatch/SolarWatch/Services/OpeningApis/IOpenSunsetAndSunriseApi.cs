namespace SolarWatch.Services.OpeningApis
{
    public interface IOpenSunsetAndSunriseApi
    {
        Task<string> GetSunsetAndSunriseDataAsync(double[] data, DateOnly date);
    }
}
