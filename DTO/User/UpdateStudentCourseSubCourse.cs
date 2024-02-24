using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class UpdateStudentCourseSubCourse
{
    public string StudentId { get; set; } = "";

    [Required]
    public int SubCourseId { get; set; }
}
