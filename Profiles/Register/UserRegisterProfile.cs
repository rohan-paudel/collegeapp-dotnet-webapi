using AutoMapper;

namespace CollegeAppDotnetWebApi;

public class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<CollegeRequestDTO, TejiloCollege>();

        CreateMap<EditCollegeRequestDTO, TejiloCollege>();

        CreateMap<TejiloCollege, EditCollegeRequestDTO>();

        CreateMap<TejiloCollege, CollegeOnlyNameResponseDTO>();

        CreateMap<TejiloCollege, CollegeResponseDTO>()
            .ForMember(dest => dest.CollegeId, src => src.MapFrom(x => x.Id))
            .ForMember(
                dest => dest.StudentCount,
                src => src.MapFrom(x => x.Students != null ? x.Students.Count : 0)
            );

        CreateMap<RegisterRequestDTO, TejiloUser>()
            .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Email));
        CreateMap<CourseModel, CourseResponseDTO>();

        CreateMap<CourseModel, CourseResponseOnlyNameDTO>();

        CreateMap<SubCourseModel, SubCourseResponseDTO>();

        CreateMap<SubCourseModel, SubCourseResponseOnlyNameDTO>();

        CreateMap<SubjectModel, SubjectResponseDTO>();

        CreateMap<SubjectModel, SubjectResponseOnlyNameDTO>();

        CreateMap<TopicModel, TopicResponseDTO>();

        CreateMap<TejiloUser, EditStudentRequestDTO>();

        CreateMap<EditStudentRequestDTO, TejiloUser>()
            .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Email));

        CreateMap<TejiloUser, RegisterStudentResponseDTO>();
    }
}
