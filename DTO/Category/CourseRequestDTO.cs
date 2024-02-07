using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class CourseRequestDTO
{
    [Required]
    public string Name { get; set; } = "";
}

public class CourseRequestEditNameDTO
{
    [Required]
    public string CourseId { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";
}

public class CourseDeleteDTO
{
    [Required]
    public string CourseId { get; set; } = "";
}

public class CourseStatusToggleRequestDTO
{
    [Required]
    public string CourseId { get; set; } = "";
}
