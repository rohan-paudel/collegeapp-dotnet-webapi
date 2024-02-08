using AutoMapper;

namespace CollegeAppDotnetWebApi;

public class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<CollegeRequestDTO, TejiloCollege>();

        CreateMap<EditCollegeRequestDTO, TejiloCollege>();

        CreateMap<TejiloCollege, EditCollegeRequestDTO>();

        CreateMap<RegisterRequestDTO, TejiloUser>()
            .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Email));

        CreateMap<CourseModel, CourseResponseDTO>();

        CreateMap<CourseModel, CourseResponseOnlyNameDTO>();

        CreateMap<SubCourseModel, SubCourseResponseDTO>();

        CreateMap<SubCourseModel, SubCourseResponseOnlyNameDTO>();

        CreateMap<SubjectModel, SubjectResponseDTO>();

        CreateMap<SubjectModel, SubjectResponseOnlyNameDTO>();

        CreateMap<TopicModel, TopicResponseDTO>();
    }
}
