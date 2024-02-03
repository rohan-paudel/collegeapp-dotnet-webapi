using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class AppDataContext : IdentityDbContext<TejiloUser>
{
    public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options) { }
}
