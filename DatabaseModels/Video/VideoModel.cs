using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(TopicId))]
public class VideoModel
{
    [Key]
    public int Id { get; set; }
    public bool Status { get; set; } = true;

    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string VdoCipherId { get; set; } = "";

    [Required]
    public int TotalVideoDuration { get; set; }

    [Required]
    public int TopicId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("TopicId")]
    public TopicModel Topic { get; set; }
#pragma warning restore CS8618
}
