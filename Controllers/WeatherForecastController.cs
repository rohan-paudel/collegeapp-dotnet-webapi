using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CollegeAppDotnetWebApi.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
// [EnableRateLimiting("fixed")]
// // [Authorize(Roles = Roles.User, Policy = "PolicyForMobileDevice")]
// [Authorize(Policy = "PolicyForMobileDevice")]
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
    public dynamic Get()
    {
        IEnumerable<Claim> claimsIdentities;

        Dictionary<string, string> hey = new();

        claimsIdentities = User.Claims;

        foreach (var claim in claimsIdentities)
        {
            hey.Add(claim.Type, claim.Value);
        }
        hey.Add("normal", User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        return hey;
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
