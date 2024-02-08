using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface ITopicManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetTopic(
        TopicRequestDTO topicRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleTopicStatus(TopicStatusToggleRequestDTO topicStatusToggleRequestDTO);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> EditTopicName(
        TopicRequestEditNameDTO topicRequestEditNameDTO
    );

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteTopic(
        TopicDeleteDTO topicDeleteDTO
    );

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<TopicResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetTopics(
        string? courseId,
        string? subcourseId,
        string? subjectId,
        string? topicName,
        bool? topicStatus
    );
}
