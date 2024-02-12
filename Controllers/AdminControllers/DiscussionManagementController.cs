using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

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
        // discussionRequestDTO.StudentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        // discussionRequestDTO.CollegeId = int.Parse(User.FindFirstValue(ClaimTypes.GroupSid)!);
        var result = await _discussionManagementDL
            .SetDiscussion(discussionRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<
            Ok<ResponseDTO<IEnumerable<DiscussionResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetDiscussion(
        [FromQuery] [Required] int collegeId,
        [FromQuery] [Required] int topicId,
        [FromQuery] [Required] int page,
        [FromQuery] bool? discussionStatus
    )
    {
        var result = await _discussionManagementDL
            .GetDiscussion(collegeId, topicId, page, discussionStatus)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetQuery(
        QueryRequestDTO queryRequestDTO
    )
    {
        var result = await _discussionManagementDL.SetQuery(queryRequestDTO).ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<QueryResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetQuery(
        [FromQuery] [Required] int discussionId,
        [FromQuery] [Required] int page,
        bool? queryStatus
    )
    {
        var result = await _discussionManagementDL
            .GetQuery(discussionId, page, queryStatus)
            .ConfigureAwait(false);
        return result;
    }
}
