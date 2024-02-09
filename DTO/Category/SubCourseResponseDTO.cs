namespace CollegeAppDotnetWebApi;

public class SubCourseResponseDTO
{
    public string Id { get; set; } = "";
    public CourseResponseOnlyNameDTO? Course { get; set; }
    public string Name { get; set; } = "";
    public bool Status { get; set; }
}

public class SubCourseResponseOnlyNameDTO
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
}
