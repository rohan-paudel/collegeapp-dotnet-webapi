using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeAppDotnetWebApi;

public class QueryModel
{
    [Key]
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    [Required]
    public string Query { get; set; } = "";

    [Required]
    public string UserId { get; set; } = "";

    [ForeignKey("UserId")]
    public TejiloUser TejiloUser { get; set; }

    [Required]
    public int DiscussionId { get; set; }

    [ForeignKey("DiscussionId")]
    public DiscussionModel Discussion { get; set; }
}
