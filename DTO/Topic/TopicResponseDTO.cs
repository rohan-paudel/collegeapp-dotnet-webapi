namespace CollegeAppDotnetWebApi;

public class TopicResponseDTO
{
    public string Id { get; set; } = "";

    public bool Status { get; set; } = true;

    public string Name { get; set; } = "";

    public SubjectResponseOnlyNameDTO Subject { get; set; }
}
