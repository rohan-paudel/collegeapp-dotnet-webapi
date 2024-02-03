using Microsoft.AspNetCore.Identity;

namespace CollegeAppDotnetWebApi;

public class TejiloUser : IdentityUser
{
    public string? Gender { get; set; }
}
