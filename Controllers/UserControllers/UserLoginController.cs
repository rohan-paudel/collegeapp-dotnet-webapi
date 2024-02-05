using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class UserLoginController : ControllerBase
{
    private readonly IUserLoginDL _userLoginDL;

    public UserLoginController(IUserLoginDL userLoginDL)
    {
        _userLoginDL = userLoginDL;
    }

    [HttpPost]
    [ProducesResponseType(
        StatusCodes.Status400BadRequest,
        Type = typeof(ResponseDTO<LoginResponseDTO>)
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDTO<LoginResponseDTO>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<
        Results<
            Ok<ResponseDTO<AccessTokenResponse>>,
            EmptyHttpResult,
            BadRequest<ResponseDTO<LoginResponseDTO>>
        >
    > Login(LoginRequestDTO loginRequestDTO)
    {
        Results<
            Ok<ResponseDTO<AccessTokenResponse>>,
            EmptyHttpResult,
            BadRequest<ResponseDTO<LoginResponseDTO>>
        > responseDTO;

        if (ModelState.IsValid)
        {
            responseDTO = await _userLoginDL.Login(loginRequestDTO).ConfigureAwait(true);
            return (responseDTO);
        }
        else
        {
            ResponseDTO<LoginResponseDTO> errorResponseDTO =
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something went wrong"
                };
            return TypedResults.BadRequest(errorResponseDTO);
        }
    }
}
