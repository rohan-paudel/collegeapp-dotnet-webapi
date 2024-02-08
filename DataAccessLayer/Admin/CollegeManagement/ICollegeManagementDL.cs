using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CollegeAppDotnetWebApi;

public interface ICollegeManagementDL
{
    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetCollege(
        CollegeRequestDTO collegeRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleCollegeStatus(CollegeStatusToggleRequestDTO collegeStatusToggleRequestDTO);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteCollege(
        CollegeDeleteDTO collegeDeleteDTO
    );

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> EditCollege(
        EditCollegeRequestDTO editCollegeRequestDTO
    );

    public Task<
        Results<Ok<ResponseDTO<IEnumerable<CollegeResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetCollege(string? searchTerm, bool? collegeStatus);
}
