namespace CollegeAppDotnetWebApi;

public class SubjectResponseDTO
{
    public string Id { get; set; } = "";

    public bool Status { get; set; } = true;

    public string Name { get; set; } = "";

    public List<SubCourseResponseOnlyNameDTO>? SubCourses { get; set; }
}

public class SubjectResponseOnlyNameDTO
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
}
