using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class NoticeBoardManagementController : ControllerBase
{
    private readonly INoticeBoardManagementDL _noticeBoardManagementDL;

    public NoticeBoardManagementController(INoticeBoardManagementDL noticeBoardManagementDL)
    {
        _noticeBoardManagementDL = noticeBoardManagementDL;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetNoticeBoard(NoticeBoardRequestDTO noticeBoardRequestDTO)
    {
        var result = await _noticeBoardManagementDL
            .SetNoticeBoard(noticeBoardRequestDTO)
            .ConfigureAwait(false);
        return result;
    }
}
