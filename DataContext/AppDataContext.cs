using Microsoft.AspNetCore.Identity;
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
}
