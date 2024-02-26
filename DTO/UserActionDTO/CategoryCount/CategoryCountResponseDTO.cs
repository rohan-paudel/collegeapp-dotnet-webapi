namespace CollegeAppDotnetWebApi;

public class CategoryCountResponseDTO
{
    public int Id { get; set; }
    public int VideoCount { get; set; } = 0;

    public int NoteCount { get; set; } = 0;

    public int ChapterTestCount { get; set; } = 0;
}
