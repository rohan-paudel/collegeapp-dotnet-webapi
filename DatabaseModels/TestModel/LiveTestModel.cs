using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(SubjectId))]
[Index(nameof(Name))]
public class LiveTestModel
{
    [Key]
    public int Id { get; set; }
    public bool Status { get; set; } = true;

    [Required]
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string Instruction { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public DateTime ResultDate { get; set; }

    [Required]
    public int TestDuration { get; set; }

    [Required]
    public int SubjectId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("SubjectId")]
    public SubjectModel SubjectModel { get; set; }
#pragma warning restore CS8618

    public ICollection<LiveTestQuestionModel>? Questions { get; set; }

    public int Count { get; set; } = 0;
}
