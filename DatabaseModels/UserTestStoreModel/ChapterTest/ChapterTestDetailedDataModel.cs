using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

[Index(nameof(ChapterTestUserDataId))]
public class ChapterTestDetailedDataModel
{
    [Key]
    public int Id { get; set; }

    public bool Status { get; set; } = true;

    [Required]
    public int ChapterTestQuestionId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("ChapterTestQuestionId")]
    public ChapterTestQuestionModel ChapterTestQuestion { get; set; }

    [AllowNull]
    public int? UserAnswerId { get; set; }

    [ForeignKey("UserAnswerId")]
    public ChapterTestOptionModel UserAnswer { get; set; }

    [Required]
    public int ChapterTestUserDataId { get; set; }

    [ForeignKey("ChapterTestUserDataId")]
    public ChapterTestUserDataModel ChapterTestUserData { get; set; }
#pragma warning restore CS8618
}
