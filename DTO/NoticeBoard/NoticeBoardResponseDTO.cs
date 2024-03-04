namespace CollegeAppDotnetWebApi;

public class NoticeBoardResponseDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = "";

    public string Notice { get; set; } = "";

    public string? FileName { get; set; } = "";
}
