using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class UploadVideoRequestDTO
{
    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string VdoCipherId { get; set; } = "";

    public string Description { get; set; } = "";

    public int TotalVideoDuration { get; set; }

    [Required]
    public int TopicId { get; set; }
}
