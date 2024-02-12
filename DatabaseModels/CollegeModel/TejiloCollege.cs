using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace CollegeAppDotnetWebApi;

[Index(nameof(Status))]
[Index(nameof(Name))]
public class TejiloCollege
{
    [Key]
    public int Id { get; set; }

    public bool Status { get; set; } = true;

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

    public ICollection<TejiloUser>? Students { get; set; }

    public ICollection<DiscussionModel>? Discussions { get; set; }
}
