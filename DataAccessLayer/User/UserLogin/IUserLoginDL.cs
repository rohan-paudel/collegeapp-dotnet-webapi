using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface IUserLoginDL
{
    public Task<
        Results<
            Ok<ResponseDTO<AccessTokenResponse>>,
            EmptyHttpResult,
            BadRequest<ResponseDTO<LoginResponseDTO>>
        >
    > Login(LoginRequestDTO loginRequestDTO);
}
