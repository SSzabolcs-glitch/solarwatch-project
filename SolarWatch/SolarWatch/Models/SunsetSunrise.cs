namespace SolarWatch.Models
{
    public class SunsetSunrise
    {
        public int Id { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        // Foreign key property
        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
