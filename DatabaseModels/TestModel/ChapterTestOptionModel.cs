using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeAppDotnetWebApi;

public class ChapterTestOptionModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Option { get; set; } = "";

    public string OptionImage { get; set; } = "";

    [Required]
    public int ChapterTestQuestionId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("ChapterTestQuestionId")]
    public ChapterTestQuestionModel ChapterTestQuestionModel { get; set; }
#pragma warning restore CS8618
}
