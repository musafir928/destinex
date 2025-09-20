using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WeatherController(
    WeatherApiService weatherApiService,
    ILogger<WeatherController> logger
) : BaseApiController
{
    [HttpGet("forecast")]
    [Authorize]
    public async Task<ActionResult<WeatherDto>> GetForecastWeather([FromQuery] string location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            logger.LogWarning("Weather forecast request failed: missing location parameter");
            return BadRequest("Location is required");
        }

        try
        {
            logger.LogInformation("Fetching weather forecast for {Location}", location);

            var weather = await weatherApiService.GetForecastAsync(location);

            logger.LogInformation("Weather forecast retrieved successfully for {Location}", location);

            return Ok(weather);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while fetching weather forecast for {Location}", location);
            return StatusCode(500, ex.Message);
        }
    }
}
