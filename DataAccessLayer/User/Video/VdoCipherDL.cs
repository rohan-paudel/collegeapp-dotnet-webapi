using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class VdoCipherDL : IVdoCipherDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public VdoCipherDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<GetOtpResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetOtpToPlayVideo(string videoPlayId, HttpClient httpClient)
    {
        try
        {
            var response = await httpClient
                .PostAsJsonAsync(
                    $"https://dev.vdocipher.com/api/videos/{videoPlayId}/otp",
                    new { ttl = 300 }
                )
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                // Read response content
                var responseData = await response
                    .Content
                    .ReadFromJsonAsync<GetOtpResponseDTO>()
                    .ConfigureAwait(false);
                ;

                return TypedResults.Ok<ResponseDTO<GetOtpResponseDTO>>(
                    new() { Data = responseData }
                );
            }
            else
            {
                // Handle unsuccessful response
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong"
                    }
                );
            }
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something went wrong"
                }
            );
        }
    }

    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<VideoResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetVideos(int topicId, bool? statusCode)
    {
        try
        {
            IQueryable<VideoModel> queryVideo = _dataContext.VideoModel;

            queryVideo = queryVideo.Where(x => x.TopicId == topicId);

            if (statusCode != null)
            {
                queryVideo = queryVideo.Where(x => x.Status == statusCode);
            }

            var videoModels = await queryVideo
                .ProjectTo<VideoResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<VideoResponseDTO>>>(
                new() { Data = videoModels }
            );
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something wend wrong."
                }
            );
        }
    }
}
