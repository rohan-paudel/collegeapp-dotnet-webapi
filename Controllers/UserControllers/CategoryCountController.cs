using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class CategoryCountController : ControllerBase
{
    private readonly ICategoryCountDL _categoryCountDL;

    public CategoryCountController(ICategoryCountDL categoryCountDL)
    {
        _categoryCountDL = categoryCountDL;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<CategoryCountResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetTopicsCategoryCount(int topicId)
    {
        var result = await _categoryCountDL.GetTopicsCategoryCount(topicId).ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > UpdateVideoCount(int topicId)
    {
        var result = await _categoryCountDL.UpdateVideoCount(topicId).ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > UpdateNoteCount(int topicId)
    {
        var result = await _categoryCountDL.UpdateNoteCount(topicId).ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > UpdateChapterTestCount(int topicId)
    {
        var result = await _categoryCountDL.UpdateChapterTestCount(topicId).ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > UpdateChapterTestQuestionsCount(int chapterTestId)
    {
        var result = await _categoryCountDL
            .UpdateChapterTestQuestionsCount(chapterTestId)
            .ConfigureAwait(false);
        return result;
    }
}
