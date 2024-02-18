using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class TestManagementController : ControllerBase
{
    private readonly ITestManagementDL _testManagementDL;

    public TestManagementController(ITestManagementDL testManagementDL)
    {
        _testManagementDL = testManagementDL;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetChapterTest(ChapterTestRequestDTO chapterTestRequestDTO)
    {
        var result = await _testManagementDL
            .SetChapterTest(chapterTestRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetChapterTestQuestion(ChapterTestQuestionDTO chapterTestQuestionDTO)
    {
        var result = await _testManagementDL
            .SetChapterTestQuestion(chapterTestQuestionDTO)
            .ConfigureAwait(false);
        return result;
    }
}
