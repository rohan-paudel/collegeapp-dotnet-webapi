using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class SubCourseRequestDTO
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string CourseId { get; set; } = "";
}
