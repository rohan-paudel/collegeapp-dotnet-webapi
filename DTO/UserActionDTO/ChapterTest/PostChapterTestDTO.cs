using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class PostChapterTestDTO
{
    [Required]
    public int ChapterTestId { get; set; }

    public string StudentId { get; set; } = "";

    public int Correct { get; set; }

    public int Incorrect { get; set; }

    public int Unanswered { get; set; }

    public float MarksObtained { get; set; }

    public float TotalMark { get; set; }

    public int TotalQuestion { get; set; }

    [Required]
    public List<PostChapterTestOptionChosedDTO> ChapterTestDetailedData { get; set; } = [];
}

public class PostChapterTestOptionChosedDTO
{
    public int ChapterTestQuestionId { get; set; }

    public int? UserAnswerId { get; set; }
}
