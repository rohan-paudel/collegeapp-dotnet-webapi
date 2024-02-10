namespace CollegeAppDotnetWebApi;

public class NoteResponseDTO
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string TopicId { get; set; } = "";

    public string FileName { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public TopicResponseOnlyNameDTO Topic { get; set; } = new();
}
