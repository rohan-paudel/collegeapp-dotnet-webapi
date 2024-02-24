using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface INoticeBoardManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetNoticeBoard(
        NoticeBoardRequestDTO noticeBoardRequestDTO
    );
}
