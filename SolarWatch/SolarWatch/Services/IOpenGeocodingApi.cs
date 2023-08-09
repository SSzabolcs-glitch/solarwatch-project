namespace SolarWatch.Services
{
    public interface IOpenGeocodingApi
    {
        Task<string> GetGeocodingDataAsync(string city);
    }
}
