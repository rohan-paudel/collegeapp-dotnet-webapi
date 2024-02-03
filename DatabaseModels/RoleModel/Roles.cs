using Microsoft.AspNetCore.Identity;

namespace CollegeAppDotnetWebApi;

public static class Roles
{
    public const string User = "User";
    public const string CollegeAdmin = "CollegeAdmin";
    public const string TejiloSuperAdmin = "TejiloSuperAdmin";
    public const string TejiloAdmin = "TejiloAdmin";
    public const string TejiloSubAdmin = "TejiloSubAdmin";
    public const string TejiloCustomerCare = "TejiloCustomerCare";
    public const string TejiloMarketing = "TejiloMarketing";

    public static List<IdentityRole> identityRoles =
    [
        new IdentityRole(User),
        new IdentityRole(CollegeAdmin),
        new IdentityRole(TejiloSuperAdmin),
        new IdentityRole(TejiloAdmin),
        new IdentityRole(TejiloSubAdmin),
        new IdentityRole(TejiloCustomerCare),
        new IdentityRole(TejiloMarketing)
    ];
}
