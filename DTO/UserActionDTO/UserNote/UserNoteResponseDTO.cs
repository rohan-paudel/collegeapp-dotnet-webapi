namespace CollegeAppDotnetWebApi;

public class UserNoteResponseDTO
{
    public int Id { get; set; }
    public bool Status { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}
