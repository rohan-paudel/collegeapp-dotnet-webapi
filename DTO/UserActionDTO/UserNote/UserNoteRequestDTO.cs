using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class UserNoteRequestDTO
{
    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public int TopicId { get; set; }

    public string StudentId { get; set; } = "";
}

public class UserNoteUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public int TopicId { get; set; }

    public string StudentId { get; set; } = "";
}
