using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class UserNoteManagementController : ControllerBase
{
    private readonly IUserNoteManagementDL _userNoteManagementDL;

    public UserNoteManagementController(IUserNoteManagementDL userNoteManagementDL)
    {
        _userNoteManagementDL = userNoteManagementDL;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetUserNote(UserNoteRequestDTO userNoteRequestDTO)
    {
        return await _userNoteManagementDL.SetUserNote(userNoteRequestDTO).ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<UserNoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetUserNotes(int topicId, string studentId, bool? noteStatus)
    {
        return await (
            _userNoteManagementDL.GetUserNotes(topicId, studentId, noteStatus)
        ).ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditUserNote(UserNoteUpdateDTO userNoteUpdateDTO)
    {
        return await _userNoteManagementDL.EditUserNote(userNoteUpdateDTO).ConfigureAwait(false);
    }
}
