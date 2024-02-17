using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;

namespace CollegeAppDotnetWebApi;

public class NoteRequestDTO
{
    [Required]
    [StringLength(60)]
    public string Name { get; set; } = "";

    [Required]
    [StringLength(300)]
    public string Description { get; set; } = "";

    [Required]
    public int TopicId { get; set; }

#pragma warning disable CS8618
    [Required]
    public IFormFile File { get; set; }
#pragma warning restore CS8618

    public string FileNameByDeveloper { get; set; } = "";
}

public class DeleteNoteRequestDTO
{
    [Required]
    public int NoteId { get; set; }
}

public class NoteStatusToggleRequestDTO
{
    [Required]
    public int NoteId { get; set; }
}
