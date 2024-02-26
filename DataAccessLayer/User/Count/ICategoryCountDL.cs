using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface ICategoryCountDL
{
    public Task<
        Results<Ok<ResponseDTO<CategoryCountResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetTopicsCategoryCount(int topicId);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> UpdateVideoCount(
        int topicId
    );

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> UpdateNoteCount(
        int topicId
    );
}
