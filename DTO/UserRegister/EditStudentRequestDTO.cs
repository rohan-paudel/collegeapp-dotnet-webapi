using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class EditStudentRequestDTO
{
    [Required]
    public string StudentId { get; set; } = "";

    // [Required]
    // [StringLength(50)]
    // public string CollegeId { get; set; } = "";

    [Required]
    [StringLength(30)]
    public string FullName { get; set; } = "";

    [EmailAddress]
    [Required]
    public string Email { get; set; } = "";

    // [Required]
    // [DataType(DataType.Password)]
    // public string Password { get; set; } = "";

    [Required]
    [Phone]
    [StringLength(10, MinimumLength = 10)]
    [RegularExpression("^9[0-9]*$", ErrorMessage = "Phone number must start with '9'.")]
    public string PhoneNumber { get; set; } = "";

    [Required]
    public string? Gender { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }
}
