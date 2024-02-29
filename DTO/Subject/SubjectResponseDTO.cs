namespace CollegeAppDotnetWebApi;

public class SubjectResponseDTO
{
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    public string Name { get; set; } = "";

    public List<SubCourseResponseOnlyNameDTO>? SubCourses { get; set; }
}

public class SubjectResponseOnlyNameDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class SubjectResponseForUser
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string ImageUrl { get; set; } = "";
}

public class SubjectResponseWithSubCourseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public List<SubCourseResponseOnlyNameDTO>? SubCourses { get; set; }
}
