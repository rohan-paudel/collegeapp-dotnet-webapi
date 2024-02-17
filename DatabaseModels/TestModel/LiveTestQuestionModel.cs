using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class LiveTestQuestionModel
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

    [Required]
    public int AnswerId { get; set; }

    [Required]
    public ICollection<LiveTestOptionModel> Options { get; set; } =
        new HashSet<LiveTestOptionModel>();
}
