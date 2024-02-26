using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface INoteManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetNote(
        NoteRequestDTO noteRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<NoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetNotes(int? topicId, string? noteName, int page, bool? noteStatus);

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<NoteUResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetUNotes(int topicId);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteNote(
        DeleteNoteRequestDTO deleteNoteRequestDTO
    );

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> ToggleNoteStatus(
        NoteStatusToggleRequestDTO noteStatusToggleRequestDTO
    );
}
