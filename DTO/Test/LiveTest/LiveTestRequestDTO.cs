using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class LiveTestRequestDTO
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string Instruction { get; set; } = "";

    [Required]
    public int SubjectId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public DateTime ResultDate { get; set; }

    [Required]
    public int TestDuration { get; set; }
}

public class LiveTestQuestionDTO
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
    public int LiveTestId { get; set; }

    [Required]
    public List<LiveTestOptionDTO> Options { get; set; } = [];
}

public class LiveTestOptionDTO
{
    [Required]
    public string Option { get; set; } = "";

    [Required]
    public bool IsCorrect { get; set; }

    public string OptionImage { get; set; } = "";
}
