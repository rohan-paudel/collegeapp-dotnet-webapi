namespace CollegeAppDotnetWebApi;

public class SubCourseResponseDTO
{
    public int Id { get; set; }
    public CourseResponseOnlyNameDTO? Course { get; set; }
    public string Name { get; set; } = "";
    public bool Status { get; set; }
}

public class SubCourseResponseOnlyNameDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public CourseResponseOnlyNameDTO Course { get; set; } = new CourseResponseOnlyNameDTO();
}
