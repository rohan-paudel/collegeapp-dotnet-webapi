namespace CollegeAppDotnetWebApi;

public class QuestionResponseFromDatabaseDTO
{
    public int Id { get; set; }

    public int AnswerId { get; set; }

    public float PositiveMark { get; set; }

    public float NegativeMark { get; set; }
}
