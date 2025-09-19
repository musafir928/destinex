using System.Text.Json;
using API.DTOs;

namespace API.Services;

public class WeatherApiService(HttpClient httpClient, ILogger<WeatherApiService> logger, IConfiguration config)
{
    public async Task<WeatherDto> GetForecastAsync(string location)
    {
        if (config["API_KEY"] is not string API_KEY)
        {
            logger.LogError("No API_KEY provided");
            throw new Exception("Internal Server Error");
        }

        var url = $"https://api.weatherapi.com/v1/forecast.json?key={API_KEY}&q={location}&days=3&aqi=no&alerts=no";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Weather API call failed with status {StatusCode}", response.StatusCode);
            throw new Exception("Weather API error");
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        var root = doc.RootElement;

        var dto = new WeatherDto
        {
            Location = root.GetProperty("location").GetProperty("name").GetString() ?? "",
            Region = root.GetProperty("location").GetProperty("region").GetString() ?? "",
            Country = root.GetProperty("location").GetProperty("country").GetString() ?? "",
            CurrentC = root.GetProperty("current").GetProperty("temp_c").GetDouble(),
        };

        var forecastDays = root.GetProperty("forecast").GetProperty("forecastday").EnumerateArray().ToList();

        if (forecastDays.Count >= 1)
        {
            dto.TodayMinC = forecastDays[0].GetProperty("day").GetProperty("mintemp_c").GetDouble();
            dto.TodayMaxC = forecastDays[0].GetProperty("day").GetProperty("maxtemp_c").GetDouble();
        }

        if (forecastDays.Count >= 2)
        {
            dto.TomorrowMinC = forecastDays[1].GetProperty("day").GetProperty("mintemp_c").GetDouble();
            dto.TomorrowMaxC = forecastDays[1].GetProperty("day").GetProperty("maxtemp_c").GetDouble();
        }

        if (forecastDays.Count >= 3)
        {
            dto.AfterTomorrowMinC = forecastDays[2].GetProperty("day").GetProperty("mintemp_c").GetDouble();
            dto.AfterTomorrowMaxC = forecastDays[2].GetProperty("day").GetProperty("maxtemp_c").GetDouble();
        }

        return dto;
    }
}
