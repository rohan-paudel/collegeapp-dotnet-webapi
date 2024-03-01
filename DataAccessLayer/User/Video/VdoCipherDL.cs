using System.Text.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

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
    > GetOtpDownloadVideo(string videoPlayId, HttpClient httpClient)
    {
        try
        {
            var response = await httpClient
                .PostAsJsonAsync(
                    $"https://dev.vdocipher.com/api/videos/{videoPlayId}/otp",
                    new
                    {
                        // 5 minutes until OTP is used
                        // only used when setting up offline, not again
                        ttl = 300,
                        licenseRules = JsonSerializer.Serialize(
                            new
                            {
                                rentalDuration = 15 * 24 * 3600, // 15 days
                                canPersist = true,
                            }
                        ),
                    }
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
    > GetUVideos(int topicId)
    {
        try
        {
            var videoModels = await _dataContext
                .VideoModel
                .Where(x => x.TopicId == topicId && x.Status == true)
                .Select(
                    x =>
                        new VideoResponseDTO
                        {
                            Id = x.Id,
                            Status = x.Status,
                            Title = x.Title,
                            Description = x.Description,
                            VdoCipherId = x.VdoCipherId,
                            TotalVideoDuration = x.TotalVideoDuration
                        }
                )
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
