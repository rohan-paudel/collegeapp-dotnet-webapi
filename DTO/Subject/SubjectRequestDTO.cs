using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class SubjectRequestDTO
{
    [Required]
    public string Name { get; set; } = "";

    public string? ImageUrl { get; set; }
}

public class SubjectRequestEditNameDTO
{
    [Required]
    public string SubjectId { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";
}

public class SubjectDeleteDTO
{
    [Required]
    public string SubjectId { get; set; } = "";
}

public class SubjectStatusToggleRequestDTO
{
    [Required]
    public string SubjectId { get; set; } = "";
}
