using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface IVdoCipherDL
{
    public Task<
        Results<Ok<ResponseDTO<GetOtpResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetOtpToPlayVideo(string videoPlayId, HttpClient httpClient);

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<VideoResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetVideos(int topicId, bool? statusCode);
}
