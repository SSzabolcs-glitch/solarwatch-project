namespace SolarWatch.Services
{
    public interface ISolarWatchProvider
    {
        public Twilight Process(string data, string city);
    }
}
