using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(DiscussionId))]
[Index(nameof(StudentId))]
public class QueryModel
{
    [Key]
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    [Required]
    public string Query { get; set; } = "";

    [Required]
    public string StudentId { get; set; } = "";

    [ForeignKey("StudentId")]
    public TejiloUser TejiloUser { get; set; }

    [Required]
    public int DiscussionId { get; set; }

    [ForeignKey("DiscussionId")]
    public DiscussionModel Discussion { get; set; }
}
