using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class CategoryManagementController : ControllerBase
{
    private readonly ICategoryManagementDL _categoryManagementDL;

    public CategoryManagementController(ICategoryManagementDL categoryManagementDL)
    {
        _categoryManagementDL = categoryManagementDL;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetCourse(
        CourseRequestDTO courseRequestDTO
    )
    {
        var result = await _categoryManagementDL.SetCourse(courseRequestDTO).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleCourseStatus(CourseStatusToggleRequestDTO courseStatusToggleRequestDTO)
    {
        var result = await _categoryManagementDL
            .ToggleCourseStatus(courseStatusToggleRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditCourseName(CourseRequestEditNameDTO courseRequestEditNameDTO)
    {
        var result = await _categoryManagementDL
            .EditCourseName(courseRequestEditNameDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > DeleteCourse(CourseDeleteDTO courseDeleteDTO)
    {
        var result = await _categoryManagementDL
            .DeleteCourse(courseDeleteDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<CourseResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetCourses([FromQuery] string? courseName, [FromQuery] bool? courseStatus)
    {
        var result = await _categoryManagementDL.GetCourses(courseName, courseStatus);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetSubCourse(SubCourseRequestDTO subCourseRequestDTO)
    {
        var result = await _categoryManagementDL
            .SetSubCourse(subCourseRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleSubCourseStatus(SubCourseStatusToggleRequestDTO subCourseStatusToggleRequestDTO)
    {
        var result = await _categoryManagementDL
            .ToggleSubCourseStatus(subCourseStatusToggleRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditSubCourseName(SubCourseRequestEditNameDTO subCourseRequestEditNameDTO)
    {
        var result = await _categoryManagementDL
            .EditSubCourseName(subCourseRequestEditNameDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > DeleteSubCourse(SubCourseDeleteDTO subCourseDeleteDTO)
    {
        var result = await _categoryManagementDL
            .DeleteSubCourse(subCourseDeleteDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<SubCourseResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetSubCourses(
        [FromQuery] int? courseId,
        [FromQuery] string? subCourseName,
        [FromQuery] bool? subCourseStatus
    )
    {
        var result = await _categoryManagementDL.GetSubCourses(
            courseId,
            subCourseName,
            subCourseStatus
        );
        return result;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> AddSubject(
        SubCourseSubjectSetDTO subCourseSubjectSetDTO
    )
    {
        var result = await _categoryManagementDL
            .AddSubject(subCourseSubjectSetDTO)
            .ConfigureAwait(false);
        return result;
    }
}
