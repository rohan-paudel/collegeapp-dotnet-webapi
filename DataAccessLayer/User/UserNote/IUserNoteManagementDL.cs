using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface IUserNoteManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetUserNote(
        UserNoteRequestDTO userNoteRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<UserNoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetUserNotes(int topicId, string studentId, bool? noteStatus);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> EditUserNote(
        UserNoteUpdateDTO userNoteUpdateDTO
    );
}
