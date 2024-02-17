using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeAppDotnetWebApi;

public class ChapterTestModel
{
    [Key]
    public int Id { get; set; }
    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string Instruction { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public int TopicId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("TopicId")]
    public TopicModel Topic { get; set; }
#pragma warning restore CS8618

    public ICollection<ChapterTestQuestionModel>? Questions { get; set; }

    public int Count { get; set; } = 0;
}
