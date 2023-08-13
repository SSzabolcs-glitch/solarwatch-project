namespace SolarWatch.Services.Provider
{
    public interface ISolarWatchProvider
    {
        public Twilight Process(string data, string city);
        DateTime[] ProvideSunriseSunset(string data);
    }
}
