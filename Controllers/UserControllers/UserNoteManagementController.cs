using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "PolicyForMobileDevice")]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetUserNote(UserNoteRequestDTO userNoteRequestDTO)
    {
        userNoteRequestDTO.StudentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return await _userNoteManagementDL.SetUserNote(userNoteRequestDTO).ConfigureAwait(false);
    }

    [HttpGet]
    [Authorize(Policy = "PolicyForMobileDevice")]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<UserNoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetUserNotes(int topicId)
    {
        return await (
            _userNoteManagementDL.GetUserNotes(
                topicId,
                User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                true
            )
        ).ConfigureAwait(false);
    }

    [HttpPost]
    [Authorize(Policy = "PolicyForMobileDevice")]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditUserNote(UserNoteUpdateDTO userNoteUpdateDTO)
    {
        userNoteUpdateDTO.StudentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return await _userNoteManagementDL.EditUserNote(userNoteUpdateDTO).ConfigureAwait(false);
    }
}
