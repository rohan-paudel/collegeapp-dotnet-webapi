using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(StudentId))]
[Index(nameof(CollegeId))]
[Index(nameof(TopicId))]
[Index(nameof(CollegeId), nameof(TopicId))]
public class DiscussionModel
{
    [Key]
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    [Required]
    public string Discussion { get; set; } = "";

    [Required]
    public string StudentId { get; set; } = "";

    [ForeignKey("StudentId")]
    public TejiloUser TejiloUser { get; set; }

    [Required]
    public int TopicId { get; set; }

    [ForeignKey("TopicId")]
    public TopicModel TopicModel { get; set; }

    [Required]
    public int CollegeId { get; set; }

    [ForeignKey("CollegeId")]
    public TejiloCollege TejiloCollege { get; set; }

    public ICollection<QueryModel>? Queries { get; set; }
}
