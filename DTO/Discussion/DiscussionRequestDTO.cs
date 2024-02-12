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
