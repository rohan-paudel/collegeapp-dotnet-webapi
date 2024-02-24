using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class CollgeManagementController : ControllerBase
{
    private readonly ICollegeManagementDL _collegeManagementDL;

    public CollgeManagementController(ICollegeManagementDL collegeManagementDL)
    {
        _collegeManagementDL = collegeManagementDL;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetCollege(
        CollegeRequestDTO collegeRequestDTO
    )
    {
        var result = await _collegeManagementDL.SetCollege(collegeRequestDTO).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditCollege(EditCollegeRequestDTO editCollegeRequestDTO)
    {
        var result = await _collegeManagementDL
            .EditCollege(editCollegeRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleCollegeStatus(CollegeStatusToggleRequestDTO collegeStatusToggleRequestDTO)
    {
        var result = await _collegeManagementDL
            .ToggleCollegeStatus(collegeStatusToggleRequestDTO)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<CollegeResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetCollege([FromQuery] string? searchTerm, [FromQuery] bool? collegeStatus)
    {
        var result = await _collegeManagementDL
            .GetCollege(searchTerm, collegeStatus)
            .ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<CollegeResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetCollegeById([FromHeader(Name = "sim")] string collegeId)
    {
        var result = await _collegeManagementDL.GetCollegeById(collegeId).ConfigureAwait(false);
        return result;
    }
}
