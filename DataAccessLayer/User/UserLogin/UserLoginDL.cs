using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public async Task<
        Results<
            Ok<ResponseDTO<AccessTokenResponse>>,
            EmptyHttpResult,
            BadRequest<ResponseDTO<LoginResponseDTO>>
        >
    > Login(LoginRequestDTO loginRequestDTO)
    {
        ResponseDTO<LoginResponseDTO> errorResponseDTO =
            new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Something went wrong"
            };

        try
        {
            _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

            var result = await _signInManager
                .PasswordSignInAsync(loginRequestDTO.Email, loginRequestDTO.Password, true, true)
                .ConfigureAwait(true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    errorResponseDTO.Message = "Please try later now.";
                    return (TypedResults.BadRequest(errorResponseDTO));
                }
                errorResponseDTO.Message = "Please check email or password";
                return (TypedResults.BadRequest(errorResponseDTO));
            }

            return TypedResults.BadRequest(errorResponseDTO);
        }
        catch (Exception)
        {
            errorResponseDTO.Errors = [new() { Code = "Exception Occured", Description = "" }];
            return TypedResults.BadRequest(errorResponseDTO);
        }
    }
}
