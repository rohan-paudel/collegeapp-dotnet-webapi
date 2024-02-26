using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(TopicId))]
public class CategoryCountModel
{
    [Key]
    public int Id { get; set; }

    public int VideoCount { get; set; } = 0;

    public int NoteCount { get; set; } = 0;

    public int ChapterTestCount { get; set; } = 0;

    [Required]
    public int TopicId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("TopicId")]
    public TopicModel TopicModel { get; set; }
#pragma warning restore CS8618
}
