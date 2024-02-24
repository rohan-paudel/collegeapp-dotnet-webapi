namespace CollegeAppDotnetWebApi;

public class VideoResponseDTO
{
    public int Id { get; set; }
    public bool Status { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public string VdoCipherId { get; set; } = "";

    public int TotalVideoDuration { get; set; }
}
