using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(StudentId))]
[Index(nameof(ChapterTestId))]
[Index(nameof(StudentId), nameof(ChapterTestId))]
public class ChapterTestUserDataModel
{
    [Key]
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    [Required]
    public string StudentId { get; set; } = "";

#pragma warning disable CS8618
    [ForeignKey("StudentId")]
    public TejiloUser TejiloUser { get; set; }

    [Required]
    public int ChapterTestId { get; set; }

    [ForeignKey("ChapterTestId")]
    public ChapterTestModel ChapterTest { get; set; }
#pragma warning restore CS8618

    public int Correct { get; set; }

    public int Incorrect { get; set; }

    public int Unanswered { get; set; }

    public float MarksObtained { get; set; }

    public float TotalMark { get; set; }

    public int TotalQuestion { get; set; }

    public ICollection<ChapterTestDetailedDataModel>? ChapterTestDetailedData { get; set; }
}
