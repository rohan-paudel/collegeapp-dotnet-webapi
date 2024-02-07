namespace CollegeAppDotnetWebApi;

public class SubjectResponseDTO
{
    public string Id { get; set; } = "";

    public bool Status { get; set; } = true;

    public string Name { get; set; } = "";

    public CourseResponseOnlyNameDTO? Course { get; set; }

    public SubCourseResponseOnlyNameDTO? SubCourse { get; set; }
}
