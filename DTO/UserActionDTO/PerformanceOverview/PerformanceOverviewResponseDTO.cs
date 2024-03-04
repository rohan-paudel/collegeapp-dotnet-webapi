namespace CollegeAppDotnetWebApi;

public class PerformanceOverviewResponseDTO
{
    public float PercentageForChapterTest { get; set; }

    public int TotalChapterTest { get; set; }
    public int ChaterTestGivenByUser { get; set; }

    public SubCourseResponseOnlyIdDTO? SubCourse { get; set; }
}

public class ChapterTestCountList
{
    public int count { get; set; }
}
