using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(PhoneNumber), IsUnique = true)]
[Index(nameof(CollegeId))]
[Index(nameof(CourseId))]
[Index(nameof(SubCourseId))]
public class TejiloUser : IdentityUser
{
    [Phone]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    [RegularExpression("^9[0-9]*$", ErrorMessage = "Phone number must start with '9'.")]
    public override string? PhoneNumber { get; set; }

    public bool Status { get; set; } = true;

    [Required]
    public string FullName { get; set; } = "";

    [Required]
    public string Gender { get; set; } = "male";

    [Required]
    public DateTime DateOfBirth { get; set; }

    public string? Address { get; set; }

    [ForeignKey("Id")]
    [Required]
    public int CollegeId { get; set; }

    public TejiloCollege College { get; set; }

    [ForeignKey("Id")]
    public int? CourseId { get; set; }

    public CourseModel? Course { get; set; }

    [ForeignKey("Id")]
    public int? SubCourseId { get; set; }

    public SubCourseModel? SubCourse { get; set; }
}
