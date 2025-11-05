namespace AquilaTodos.Weather;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class WeatherController
{
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(ILogger<WeatherController> logger)
    {
        this._logger = logger;
    }

    [HttpGet("{city}")]
    public WeatherDto Get(string city)
    {
        try
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync($"https://goweather.xyz/weather/{city}").Result;
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var weather = JsonSerializer.Deserialize<WeatherDto>(json, options);
            return weather;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw e;
        }
    }

    [HttpGet("{city}/temperature")]
    public string GetTemperature(string city)
    {
        try
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync($"https://goweather.xyz/weather/{city}").Result;
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var weather = JsonSerializer.Deserialize<WeatherDto>(json, options).Temperature;
            return weather;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw e;
        }

    }
}
