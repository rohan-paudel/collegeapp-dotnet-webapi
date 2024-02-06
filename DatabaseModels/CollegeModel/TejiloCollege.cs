using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Crypto;

namespace CollegeAppDotnetWebApi;

public class TejiloCollege
{
    [Key]
    public string Id { get; set; } = new Guid().ToString();

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Mobile { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";

    [Required]
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

    public ICollection<TejiloUser>? Students { get; set; }
}
