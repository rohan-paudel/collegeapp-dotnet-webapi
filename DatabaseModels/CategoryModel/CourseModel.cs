using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status), nameof(Name))]
public class CourseModel
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    public ICollection<SubCourseModel>? SubCourses { get; set; }
}
