using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace CollegeAppDotnetWebApi;

public class LoginRequestDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
