using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class UpdateStudentCourseSubCourse
{
    [Required]
    public string StudentId { get; set; } = "";

    [Required]
    public int CourseId { get; set; }

    [Required]
    public int SubCourseId { get; set; }
}
