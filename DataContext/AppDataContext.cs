using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class AppDataContext : IdentityDbContext<TejiloUser>
{
    public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options) { }

    public DbSet<TejiloUser> TejiloUsers { get; set; }
    public DbSet<TejiloCollege> TejiloCollege { get; set; }
    public DbSet<CourseModel> CourseModel { get; set; }
    public DbSet<SubCourseModel> SubCourseModel { get; set; }

    public DbSet<SubjectModel> SubjectModel { get; set; }

    public DbSet<TopicModel> TopicModel { get; set; }

    public DbSet<NoteModel> NoteModel { get; set; }

    public DbSet<DiscussionModel> DiscussionModel { get; set; }

    public DbSet<QueryModel> QueryModel { get; set; }

    public DbSet<ChapterTestModel> ChapterTestModel { get; set; }

    public DbSet<ChapterTestQuestionModel> ChapterTestQuestionModel { get; set; }

    public DbSet<ChapterTestOptionModel> ChapterTestOptionModel { get; set; }

    public DbSet<LiveTestModel> LiveTestModel { get; set; }

    public DbSet<LiveTestQuestionModel> LiveTestQuestionModel { get; set; }

    public DbSet<LiveTestOptionModel> LiveTestOptionModel { get; set; }

    public DbSet<ChapterTestDetailedDataModel> ChapterTestDetailedDataModel { get; set; }

    public DbSet<ChapterTestUserDataModel> ChapterTestUserDataModel { get; set; }

    public DbSet<QuoteModel> QuoteModel { get; set; }

    public DbSet<NoticeBoardModel> NoticeBoardModel { get; set; }
    public DbSet<VideoModel> VideoModel { get; set; }

    public DbSet<UserNoteModel> UserNoteModel { get; set; }
}
