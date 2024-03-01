namespace CollegeAppDotnetWebApi;

public class ChapterTestUResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string Instruction { get; set; } = "";

    public int NumberOfQuestions { get; set; } = 0;

    public DateTime CreatedAt { get; set; }
}

public class ChapterTestQuestionsUResponseDTO
{
    public int Id { get; set; }
    public string Question { get; set; } = "";
    public string QuestionImage { get; set; } = "";
    public int AnswerId { get; set; }

    public string Solution { get; set; } = "";

    public string SolutionImage { get; set; } = "";

    public IEnumerable<ChapterTestOptionUResponseDTO> Options { get; set; }
}

public class ChapterTestOptionUResponseDTO
{
    public int Id { get; set; }

    public string Option { get; set; } = "";

    public string OptionImage { get; set; } = "";

    public bool IsCorrect { get; set; }
}
