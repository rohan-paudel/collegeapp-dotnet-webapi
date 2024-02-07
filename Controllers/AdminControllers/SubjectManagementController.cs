using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class SubjectManagementController : ControllerBase
{
    private readonly ISubjectManagementDL _subjectManagementDL;

    public SubjectManagementController(ISubjectManagementDL subjectManagementDL)
    {
        _subjectManagementDL = subjectManagementDL;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetSubject(
        SubjectRequestDTO subjectRequestDTO
    )
    {
        var result = await _subjectManagementDL.SetSubject(subjectRequestDTO).ConfigureAwait(true);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleSubjectStatus(SubjectStatusToggleRequestDTO subjectStatusToggleRequestDTO)
    {
        var result = await _subjectManagementDL
            .ToggleSubjectStatus(subjectStatusToggleRequestDTO)
            .ConfigureAwait(true);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditSubjectName(SubjectRequestEditNameDTO subjectRequestEditNameDTO)
    {
        var result = await _subjectManagementDL
            .EditSubjectName(subjectRequestEditNameDTO)
            .ConfigureAwait(true);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > DeleteSubject(SubjectDeleteDTO subjectDeleteDTO)
    {
        var result = await _subjectManagementDL
            .DeleteSubject(subjectDeleteDTO)
            .ConfigureAwait(true);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<SubjectResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetSubjects(
        [FromQuery] string? courseId,
        [FromQuery] string? subcourseId,
        [FromQuery] string? subjectName,
        [FromQuery] bool? subjectStatus
    )
    {
        var result = await _subjectManagementDL
            .GetSubjects(courseId, subcourseId, subjectName, subjectStatus)
            .ConfigureAwait(true);
        return result;
    }
}
