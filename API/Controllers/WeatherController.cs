using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WeatherController(WeatherApiService weatherApiService) : BaseApiController
{
    [HttpGet("forecast")]
    [Authorize]
    public async Task<ActionResult<WeatherDto>> GetForecastWeather([FromQuery] string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            return BadRequest("Location is required");

        try
        {
            var weather = await weatherApiService.GetForecastAsync(location);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
