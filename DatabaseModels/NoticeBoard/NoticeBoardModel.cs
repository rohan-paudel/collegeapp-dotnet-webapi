using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(CollegeId))]
public class NoticeBoardModel
{
    [Key]
    public int Id { get; set; }
    public bool Status { get; set; } = true;

    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Notice { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [AllowNull]
    public string? FileName { get; set; }

    [Required]
    public int CollegeId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("CollegeId")]
    public TejiloCollege TejiloCollege { get; set; }
#pragma warning restore CS8618

    public ICollection<SubCourseModel> SubCourses { get; set; } = new HashSet<SubCourseModel>();
}
