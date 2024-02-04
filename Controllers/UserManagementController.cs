using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RegisterResponseDTO))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponseDTO))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<RegisterResponseDTO>> RegisterUser(
        RegisterRequestDTO registerRequestDTO
    )
    {
        RegisterResponseDTO registerResponseDTO;

        if (ModelState.IsValid)
        {
            registerResponseDTO = await _userManagementDL
                .RegisterUser(registerRequestDTO)
                .ConfigureAwait(false);
            if (registerResponseDTO.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(registerResponseDTO);
            }
            else
            {
                return BadRequest(registerResponseDTO);
            }
        }
        else
        {
            registerResponseDTO = new()
            {
                StatusCode = 400,
                Errors = [new IdentityError() { Code = "Bad Request", Description = "Bad Request" }]
            };
            return BadRequest(registerRequestDTO);
        }
    }
}
