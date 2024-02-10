using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        [FromQuery] string? topicId,
        [FromQuery] string? noteName,
        [FromQuery] bool? noteStatus
    )
    {
        var result = await _noteManagementDL
            .GetNotes(topicId, noteName, noteStatus)
            .ConfigureAwait(false);
        return result;
    }
}
