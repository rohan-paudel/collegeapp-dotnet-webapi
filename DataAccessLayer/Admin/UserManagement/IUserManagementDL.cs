using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

public interface IUserManagementDL
{
    public Task<RegisterResponseDTO> RegisterUser(RegisterRequestDTO registerRequestDTO);
}
