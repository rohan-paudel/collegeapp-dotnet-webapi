using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class TopicManagementController : ControllerBase
{
    private readonly ITopicManagementDL _topicManagementDL;

    public TopicManagementController(ITopicManagementDL topicManagementDL)
    {
        _topicManagementDL = topicManagementDL;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetTopic(
        TopicRequestDTO topicRequestDTO
    )
    {
        var result = await _topicManagementDL.SetTopic(topicRequestDTO).ConfigureAwait(true);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleTopicStatus(TopicStatusToggleRequestDTO topicStatusToggleRequestDTO)
    {
        var result = await _topicManagementDL
            .ToggleTopicStatus(topicStatusToggleRequestDTO)
            .ConfigureAwait(true);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditTopicName(TopicRequestEditNameDTO topicRequestEditNameDTO)
    {
        var result = await _topicManagementDL
            .EditTopicName(topicRequestEditNameDTO)
            .ConfigureAwait(true);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > DeleteTopic(TopicDeleteDTO topicDeleteDTO)
    {
        var result = await _topicManagementDL.DeleteTopic(topicDeleteDTO).ConfigureAwait(true);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<TopicResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetTopics(
        [FromQuery] string? courseId,
        [FromQuery] string? subcourseId,
        [FromQuery] string? subjectId,
        [FromQuery] string? topicName,
        [FromQuery] bool? topicStatus
    )
    {
        var result = await _topicManagementDL
            .GetTopics(courseId, subcourseId, subjectId, topicName, topicStatus)
            .ConfigureAwait(true);
        return result;
    }
}
