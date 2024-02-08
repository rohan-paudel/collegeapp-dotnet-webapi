using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class CollegeRequestDTO
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Mobile { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";

    public string Telephone { get; set; } = "";

    [Required]
    public string Address { get; set; } = "";

    [Required]
    public string LocationUrl { get; set; } = "";

    [Required]
    public string WebsiteUrl { get; set; } = "";

    [Required]
    public string IconUrl { get; set; } = "";

    [Required]
    public string ThumbnailUrl { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";
}

public class CollegeStatusToggleRequestDTO
{
    [Required]
    public string CollegeId { get; set; } = "";
}

public class CollegeDeleteDTO
{
    [Required]
    public string CollegeId { get; set; } = "";
}
