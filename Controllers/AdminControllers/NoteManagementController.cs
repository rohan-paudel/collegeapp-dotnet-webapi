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
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<NoteUResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetUNotes([FromQuery] int topicId)
    {
        var result = await _noteManagementDL.GetUNotes(topicId).ConfigureAwait(false);
        return result;
    }

    [HttpDelete]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteNote(
        DeleteNoteRequestDTO deleteNoteRequestDTO
    )
    {
        var result = await _noteManagementDL.DeleteNote(deleteNoteRequestDTO).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleNoteStatus(NoteStatusToggleRequestDTO noteStatusToggleRequestDTO)
    {
        var result = await _noteManagementDL
            .ToggleNoteStatus(noteStatusToggleRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public IResult GetNotePDF([FromQuery] [Required] string fileName)
    {
        var filePath = Path.Combine("/data", "Notes", fileName);
        if (System.IO.File.Exists(filePath))
        {
            return TypedResults.PhysicalFile(filePath, fileDownloadName: $"{fileName}");
        }
        return TypedResults.BadRequest<ResponseDTO<string>>(
            new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Doesnot contains any file"
            }
        );
    }
}
