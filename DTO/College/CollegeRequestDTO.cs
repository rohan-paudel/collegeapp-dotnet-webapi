using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class CollegeRequestDTO
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = "";

    [Required]
    [Phone]
    [StringLength(10)]
    public string Mobile { get; set; } = "";

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; } = "";

    [StringLength(50)]
    [Phone]
    public string Telephone { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string Address { get; set; } = "";

    [Required]
    [StringLength(100)]
    [Url]
    public string LocationUrl { get; set; } = "";

    [Required]
    [StringLength(100)]
    [Url]
    public string WebsiteUrl { get; set; } = "";

    [Required]
    [StringLength(100)]
    [Url]
    public string IconUrl { get; set; } = "";

    [Required]
    [StringLength(100)]
    [Url]
    public string ThumbnailUrl { get; set; } = "";

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = "";
}

public class EditCollegeRequestDTO
{
    [Required]
    public string CollegeId { get; set; } = "";

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = "";

    [Required]
    [Phone]
    [StringLength(10)]
    public string Mobile { get; set; } = "";

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; } = "";

    [StringLength(50)]
    public string Telephone { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string Address { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string LocationUrl { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string WebsiteUrl { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string IconUrl { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string ThumbnailUrl { get; set; } = "";

    [Required]
    [StringLength(500)]
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

// public class GetCollegeDTO
// {
//     [Required]
//     public string CollegeId { get; set; } = "";
// }
