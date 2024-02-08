using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CollegeAppDotnetWebApi.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
[EnableRateLimiting("fixed")]
// [Authorize(Roles = Roles.User, Policy = "PolicyForMobileDevice")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly UserManager<TejiloUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        UserManager<TejiloUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable
            .Range(1, 5)
            .Select(
                index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    }
            )
            .ToArray();
    }

    [HttpGet]
    public async Task<string> SetRole()
    {
        foreach (var role in Roles.identityRoles)
        {
            await _roleManager.CreateAsync(role).ConfigureAwait(false);
        }

        return "done";
    }
}
