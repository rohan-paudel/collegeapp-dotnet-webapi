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
