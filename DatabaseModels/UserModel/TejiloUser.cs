using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(PhoneNumber), IsUnique = true)]
public class TejiloUser : IdentityUser
{
    [Phone]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    [RegularExpression("^9[0-9]*$", ErrorMessage = "Phone number must start with '9'.")]
    public override string? PhoneNumber { get; set; }
    public string? FullName { get; set; }

    public string? Gender { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    public string? Address { get; set; }

    [ForeignKey("Id")]
    [Required]
    public string CollegeId { get; set; }

    public TejiloCollege College { get; set; }
}
