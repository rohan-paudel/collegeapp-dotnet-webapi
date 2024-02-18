using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface ITestManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetChapterTest(
        ChapterTestRequestDTO chapterTestRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetChapterTestQuestion(ChapterTestQuestionDTO chapterTestQuestionDTO);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetLiveTest(
        LiveTestRequestDTO liveTestRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<TestResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetTest(int? testType, string? name, bool? testStatus);
}
