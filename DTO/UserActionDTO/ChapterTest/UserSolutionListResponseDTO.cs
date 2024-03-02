namespace CollegeAppDotnetWebApi;

public class UserSolutionListResponseDTO
{
    public int Id { get; set; }

    public string Question { get; set; } = "";

    public string QuestionImage { get; set; } = "";

    public string Solution { get; set; } = "";

    public string SolutionImage { get; set; } = "";

    public int AnswerId { get; set; }

    public int? UserAnswerId { get; set; }

    public IEnumerable<ChapterTestOptionUResponseDTO> Options { get; set; }
}
