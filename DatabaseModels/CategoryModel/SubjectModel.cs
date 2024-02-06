using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeAppDotnetWebApi;

public class SubjectModel
{
    public string Id { get; set; } = new Guid().ToString();
    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    public ICollection<SubCourseModel>? SubCourses { get; set; }
}
