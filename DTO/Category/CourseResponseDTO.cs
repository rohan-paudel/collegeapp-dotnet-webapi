namespace CollegeAppDotnetWebApi;

public class CourseResponseDTO
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public bool Status { get; set; }
}

public class CourseResponseOnlyNameDTO
{
    public string Name { get; set; } = "";
}
