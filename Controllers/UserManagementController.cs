using AutoMapper;
using CollegeAppDotnetWebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MySqlConnector;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class UserManagementController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly UserManager<TejiloUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public UserManagementController(
        ILogger<WeatherForecastController> logger,
        UserManager<TejiloUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper
    )
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RegisterResponseDTO))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponseDTO))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<RegisterResponseDTO>> RegisterUser(
        RegisterRequestDTO registerRequestDTO
    )
    {
        RegisterResponseDTO successRegisterResponseDTO = new();

        RegisterResponseDTO errorRegisterResponseDTO =
            new() { StatusCode = StatusCodes.Status400BadRequest, Message = "Error Occured." };

        if (ModelState.IsValid)
        {
            TejiloUser tejiloUser = _mapper.Map<TejiloUser>(registerRequestDTO);

            try
            {
                var isCreated = await _userManager
                    .CreateAsync(tejiloUser, registerRequestDTO.Password)
                    .ConfigureAwait(false);
                if (isCreated.Succeeded)
                {
                    return Ok(successRegisterResponseDTO);
                }
                else
                {
                    errorRegisterResponseDTO.Errors = isCreated.Errors;
                    return BadRequest(errorRegisterResponseDTO);
                }
            }
            catch (DbUpdateException e)
            {
                if (
                    (e.InnerException?.Message.Contains("Duplicate")) == true
                    && (
                        e.InnerException?.Message.Contains("AspNetUsers.IX_AspNetUsers_PhoneNumber")
                    ) == true
                )
                {
                    errorRegisterResponseDTO.Errors = new List<IdentityError>()
                    {
                        new()
                        {
                            Code = "Duplicate Phone Number",
                            Description = "Phone Number entered is already in use"
                        }
                    };
                    return BadRequest(errorRegisterResponseDTO);
                }
                else
                {
                    errorRegisterResponseDTO.Errors = new List<IdentityError>()
                    {
                        new()
                        {
                            Code = e.InnerException?.Message ?? "Error Occured",
                            Description = e.InnerException?.Message ?? "Error Occured"
                        }
                    };
                    return BadRequest(errorRegisterResponseDTO);
                }
            }
            catch (Exception ex)
            {
                errorRegisterResponseDTO.Errors = new List<IdentityError>()
                {
                    new()
                    {
                        Code = ex.InnerException?.Message ?? "Error Occured",
                        Description = ex.InnerException?.Message ?? "Error Occured"
                    }
                };
                return BadRequest(errorRegisterResponseDTO);
            }
        }
        else
        {
            return BadRequest(errorRegisterResponseDTO);
        }
    }
}
