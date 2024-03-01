namespace CollegeAppDotnetWebApi;

public class UserChapterTestPerformanceResponseDTO
{
    public int Id { get; set; }

    public int Correct { get; set; }

    public int Incorrect { get; set; }

    public int Unanswered { get; set; }

    public float MarksObtained { get; set; }

    public float TotalMark { get; set; }

    public int TotalQuestion { get; set; }
}

public class IsAlreadyGivenChapterTestResponseDTO
{
    public bool IsGiven { get; set; }
}
