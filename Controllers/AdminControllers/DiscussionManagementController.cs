using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[Authorize(Policy = "PolicyForMobileDevice")]
[ApiController]
[Route("api/[controller]/[Action]")]
public class DiscussionManagementController : ControllerBase
{
    private readonly IDiscussionManagementDL _discussionManagementDL;

    public DiscussionManagementController(IDiscussionManagementDL discussionManagementDL)
    {
        _discussionManagementDL = discussionManagementDL;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetDiscussion(DiscussionRequestDTO discussionRequestDTO)
    {
        discussionRequestDTO.StudentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        discussionRequestDTO.CollegeId = int.Parse(User.FindFirstValue(ClaimTypes.GroupSid)!);
        var result = await _discussionManagementDL
            .SetDiscussion(discussionRequestDTO)
            .ConfigureAwait(false);
        return result;
    }
}
