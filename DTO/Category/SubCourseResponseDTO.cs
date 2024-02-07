namespace CollegeAppDotnetWebApi;

public class SubCourseResponseDTO
{
    public string Id { get; set; } = "";
    public CourseResponseForSubCourseDTO? Course { get; set; }
    public string Name { get; set; } = "";
    public string Status { get; set; } = "";
}

public class CourseResponseForSubCourseDTO
{
    public string Name { get; set; } = "";
}
