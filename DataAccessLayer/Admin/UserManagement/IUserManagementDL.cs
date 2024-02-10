using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

public interface IUserManagementDL
{
    public Task<RegisterResponseDTO> RegisterUser(RegisterRequestDTO registerRequestDTO);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> RegisterStudent(
        RegisterRequestDTO registerRequestDTO
    );

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> EditStudent(
        EditStudentRequestDTO editStudentRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > UpdateStudentCourseSubCourse(UpdateStudentCourseSubCourse updateStudentCourseSubCourse);

    public Task<
        Results<
            Ok<ResponseDTO<IEnumerable<RegisterStudentResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetStudent(string? searchTerm, bool? studentStatus);

    public Task<
        Results<
            Ok<ResponseDTO<IEnumerable<RegisterStudentResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetStudentByCollegeId(string collegeId, bool? studentStatus);

    public Task<
        Results<Ok<ResponseDTO<RegisterStudentResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetStudentByStudentId(string studentId, bool? studentStatus);
}
