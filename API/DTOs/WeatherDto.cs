namespace API.DTOs;

public class WeatherDto
{
    public string Location { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public double CurrentC { get; set; }

    public double TodayMinC { get; set; }
    public double TodayMaxC { get; set; }

    public double TomorrowMinC { get; set; }
    public double TomorrowMaxC { get; set; }

    public double AfterTomorrowMinC { get; set; }
    public double AfterTomorrowMaxC { get; set; }
}
