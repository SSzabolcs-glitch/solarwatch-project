namespace SolarWatch.Contracts
{
    public record CityUpdateResponse(
        int Id,
        string CityName,
        double Latitude,
        double Longitude,
        string State,
        string Country
    );
}
