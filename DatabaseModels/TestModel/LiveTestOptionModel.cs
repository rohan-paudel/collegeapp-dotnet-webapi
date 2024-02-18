using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeAppDotnetWebApi;

public class LiveTestOptionModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Option { get; set; } = "";

    public string OptionImage { get; set; } = "";

    [Required]
    public bool IsCorrect { get; set; }

    [Required]
    public int LiveTestQuestionId { get; set; }

#pragma warning disable CS8618
    [ForeignKey("LiveTestQuestionId")]
    public LiveTestQuestionModel LiveTestQuestion { get; set; }
#pragma warning restore CS8618
}
