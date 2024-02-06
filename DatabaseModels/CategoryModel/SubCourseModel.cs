using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeAppDotnetWebApi;

public class SubCourseModel
{
    [Key]
    public string Id { get; set; } = new Guid().ToString();

    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    [Required]
    [ForeignKey("Id")]
    public string CourseId { get; set; } = "";

    [Required]
    public CourseModel Course { get; set; } = new();
}
