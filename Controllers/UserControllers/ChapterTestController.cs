using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("/api/[controller]/[Action]")]
public class ChapterTestController : ControllerBase
{
    private readonly IChapterTestDL _chapterTestDL;

    public ChapterTestController(IChapterTestDL chapterTestDL)
    {
        _chapterTestDL = chapterTestDL;
    }

    [HttpPost]
    [Authorize(Policy = "PolicyForMobileDevice")]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > PostChapterTest(PostChapterTestDTO postChapterTestDTO)
    {
        postChapterTestDTO.StudentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _chapterTestDL.PostChapterTest(postChapterTestDTO).ConfigureAwait(false);
        return result;
    }
}
