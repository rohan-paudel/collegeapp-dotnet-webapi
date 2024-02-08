using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

public interface IUserManagementDL
{
    public Task<RegisterResponseDTO> RegisterUser(RegisterRequestDTO registerRequestDTO);

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> RegisterStudent(
        RegisterRequestDTO registerRequestDTO
    );
}
