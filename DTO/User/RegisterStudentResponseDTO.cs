namespace CollegeAppDotnetWebApi;

public class RegisterStudentResponseDTO
{
    public string Id { get; set; } = "";

    public bool Status { get; set; } = false;
    public string FullName { get; set; } = "";

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; } = false;

    public string Gender { get; set; } = "others";

    public string Email { get; set; } = "";

    public DateTime DateOfBirth { get; set; }

    public bool EmailConfirmed { get; set; } = false;

    public string? Address { get; set; }

    public CollegeOnlyNameResponseDTO College { get; set; } = new CollegeOnlyNameResponseDTO();
    public SubCourseResponseOnlyNameDTO SubCourse { get; set; } =
        new SubCourseResponseOnlyNameDTO();
    public CourseResponseOnlyNameDTO Course { get; set; } = new CourseResponseOnlyNameDTO();
}
