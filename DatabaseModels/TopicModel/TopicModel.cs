using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(Name))]
[Index(nameof(SubjectId))]
[Index(nameof(SubjectId), nameof(Status))]
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

#pragma warning disable CS8618
    [ForeignKey("SubjectId")]
    public SubjectModel Subject { get; set; }
#pragma warning restore CS8618

    public ICollection<DiscussionModel>? Discussions { get; set; }

    public ICollection<ChapterTestModel>? ChapterTests { get; set; }

    public ICollection<VideoModel>? Videos { get; set; }

    public ICollection<UserNoteModel>? UserNotes { get; set; }
}
