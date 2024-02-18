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
            )
            .ForMember(
                dest => dest.RemainingDays,
                opt =>
                    opt.MapFrom(
                        src => (int)Math.Ceiling((src.ValidTill - DateTime.UtcNow).TotalDays)
                    )
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

        CreateMap<TopicModel, TopicResponseOnlyNameDTO>();

        CreateMap<TejiloUser, EditStudentRequestDTO>();

        CreateMap<TejiloUser, StudentResponseOnlyNameDTO>();

        CreateMap<EditStudentRequestDTO, TejiloUser>()
            .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Email));

        CreateMap<TejiloUser, RegisterStudentResponseDTO>();

        CreateMap<NoteRequestDTO, NoteModel>()
            .ForMember(dest => dest.FileName, src => src.MapFrom(x => x.FileNameByDeveloper));

        CreateMap<NoteModel, NoteResponseDTO>();

        // Discussion Models

        CreateMap<DiscussionRequestDTO, DiscussionModel>();
        CreateMap<DiscussionModel, DiscussionResponseDTO>()
            .ForMember(dest => dest.Student, src => src.MapFrom(x => x.TejiloUser));

        // For Query Models

        CreateMap<QueryRequestDTO, QueryModel>();
        CreateMap<QueryModel, QueryResponseDTO>()
            .ForMember(dest => dest.Student, src => src.MapFrom(x => x.TejiloUser));

        // For Chapter Test
        CreateMap<ChapterTestRequestDTO, ChapterTestModel>();
        CreateMap<ChapterTestQuestionDTO, ChapterTestQuestionModel>();
        CreateMap<ChapterTestOptionDTO, ChapterTestOptionModel>();
    }
}
