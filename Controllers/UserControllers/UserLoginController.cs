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
    public async Task<ActionResult<ResponseDTO<LoginResponseDTO>>> Login(
        LoginRequestDTO loginRequestDTO
    )
    {
        ResponseDTO<LoginResponseDTO> responseDTO;

        if (ModelState.IsValid)
        {
            try
            {
                responseDTO = await _userLoginDL.Login(loginRequestDTO).ConfigureAwait(true);
                if (responseDTO.StatusCode == StatusCodes.Status200OK)
                {
                    return Ok(responseDTO);
                }
                else
                {
                    return BadRequest(responseDTO);
                }
            }
            catch (Exception ex)
            {
                responseDTO = new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something went wrong"
                };
                return BadRequest(responseDTO);
            }
        }
        else
        {
            responseDTO = new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Something went wrong"
            };
            return BadRequest(responseDTO);
        }
    }
}
