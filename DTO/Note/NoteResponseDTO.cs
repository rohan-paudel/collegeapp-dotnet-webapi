namespace CollegeAppDotnetWebApi;

public class NoteResponseDTO
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public int TopicId { get; set; }

    public string FileName { get; set; } = "";

    public DateTime CreatedAt { get; set; }

    public TopicResponseOnlyNameDTO Topic { get; set; } = new();
}
