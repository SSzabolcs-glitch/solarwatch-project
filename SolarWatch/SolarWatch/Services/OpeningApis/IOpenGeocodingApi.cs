namespace SolarWatch.Services.OpeningApis
{
    public interface IOpenGeocodingApi
    {
        Task<string> GetGeocodingDataAsync(string city);
    }
}
