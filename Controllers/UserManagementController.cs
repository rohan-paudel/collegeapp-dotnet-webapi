using AutoMapper;
using CollegeAppDotnetWebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<RegisterResponseDTO>> RegisterUser(
        RegisterRequestDTO registerRequestDTO
    )
    {
        RegisterResponseDTO registerResponseDTO = new();
        if (ModelState.IsValid)
        {
            TejiloUser tejiloUser = _mapper.Map<TejiloUser>(registerRequestDTO);

            try
            {
                var isCreated = await _userManager.CreateAsync(
                    tejiloUser,
                    registerRequestDTO.Password
                );
                if (isCreated.Succeeded)
                {
                    return Ok(registerResponseDTO);
                }
                else
                {
                    registerResponseDTO.StatusCode = StatusCodes.Status400BadRequest;
                    registerResponseDTO.Message = "Error Occured.";
                    registerResponseDTO.Errors = isCreated.Errors;
                    return BadRequest(registerResponseDTO);
                }
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }
        else
        {
            registerResponseDTO.StatusCode = StatusCodes.Status400BadRequest;
            registerResponseDTO.Message = "Something went wring with your request.";
            registerResponseDTO.Data = null;

            return BadRequest(registerResponseDTO);
        }
    }
}
