using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(Name))]
[Index(nameof(SubjectId))]
public class TopicModel
{
    [Key]
    public int Id { get; set; }
    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    public string? ImageUrl { get; set; }

    public ICollection<NoteModel>? Notes { get; set; }

    [Required]
    public int SubjectId { get; set; }

    [ForeignKey("SubjectId")]
    public SubjectModel Subject { get; set; }

    public ICollection<DiscussionModel>? Discussions { get; set; }
}
