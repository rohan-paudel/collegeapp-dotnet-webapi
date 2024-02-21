using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("/userapi/[controller]/[Action]")]
public class ChapterTestController : ControllerBase
{
    private readonly IChapterTestDL _chapterTestDL;

    public ChapterTestController(IChapterTestDL chapterTestDL)
    {
        _chapterTestDL = chapterTestDL;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > PostChapterTest(PostChapterTestDTO postChapterTestDTO)
    {
        postChapterTestDTO.StudentId = "d6061da4-a2f8-4ea7-90b8-7e4e95245fa6";
        var result = await _chapterTestDL.PostChapterTest(postChapterTestDTO).ConfigureAwait(false);
        return result;
    }
}
