using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface INoticeBoardManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetNoticeBoard(
        NoticeBoardRequestDTO noticeBoardRequestDTO
    );

    public Task<
        Results<
            Ok<ResponseDTO<IEnumerable<NoticeBoardResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetNoticeBoard();
}
