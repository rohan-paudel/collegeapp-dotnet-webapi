using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class NoteManagementController : ControllerBase
{
    private readonly INoteManagementDL _noteManagementDL;

    public NoteManagementController(INoteManagementDL noteManagementDL)
    {
        _noteManagementDL = noteManagementDL;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetNote(
        [FromForm] NoteRequestDTO noteRequestDTO
    )
    {
        var result = await _noteManagementDL.SetNote(noteRequestDTO).ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<NoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetNotes(
        [FromQuery] int? topicId,
        [FromQuery] string? noteName,
        [FromQuery] [Required] int page,
        [FromQuery] bool? noteStatus
    )
    {
        var result = await _noteManagementDL
            .GetNotes(topicId, noteName, page, noteStatus)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public ActionResult<PhysicalFileResult> GetNotePDF([FromQuery] [Required] string fileName)
    {
        if (
            string.IsNullOrEmpty(fileName)
            || Path.GetInvalidFileNameChars().Any(c => fileName.Contains(c))
        )
        {
            return BadRequest(
                new ResponseDTO<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Doesnot contains any file"
                }
            );
        }

        var filePath = Path.Combine("Uploads", "Notes", fileName);

        try
        {
            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest(
                    new ResponseDTO<string>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Doesnot contains any file"
                    }
                );
            }

            // Set the file content type
            var contentType = "application/pdf";

            // Send the file to the user
            return PhysicalFile(filePath, contentType);

            // Determine the content type based on the file extension
            // var contentType = GetContentType(fileName);
            // var bytes = await System.IO.File.ReadAllBytesAsync(filePath).ConfigureAwait(false);

            // // Serve the file using FileStreamResult
            // return TypedResults.Ok(File(bytes, contentType, Path.GetFileName(filePath)));
        }
        catch (Exception)
        {
            return BadRequest(
                new ResponseDTO<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something went wrong"
                }
            );
        }
    }

    private string GetContentType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
        {
            contentType = "application/octet-stream"; // Default content type
        }
        return contentType;
    }
}
