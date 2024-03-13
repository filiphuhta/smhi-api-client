using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly HttpService _httpService;

        public WeatherController()
        {
            _httpService = new HttpService("https://opendata-download-metfcst.smhi.se/api/");
        }

        [HttpGet]
        public async Task<double?> GetCurrentTemperature(string longitude, string latitude)
        {
            try
            { 
                HttpResponseMessage response = await _httpService.GetRequest($"category/pmp3g/version/2/geotype/point/lon/{longitude}/lat/{latitude}/data.json");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(content);
                    if (weatherResponse != null && weatherResponse.timeSeries != null && weatherResponse.timeSeries.Count > 0)
                    {
                        var currentWeather = weatherResponse.timeSeries[0];
                        if (currentWeather != null && currentWeather.parameters != null)
                        {
                            foreach (var param in currentWeather.parameters)
                            {
                                if (param != null && param.name == "t" && param.values != null && param.values.Count > 0)
                                {
                                    return param.values[0];
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error during JSON deserialization: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return null;
        }
    }
}