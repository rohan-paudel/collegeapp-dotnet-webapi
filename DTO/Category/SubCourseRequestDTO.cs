using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class SubCourseRequestDTO
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string CourseId { get; set; } = "";
}

public class SubCourseRequestEditNameDTO
{
    [Required]
    public string SubCourseId { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";
}

public class SubCourseDeleteDTO
{
    [Required]
    public string SubCourseId { get; set; } = "";
}

public class SubCourseStatusToggleRequestDTO
{
    [Required]
    public string SubCourseId { get; set; } = "";
}

public class SubCourseSubjectSetDTO
{
    [Required]
    public string SubCourseId { get; set; } = "";

    [Required]
    public string SubjectId { get; set; } = "";
}
