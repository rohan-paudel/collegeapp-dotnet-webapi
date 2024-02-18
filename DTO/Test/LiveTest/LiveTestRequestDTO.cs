using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class LiveTestRequestDTO
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string Instruction { get; set; } = "";

    [Required]
    public int SubjectId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public DateTime ResultDate { get; set; }

    [Required]
    public int TestDuration { get; set; }
}
