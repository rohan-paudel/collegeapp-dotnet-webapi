namespace CollegeAppDotnetWebApi;

public class CourseResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public bool Status { get; set; }
}

public class CourseResponseOnlyNameDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
