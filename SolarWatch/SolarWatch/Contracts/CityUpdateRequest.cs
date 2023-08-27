using System.ComponentModel.DataAnnotations;

namespace SolarWatch.Contracts
{
    public record CityUpdateRequest(
        [Required] int Id,
        string CityName,
        double Latitude,
        double Longitude,
        string State,
        string Country
    );
}
