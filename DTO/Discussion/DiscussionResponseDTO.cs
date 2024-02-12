namespace CollegeAppDotnetWebApi;

public class DiscussionResponseDTO
{
    public int Id { get; set; }
    public string Discussion { get; set; }

    public StudentResponseOnlyNameDTO Student { get; set; }
}
