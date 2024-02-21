using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class QuoteRequestDTO
{
    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
}
