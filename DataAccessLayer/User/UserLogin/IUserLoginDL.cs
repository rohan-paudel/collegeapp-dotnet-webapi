namespace CollegeAppDotnetWebApi;

public interface IUserLoginDL
{
    public Task<ResponseDTO<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO);
}
