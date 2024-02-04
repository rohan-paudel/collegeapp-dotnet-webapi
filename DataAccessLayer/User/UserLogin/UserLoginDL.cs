using Microsoft.AspNetCore.Identity;

namespace CollegeAppDotnetWebApi;

public class UserLoginDL : IUserLoginDL
{
    private readonly SignInManager<TejiloUser> _signInManager;
    private readonly UserManager<TejiloUser> _userManager;

    public UserLoginDL(SignInManager<TejiloUser> signInManager, UserManager<TejiloUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<ResponseDTO<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
    {
        ResponseDTO<LoginResponseDTO> successfullResponseDTO = new() { };
        ResponseDTO<LoginResponseDTO> errorResponseDTO =
            new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Something went wrong"
            };
        try
        {
            var result = await _signInManager
                .PasswordSignInAsync(loginRequestDTO.Email, loginRequestDTO.Password, true, true)
                .ConfigureAwait(true);

            if (result.Succeeded)
            {
                // TejiloUser? user = await _userManager
                //     .FindByEmailAsync(loginRequestDTO.Email)
                //     .ConfigureAwait(true);
                // string Token = await new JwtTokenService().GenerateJwtToken(user, _userManager);
                // successfullResponseDTO.Data = new() { Token = Token };
                return successfullResponseDTO;
            }
            else if (result.IsLockedOut)
            {
                errorResponseDTO.Message = "Please retry login after sometime.";
                return errorResponseDTO;
            }
            else
            {
                errorResponseDTO.Message = "Please check email or password.";
                return errorResponseDTO;
            }
        }
        catch (Exception ex)
        {
            errorResponseDTO.Errors =
            [
                new() { Code = "Exception Occured", Description = ex.InnerException?.Message ?? "" }
            ];
            return errorResponseDTO;
        }
    }
}
