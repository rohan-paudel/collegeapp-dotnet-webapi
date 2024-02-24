using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(StudentId))]
[Index(nameof(TopicId))]
[Index(nameof(StudentId), nameof(TopicId))]
public class UserNoteModel
{
    [Key]
    public int Id { get; set; }
    public bool Status { get; set; } = true;

    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public int TopicId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("TopicId")]
    public TopicModel Topic { get; set; }

    [Required]
    public string StudentId { get; set; } = "";

    [ForeignKey("StudentId")]
    public TejiloUser TejiloUser { get; set; }

#pragma warning restore CS8618
}
