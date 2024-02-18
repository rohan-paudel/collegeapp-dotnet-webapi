using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class ChapterTestRequestDTO
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string Instruction { get; set; } = "";

    [Required]
    public int TopicId { get; set; }
}

public class ChapterTestQuestionDTO
{
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
    public List<ChapterTestOptionDTO> ChapterTestOption { get; set; } = [];

    [Required]
    public int AnswerId { get; set; }
}

public class ChapterTestOptionDTO
{
    [Required]
    public string Option { get; set; } = "";

    [Required]
    public bool IsCorrect { get; set; }

    public string OptionImage { get; set; } = "";
}
