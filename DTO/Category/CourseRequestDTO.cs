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
    public int CourseId { get; set; }

    [Required]
    public string Name { get; set; } = "";
}

public class CourseDeleteDTO
{
    [Required]
    public int CourseId { get; set; }
}

public class CourseStatusToggleRequestDTO
{
    [Required]
    public int CourseId { get; set; }
}
