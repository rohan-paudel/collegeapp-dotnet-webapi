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

    [Required]
    public IFormFile File { get; set; }

    public string FileNameByDeveloper { get; set; } = "";
}
