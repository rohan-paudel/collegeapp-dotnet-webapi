using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/admin/[controller]/[Action]")]
public class UserManagementController : ControllerBase
{
    private readonly ILogger<UserManagementController> _logger;
    private readonly IUserManagementDL _userManagementDL;

    public UserManagementController(
        ILogger<UserManagementController> logger,
        IUserManagementDL userManagementDL
    )
    {
        _logger = logger;
        _userManagementDL = userManagementDL;
    }

    // [HttpPost]
    // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RegisterResponseDTO))]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponseDTO))]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    // public async Task<ActionResult<RegisterResponseDTO>> RegisterUser(
    //     RegisterRequestDTO registerRequestDTO
    // )
    // {
    //     RegisterResponseDTO registerResponseDTO;

    //     if (ModelState.IsValid)
    //     {
    //         registerResponseDTO = await _userManagementDL
    //             .RegisterUser(registerRequestDTO)
    //             .ConfigureAwait(false);
    //         if (registerResponseDTO.StatusCode == StatusCodes.Status200OK)
    //         {
    //             return Ok(registerResponseDTO);
    //         }
    //         else
    //         {
    //             return BadRequest(registerResponseDTO);
    //         }
    //     }
    //     else
    //     {
    //         registerResponseDTO = new()
    //         {
    //             StatusCode = 400,
    //             Errors = [new IdentityError() { Code = "Bad Request", Description = "Bad Request" }]
    //         };
    //         return BadRequest(registerRequestDTO);
    //     }
    // }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > RegisterStudent(RegisterRequestDTO registerRequestDTO)
    {
        var result = await _userManagementDL
            .RegisterStudent(registerRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditStudent(EditStudentRequestDTO editStudentRequestDTO)
    {
        var result = await _userManagementDL
            .EditStudent(editStudentRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<
            Ok<ResponseDTO<IEnumerable<RegisterStudentResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetStudents([FromQuery] string? searchTerm, [FromQuery] bool? studentStatus)
    {
        var result = await _userManagementDL
            .GetStudent(searchTerm, studentStatus)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [Authorize(Policy = "PolicyForMobileDevice")]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > UpdateStudentCourseSubCourse(UpdateStudentCourseSubCourse updateStudentCourseSubCourse)
    {
        updateStudentCourseSubCourse.StudentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _userManagementDL
            .UpdateStudentCourseSubCourse(updateStudentCourseSubCourse)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<
            Ok<ResponseDTO<IEnumerable<RegisterStudentResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetStudentsByCollegeId([FromQuery] [Required] int collegeId, bool? studentStatus)
    {
        var result = await _userManagementDL
            .GetStudentByCollegeId(collegeId, studentStatus)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<RegisterStudentResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetStudentByStudentId([FromQuery] [Required] string studentId, bool? studentStatus)
    {
        var result = await _userManagementDL
            .GetStudentByStudentId(studentId, studentStatus)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    [Authorize(Policy = "PolicyForMobileDevice")]
    public async Task<
        Results<Ok<ResponseDTO<RegisterStudentResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetStudentByToken()
    {
        var result = await _userManagementDL
            .GetStudentByStudentId(User.FindFirstValue(ClaimTypes.NameIdentifier)!, null)
            .ConfigureAwait(false);
        return result;
    }
}
