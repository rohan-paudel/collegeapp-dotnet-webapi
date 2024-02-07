using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(Name))]
public class TopicModel
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    public string? ImageUrl { get; set; }

    [Required]
    [ForeignKey("Id")]
    public string SubjectId { get; set; } = "";

    public SubjectModel Subject { get; set; }
}
