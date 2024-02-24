using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface IVideoCipherAdminDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetVideo(
        UploadVideoRequestDTO uploadVideoRequestDTO
    );
}
