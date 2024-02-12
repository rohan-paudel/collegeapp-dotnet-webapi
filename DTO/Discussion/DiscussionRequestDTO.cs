using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class DiscussionRequestDTO
{
    [Required]
    public string Discussion { get; set; }

    [Required]
    public int TopicId { get; set; }

    public string StudentId { get; set; }

    public int CollegeId { get; set; }
}

public class QueryRequestDTO
{
    [Required]
    public int DiscussionId { get; set; }

    [Required]
    public string Query { get; set; }

    [Required]
    public string StudentId { get; set; }
}
