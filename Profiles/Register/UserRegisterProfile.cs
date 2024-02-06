using AutoMapper;

namespace CollegeAppDotnetWebApi;

public class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<RegisterRequestDTO, TejiloUser>()
            .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Email));

        CreateMap<CourseModel, CourseResponseDTO>();
    }
}
