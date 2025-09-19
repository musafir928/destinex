using System;
using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WeatherController(AppDbContext context, ITokenService tokenService) : BaseApiController
{
    [HttpGet("current")]
    public async Task<ActionResult<string>> GetCurrentWeather()
    {
        return Ok("sfdfsdfs");
    }
}
