using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class UpdateStudentCourseSubCourse
{
    [Required]
    public string StudentId { get; set; } = "";

    [Required]
    public string CourseId { get; set; } = "";

    [Required]
    public string SubCourseId { get; set; } = "";
}
