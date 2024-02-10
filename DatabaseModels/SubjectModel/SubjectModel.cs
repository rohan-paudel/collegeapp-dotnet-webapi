using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(Name))]
public class SubjectModel
{
    [Key]
    public int Id { get; set; }
    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    public string? ImageUrl { get; set; }

    public ICollection<TopicModel>? Topics { get; set; }

    public ICollection<SubCourseModel> SubCourses { get; set; } = new HashSet<SubCourseModel>();
}
