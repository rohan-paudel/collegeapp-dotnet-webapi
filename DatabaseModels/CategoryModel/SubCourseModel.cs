using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(Name))]
[Index(nameof(CourseId))]
public class SubCourseModel
{
    [Key]
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    [Required]
    [ForeignKey("Id")]
    public int CourseId { get; set; }

    public CourseModel Course { get; set; }

    public ICollection<SubjectModel> Subjects { get; set; } = new HashSet<SubjectModel>();
}
