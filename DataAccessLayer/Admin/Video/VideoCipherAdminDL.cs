using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public class VideoCipherAdminDL : IVideoCipherAdminDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    private readonly HttpClient _httpClient;

    private string GetDescriptionUrl = "https://dev.vdocipher.com/api/videos/";

    public VideoCipherAdminDL(
        AppDataContext dataContext,
        IMapper mapper,
        IHttpClientFactory httpClientFactory
    )
    {
        _httpClient = httpClientFactory.CreateClient("VdoCipherClient");
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetVideo(
        UploadVideoRequestDTO uploadVideoRequestDTO
    )
    {
        try
        {
            var response = await _httpClient
                .GetAsync(GetDescriptionUrl + uploadVideoRequestDTO.VdoCipherId)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response
                    .Content
                    .ReadFromJsonAsync<VdoCipherDescriptionResponseDTO>()
                    .ConfigureAwait(false);

                if (responseData != null)
                {
                    uploadVideoRequestDTO.Description = responseData.Description;
                    uploadVideoRequestDTO.TotalVideoDuration = responseData.Length;
                }
                else
                {
                    return TypedResults.BadRequest<ResponseDTO<string>>(
                        new()
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "Something went wrong."
                        }
                    );
                }
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong."
                    }
                );
            }
            await _dataContext
                .VideoModel
                .AddAsync(_mapper.Map<VideoModel>(uploadVideoRequestDTO))
                .ConfigureAwait(false);
            int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(false);

            if (rowsAffected > 0)
            {
                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong."
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
                    Message = "Something wend wrong."
                }
            );
        }
    }
}
