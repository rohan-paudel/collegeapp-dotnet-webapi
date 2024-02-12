using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface IDiscussionManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetDiscussion(
        DiscussionRequestDTO discussionRequestDTO
    );

    public Task<
        Results<
            Ok<ResponseDTO<IEnumerable<DiscussionResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetDiscussion(int collegeId, int topicId, int page, bool? discussionStatus);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetQuery(
        QueryRequestDTO queryRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<QueryResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetQuery(int discussionId, int page, bool? queryStatus);
}
