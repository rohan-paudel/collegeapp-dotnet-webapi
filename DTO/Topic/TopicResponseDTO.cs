namespace CollegeAppDotnetWebApi;

public class TopicResponseDTO
{
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    public string Name { get; set; } = "";

    public SubjectResponseOnlyNameDTO? Subject { get; set; }
}

public class TopicResponseOnlyNameDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
}

public class TopicResponseWithSubCourseDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public SubjectResponseWithSubCourseDTO? Subject { get; set; }
}
