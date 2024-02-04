using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CollegeAppDotnetWebApi;

public class JwtTokenService
{
    // private readonly string _signingKey =
    //     "this-is-server-jwt-key-for-encryption-12344-#$@%%#@-the_hello_98889))&^^&"; // Replace with a secure key

    // public async Task<string> GenerateJwtToken(TejiloUser user, UserManager<TejiloUser> userManager)
    // {
    //     var claims = new List<Claim>
    //     {
    //         new Claim(ClaimTypes.Name, user.UserName),
    //         new Claim(ClaimTypes.Email, user.Email),
    //         new Claim(ClaimTypes.UserData, user.FullName)
    //     };

    //     // Include user roles as claims
    //     var roles = await userManager.GetRolesAsync(user).ConfigureAwait(true);
    //     foreach (var role in roles)
    //     {
    //         claims.Add(new Claim(ClaimTypes.Role, role));
    //     }

    //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
    //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //     var token = new JwtSecurityToken(
    //         issuer: "https://tejilo.com.np", // Replace with your token issuer
    //         audience: "https://tejilo.com.np", // Replace with your token audience
    //         claims: claims,
    //         expires: DateTime.Now.AddMinutes(30), // Token expiration time
    //         signingCredentials: creds
    //     );

    //     return new JwtSecurityTokenHandler().WriteToken(token);
    // }
}
