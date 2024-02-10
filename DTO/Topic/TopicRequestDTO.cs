using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class TopicRequestDTO
{
    [Required]
    public int SubjectId { get; set; }

    [Required]
    public string Name { get; set; } = "";
}

public class TopicRequestEditNameDTO
{
    public int? SubjectId { get; set; }

    [Required]
    public int TopicId { get; set; }

    [Required]
    public string Name { get; set; } = "";
}

public class TopicDeleteDTO
{
    [Required]
    public int TopicId { get; set; }
}

public class TopicStatusToggleRequestDTO
{
    [Required]
    public int TopicId { get; set; }
}

public class TopicWithIdDTO
{
    [Required]
    public int TopicId { get; set; }
}
