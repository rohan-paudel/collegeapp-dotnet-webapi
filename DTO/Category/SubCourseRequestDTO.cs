using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class SubCourseRequestDTO
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public int CourseId { get; set; }
}

public class SubCourseRequestEditNameDTO
{
    [Required]
    public int SubCourseId { get; set; }

    [Required]
    public string Name { get; set; } = "";
}

public class SubCourseDeleteDTO
{
    [Required]
    public int SubCourseId { get; set; }
}

public class SubCourseStatusToggleRequestDTO
{
    [Required]
    public int SubCourseId { get; set; }
}

public class SubCourseSubjectSetDTO
{
    [Required]
    public int SubCourseId { get; set; }

    [Required]
    public int SubjectId { get; set; }
}
