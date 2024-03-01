using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface IChapterTestDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> PostChapterTest(
        PostChapterTestDTO postChapterTestDTO
    );

    public Task<
        Results<
            Ok<ResponseDTO<UserChapterTestPerformanceResponseDTO>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetUserScoreCardForChapterTest(string studentId, int chapterTestId);

    public Task<
        Results<
            Ok<ResponseDTO<IsAlreadyGivenChapterTestResponseDTO>>,
            BadRequest<ResponseDTO<string>>
        >
    > CheckIfAlreadyGivenChapterTest(string studentId, int chapterTestId);

    public Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ResetTheGivenChapterTest(string studentId, int chapterTestId);
}
