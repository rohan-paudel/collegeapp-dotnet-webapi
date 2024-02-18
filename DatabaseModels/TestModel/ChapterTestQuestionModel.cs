using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeAppDotnetWebApi;

public class ChapterTestQuestionModel
{
    [Key]
    public int Id { get; set; }
    public bool Status { get; set; } = true;

    [Required]
    public string Question { get; set; } = "";

    public string QuestionImage { get; set; } = "";

    [Required]
    public string Solution { get; set; } = "";

    public string SolutionImage { get; set; } = "";

    [Required]
    public int NegativeMark { get; set; }

    [Required]
    public int PositiveMark { get; set; }

    public int AnswerId { get; set; }

    [Required]
    public int ChapterTestId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("ChapterTestId")]
    public ChapterTestModel ChapterTest { get; set; }
#pragma warning restore CS8618

    [Required]
    public ICollection<ChapterTestOptionModel> Options { get; set; } =
        new HashSet<ChapterTestOptionModel>();
}
