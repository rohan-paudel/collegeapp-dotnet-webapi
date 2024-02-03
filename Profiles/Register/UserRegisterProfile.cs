using AutoMapper;

namespace CollegeAppDotnetWebApi;

public class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<RegisterRequestDTO, TejiloUser>()
            .ForMember(
                dest => dest.UserName,
                src =>
                    src.MapFrom(
                        x =>
                            x.FullName.Replace(" ", new Random().Next(1000, 9999).ToString())
                            + new Random().Next(1000, 9999)
                    )
            );
    }
}
