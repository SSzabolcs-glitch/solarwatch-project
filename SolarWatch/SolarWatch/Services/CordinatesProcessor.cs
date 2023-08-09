using System.Net;
using System.Text.Json;

namespace SolarWatch.Services
{
    public class CordinatesProcessor : ICordinatesProcessor
    {
        private readonly List<double> Values = new List<double>();
        public double[] ProcessCordinates(string data)
        {
            JsonDocument cityJson = JsonDocument.Parse(data);

            if (cityJson.RootElement.ValueKind == JsonValueKind.Array)
            {
                // Get the first element in the array
                JsonElement firstElement = cityJson.RootElement.EnumerateArray().FirstOrDefault();

                if (firstElement.ValueKind == JsonValueKind.Object)
                {
                    double lat = firstElement.GetProperty("lat").GetDouble();
                    double lon = firstElement.GetProperty("lon").GetDouble();

                    return new double[] { lat, lon };
                }
                else
                {
                    Console.WriteLine("Invalid JSON data format. Expected an object inside the array.");
                }
            }
            else
            {
                Console.WriteLine("Invalid JSON data format. Expected an array.");
            }

            return null; // Return null in case of errors or empty data
        }
    }
}
