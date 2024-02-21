using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class UserManagementDL : IUserManagementDL
{
    private readonly UserManager<TejiloUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public UserManagementDL(
        UserManager<TejiloUser> userManager,
        RoleManager<IdentityRole> roleManager,
        AppDataContext dataContext,
        IMapper mapper
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditStudent(EditStudentRequestDTO editStudentRequestDTO)
    {
        try
        {
            var user = await _userManager
                .FindByIdAsync(editStudentRequestDTO.StudentId)
                .ConfigureAwait(false);

            if (user == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No such student found"
                    }
                );
            }

            var rowsAffected = await _userManager
                .UpdateAsync(_mapper.Map(editStudentRequestDTO, user))
                .ConfigureAwait(false);

            if (rowsAffected.Succeeded)
            {
                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Someting went wrong",
                        Errors = rowsAffected.Errors
                    }
                );
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
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong.",
                        Errors = new List<IdentityError>()
                        {
                            new()
                            {
                                Code = "Duplicate Phone Number",
                                Description = "Phone Number entered is already in use"
                            }
                        }
                    }
                );
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong."
                    }
                );
            }
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something Went Wrong"
                }
            );
        }
    }

    public async Task<
        Results<
            Ok<ResponseDTO<IEnumerable<RegisterStudentResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetStudent(string? searchTerm, bool? studentStatus)
    {
        try
        {
            IQueryable<TejiloUser> queryCourse = _dataContext.TejiloUsers;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                queryCourse = queryCourse.Where(
                    x =>
                        x.FullName.ToLower().Contains(searchTerm.ToLower())
                        || x.Email!.ToLower().Contains(searchTerm.ToLower())
                        || x.PhoneNumber!.Contains(searchTerm.ToLower())
                );
            }

            if (studentStatus != null)
            {
                queryCourse = queryCourse.Where(x => x.Status == studentStatus);
            }

            var userModels = await queryCourse
                .Include(x => x.SubCourse)
                .ThenInclude(a => a!.Course)
                .Include(x => x.College)
                .ProjectTo<RegisterStudentResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<RegisterStudentResponseDTO>>>(
                new() { Data = userModels }
            );
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something wend wrong."
                }
            );
        }
    }

    public async Task<
        Results<
            Ok<ResponseDTO<IEnumerable<RegisterStudentResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetStudentByCollegeId(int collegeId, bool? studentStatus)
    {
        try
        {
            IQueryable<TejiloUser> queryCourse = _dataContext.TejiloUsers;

            queryCourse = queryCourse.Where(x => x.CollegeId == collegeId);

            if (studentStatus != null)
            {
                queryCourse = queryCourse.Where(x => x.Status == studentStatus);
            }

            var userModels = await queryCourse
                .Include(x => x.College)
                .Include(x => x.SubCourse)
                .ThenInclude(x => x!.Course)
                .Select(p => _mapper.Map<RegisterStudentResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<RegisterStudentResponseDTO>>>(
                new() { Data = userModels }
            );
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something wend wrong."
                }
            );
        }
    }

    public async Task<
        Results<Ok<ResponseDTO<RegisterStudentResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetStudentByStudentId(string studentId, bool? studentStatus)
    {
        try
        {
            IQueryable<TejiloUser> queryCourse = _dataContext.TejiloUsers;

            queryCourse = queryCourse.Where(x => x.Id == studentId);

            if (studentStatus != null)
            {
                queryCourse = queryCourse.Where(x => x.Status == studentStatus);
            }

            var userModels = await queryCourse
                .Include(x => x.College)
                .Include(x => x.SubCourse)
                .ThenInclude(x => x!.Course)
                .Select(p => _mapper.Map<RegisterStudentResponseDTO>(p))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<RegisterStudentResponseDTO>>(
                new() { Data = userModels }
            );
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something wend wrong."
                }
            );
        }
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > RegisterStudent(RegisterRequestDTO registerRequestDTO)
    {
        TejiloUser tejiloUser = _mapper.Map<TejiloUser>(registerRequestDTO);
        try
        {
            var isCreated = await _userManager
                .CreateAsync(tejiloUser, registerRequestDTO.Password)
                .ConfigureAwait(false);
            if (isCreated.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(tejiloUser.Email!);

                if (user != null)
                {
                    var roleResult = await _userManager
                        .AddToRoleAsync(user, Roles.User)
                        .ConfigureAwait(false);

                    if (roleResult.Succeeded)
                    {
                        return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
                    }
                    else
                    {
                        var deletedUser = await _userManager
                            .DeleteAsync(user)
                            .ConfigureAwait(false);
                        return TypedResults.BadRequest<ResponseDTO<string>>(
                            new()
                            {
                                StatusCode = StatusCodes.Status400BadRequest,
                                Message = "Something went wrong."
                            }
                        );
                    }
                }
                else
                {
                    return TypedResults.BadRequest<ResponseDTO<string>>(
                        new()
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "",
                            Errors = isCreated.Errors
                        }
                    );
                }
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "",
                        Errors = isCreated.Errors
                    }
                );
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
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong.",
                        Errors = new List<IdentityError>()
                        {
                            new()
                            {
                                Code = "Duplicate Phone Number",
                                Description = "Phone Number entered is already in use"
                            }
                        }
                    }
                );
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong."
                    }
                );
            }
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something went wrong."
                }
            );
        }
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
                var user = await _userManager.FindByEmailAsync(tejiloUser.Email!);

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

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > UpdateStudentCourseSubCourse(UpdateStudentCourseSubCourse updateStudentCourseSubCourse)
    {
        try
        {
            var student = await _userManager
                .FindByIdAsync(updateStudentCourseSubCourse.StudentId)
                .ConfigureAwait(false);

            if (student == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No such student found"
                    }
                );
            }

            var subCourse = await _dataContext
                .SubCourseModel
                .FirstOrDefaultAsync(x => x.Id == updateStudentCourseSubCourse.SubCourseId)
                .ConfigureAwait(false);

            if (subCourse == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No such sub course found"
                    }
                );
            }
            student.SubCourseId = updateStudentCourseSubCourse.SubCourseId;

            var rowsAffected = await _userManager.UpdateAsync(student).ConfigureAwait(false);

            if (rowsAffected.Succeeded)
            {
                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Someting went wrong",
                        Errors = rowsAffected.Errors
                    }
                );
            }
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something went wrong."
                }
            );
        }
    }
}
