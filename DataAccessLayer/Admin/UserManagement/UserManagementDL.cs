using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class UserManagementDL : IUserManagementDL
{
    private readonly UserManager<TejiloUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public UserManagementDL(
        UserManager<TejiloUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<RegisterResponseDTO> RegisterUser(RegisterRequestDTO registerRequestDTO)
    {
        RegisterResponseDTO successRegisterResponseDTO = new();

        RegisterResponseDTO errorRegisterResponseDTO =
            new() { StatusCode = StatusCodes.Status400BadRequest, Message = "Error Occured." };

        TejiloUser tejiloUser = _mapper.Map<TejiloUser>(registerRequestDTO);

        try
        {
            var isCreated = await _userManager
                .CreateAsync(tejiloUser, registerRequestDTO.Password)
                .ConfigureAwait(false);
            if (isCreated.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(tejiloUser.Email);

                if (user != null)
                {
                    var roleResult = await _userManager
                        .AddToRoleAsync(user, Roles.User)
                        .ConfigureAwait(false);

                    if (roleResult.Succeeded)
                    {
                        return successRegisterResponseDTO;
                    }
                    else
                    {
                        var deletedUser = await _userManager
                            .DeleteAsync(user)
                            .ConfigureAwait(false);
                        return errorRegisterResponseDTO;
                    }
                }
                else
                {
                    errorRegisterResponseDTO.Errors = isCreated.Errors;
                    return errorRegisterResponseDTO;
                }
            }
            else
            {
                errorRegisterResponseDTO.Errors = isCreated.Errors;
                return errorRegisterResponseDTO;
            }
        }
        catch (DbUpdateException e)
        {
            if (
                (e.InnerException?.Message.Contains("Duplicate")) == true
                && (e.InnerException?.Message.Contains("AspNetUsers.IX_AspNetUsers_PhoneNumber"))
                    == true
            )
            {
                errorRegisterResponseDTO.Errors = new List<IdentityError>()
                {
                    new()
                    {
                        Code = "Duplicate Phone Number",
                        Description = "Phone Number entered is already in use"
                    }
                };
                return errorRegisterResponseDTO;
            }
            else
            {
                errorRegisterResponseDTO.Errors = new List<IdentityError>()
                {
                    new()
                    {
                        Code = e.InnerException?.Message ?? "Error Occured",
                        Description = e.InnerException?.Message ?? "Error Occured"
                    }
                };
                return errorRegisterResponseDTO;
            }
        }
        catch (Exception ex)
        {
            errorRegisterResponseDTO.Errors = new List<IdentityError>()
            {
                new()
                {
                    Code = ex.InnerException?.Message ?? "Error Occured",
                    Description = ex.InnerException?.Message ?? "Error Occured"
                }
            };
            return errorRegisterResponseDTO;
        }
    }
}
