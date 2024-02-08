using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public class CollegeManagementDL : ICollegeManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteCollege(
        CollegeDeleteDTO collegeDeleteDTO
    )
    {
        throw new NotImplementedException();
    }

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> EditCollege(
        CollegeRequestDTO collegeRequestDTO
    )
    {
        throw new NotImplementedException();
    }

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetCollege(
        CollegeRequestDTO collegeRequestDTO
    )
    {
        throw new NotImplementedException();
    }

    public Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleCollegeStatus(CollegeStatusToggleRequestDTO collegeStatusToggleRequestDTO)
    {
        throw new NotImplementedException();
    }
}
