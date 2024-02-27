using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class VdoCipherController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IVdoCipherDL _vdoCipherDL;

    public VdoCipherController(IHttpClientFactory httpClientFactory, IVdoCipherDL vdoCipherDL)
    {
        _httpClient = httpClientFactory.CreateClient("VdoCipherClient");
        _vdoCipherDL = vdoCipherDL;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<GetOtpResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetOtpToPlayVideo([FromQuery] string videoPlayId)
    {
        var result = await _vdoCipherDL
            .GetOtpToPlayVideo(videoPlayId, _httpClient)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<VideoResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetVideos(int topicId, bool? statusCode)
    {
        var result = await _vdoCipherDL.GetVideos(topicId, statusCode).ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<VideoResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetUVideos(int topicId)
    {
        var result = await _vdoCipherDL.GetUVideos(topicId).ConfigureAwait(false);
        return result;
    }
}
