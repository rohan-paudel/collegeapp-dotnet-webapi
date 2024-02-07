using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface ICategoryManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetCourse(
        CourseRequestDTO courseRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleCourseStatus(CourseStatusToggleRequestDTO courseIdRequestDTO);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> EditCourseName(
        CourseRequestEditNameDTO courseRequestEditNameDTO
    );

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteCourse(
        CourseDeleteDTO courseDeleteDTO
    );

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<CourseResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetCourses(string? courseName, bool? courseStatus);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetSubCourse(
        SubCourseRequestDTO subCourseRequestDTO
    );
}
