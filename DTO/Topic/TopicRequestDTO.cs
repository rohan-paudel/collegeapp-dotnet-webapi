using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class TopicRequestDTO
{
    [Required]
    public string SubjectId { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";
}

public class TopicRequestEditNameDTO
{
    public string? SubjectId { get; set; }

    [Required]
    public string TopicId { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";
}

public class TopicDeleteDTO
{
    [Required]
    public string TopicId { get; set; } = "";
}

public class TopicStatusToggleRequestDTO
{
    [Required]
    public string TopicId { get; set; } = "";
}

public class TopicWithIdDTO
{
    [Required]
    public string TopicId { get; set; } = "";
}
