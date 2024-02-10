using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(Name))]
[Index(nameof(TopicId))]
public class NoteModel
{
    [Key]
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string FileName { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public int TopicId { get; set; }

    [ForeignKey("TopicId")]
    public TopicModel Topic { get; set; }
}
