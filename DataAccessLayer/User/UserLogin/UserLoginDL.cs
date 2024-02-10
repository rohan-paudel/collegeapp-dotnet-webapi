using System.Security.Claims;
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

            var user = await _userManager
                .FindByEmailAsync(loginRequestDTO.Email)
                .ConfigureAwait(false);
            if (user == null)
            {
                // User not found
                errorResponseDTO.Message = "Please check email or password";
                return (TypedResults.BadRequest(errorResponseDTO));
            }

            // var result = await _signInManager
            //     .PasswordSignInAsync(user, loginRequestDTO.Password, true, true)
            //     .ConfigureAwait(false);

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                loginRequestDTO.Password,
                false
            );
            if (!result.Succeeded)
            {
                // Failed login attempt
                errorResponseDTO.Message = "Please check email or password";
                return (TypedResults.BadRequest(errorResponseDTO));
            }

            var claims = new List<Claim>
            {
                new Claim("CollegeId", user.CollegeId.ToString()),
                new Claim("CourseId", user.CourseId != null ? user.CourseId.ToString() : ""),
                new Claim(
                    "SubCourseId",
                    user.SubCourseId != null ? user.SubCourseId.ToString() : ""
                ),
                // Add more custom claims as needed
            };
            // var result = await _signInManager
            //     .PasswordSignInAsync(loginRequestDTO.Email, loginRequestDTO.Password, true, true)
            //     .ConfigureAwait(false);

            await _signInManager.SignInWithClaimsAsync(user, true, claims);

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

            return TypedResults.Empty;
        }
        catch (Exception)
        {
            errorResponseDTO.Errors = [new() { Code = "Exception Occured", Description = "" }];
            return TypedResults.BadRequest(errorResponseDTO);
        }
    }
}

public interface IUserClaimsPrincipalFactory { }
