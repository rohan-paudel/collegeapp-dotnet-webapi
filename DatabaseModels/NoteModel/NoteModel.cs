using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeAppDotnetWebApi;

public class NoteModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string FileName { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [ForeignKey("Id")]
    public string TopicId { get; set; } = "";

    public TopicModel Topic { get; set; }
}
