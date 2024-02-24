using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class VideoCipherAdminController : ControllerBase
{
    private readonly IVideoCipherAdminDL _videoCipherAdmin;

    public VideoCipherAdminController(IVideoCipherAdminDL videoCipherAdmin)
    {
        _videoCipherAdmin = videoCipherAdmin;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetVideo(
        [FromBody] UploadVideoRequestDTO uploadVideoRequestDTO
    )
    {
        var result = await _videoCipherAdmin.SetVideo(uploadVideoRequestDTO).ConfigureAwait(false);
        return result;
    }
}
