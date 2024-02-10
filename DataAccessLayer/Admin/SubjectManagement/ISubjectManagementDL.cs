using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface ISubjectManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetSubject(
        SubjectRequestDTO subjectRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleSubjectStatus(SubjectStatusToggleRequestDTO subjectStatusToggleRequestDTO);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> EditSubjectName(
        SubjectRequestEditNameDTO subjectRequestEditNameDTO
    );

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteSubject(
        SubjectDeleteDTO subjectDeleteDTO
    );

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<SubjectResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetSubjects(int? courseId, int? subcourseId, string? subjectName, bool? subjectStatus);
}
