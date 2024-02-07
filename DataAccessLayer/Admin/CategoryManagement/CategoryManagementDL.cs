using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class CategoryManagementDL : ICategoryManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public CategoryManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetCourse(
        CourseRequestDTO courseRequestDTO
    )
    {
        try
        {
            List<CourseModel> courseModels = [];

            if (courseRequestDTO.Name.Contains("++"))
            {
                string pattern = @"(?<=\w)\+\+(?=\w)";
                var substrings = Regex.Split(courseRequestDTO.Name, pattern);

                foreach (var substring in substrings)
                {
                    if (!string.IsNullOrEmpty(substring))
                    {
                        courseModels.Add(new CourseModel() { Name = substring });
                    }
                }
            }
            else
            {
                courseModels.Add(new CourseModel() { Name = courseRequestDTO.Name });
            }

            await _dataContext.CourseModel.AddRangeAsync(courseModels).ConfigureAwait(true);
            int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(true);

            if (rowsAffected > 0)
            {
                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
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
        catch (Exception ex)
        {
            // Handle exceptions appropriately, e.g., log or report the error
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
    > ToggleCourseStatus(CourseStatusToggleRequestDTO courseIdRequestDTO)
    {
        try
        {
            CourseModel? model = await _dataContext
                .CourseModel
                .FirstAsync(x => x.Id == courseIdRequestDTO.CourseId)
                .ConfigureAwait(true);
            if (model != null)
            {
                model.Status = !model.Status;
                int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(true);
                if (rowsAffected > 0)
                {
                    return TypedResults.Ok<ResponseDTO<string>>(new());
                }
                else
                {
                    return TypedResults.BadRequest<ResponseDTO<string>>(
                        new()
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "No Record Found"
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
                        Message = "No Record Found"
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
                    Message = "Something wend wrong."
                }
            );
        }
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditCourseName(CourseRequestEditNameDTO courseRequestEditNameDTO)
    {
        try
        {
            CourseModel model = await _dataContext
                .CourseModel
                .FirstAsync(x => x.Id == courseRequestEditNameDTO.CourseId)
                .ConfigureAwait(true);
            if (model != null)
            {
                model.Name = courseRequestEditNameDTO.Name;
                int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(true);
                if (rowsAffected > 0)
                {
                    return TypedResults.Ok<ResponseDTO<string>>(new());
                }
                else
                {
                    return TypedResults.BadRequest<ResponseDTO<string>>(
                        new()
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "No Record Found"
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
                        Message = "No Record Found"
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
                    Message = "Something wend wrong."
                }
            );
        }
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > DeleteCourse(CourseDeleteDTO courseDeleteDTO)
    {
        try
        {
            CourseModel model = await _dataContext
                .CourseModel
                .FirstAsync(x => x.Id == courseDeleteDTO.CourseId)
                .ConfigureAwait(true);
            if (model != null)
            {
                _dataContext.CourseModel.Remove(model);
                int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(true);
                if (rowsAffected > 0)
                {
                    return TypedResults.Ok<ResponseDTO<string>>(new());
                }
                else
                {
                    return TypedResults.BadRequest<ResponseDTO<string>>(
                        new()
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "No Record Found"
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
                        Message = "No Record Found"
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
                    Message = "Something wend wrong."
                }
            );
        }
    }

    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<CourseResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetCourses(string? courseName, bool? courseStatus)
    {
        try
        {
            IQueryable<CourseModel> queryCourse = _dataContext.CourseModel;

            if (!string.IsNullOrWhiteSpace(courseName))
            {
                queryCourse = queryCourse.Where(
                    x => x.Name.ToLower().Contains(courseName.ToLower())
                );
            }

            if (courseStatus != null)
            {
                queryCourse = queryCourse.Where(x => x.Status == courseStatus);
            }

            var courseModels = await queryCourse
                .Select(p => _mapper.Map<CourseResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(true);

            return TypedResults.Ok<ResponseDTO<IEnumerable<CourseResponseDTO>>>(
                new() { Data = courseModels }
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
    > SetSubCourse(SubCourseRequestDTO subCourseRequestDTO)
    {
        try
        {
            List<string> courseStrings = [];
            List<SubCourseModel> subCourseModels = [];

            var data = await _dataContext
                .CourseModel
                .Where(x => x.Id == subCourseRequestDTO.CourseId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(true);

            if (data == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Course Found with the given id."
                    }
                );
            }

            if (subCourseRequestDTO.Name.Contains("++"))
            {
                string pattern = @"(?<=\w)\+\+(?=\w)";
                var substrings = Regex.Split(subCourseRequestDTO.Name, pattern);

                foreach (var substring in substrings)
                {
                    if (!string.IsNullOrEmpty(substring))
                    {
                        courseStrings.Add(substring);
                    }
                }
            }
            else
            {
                courseStrings.Add(subCourseRequestDTO.Name);
            }

            foreach (var substring in courseStrings)
            {
                subCourseModels.Add(
                    new SubCourseModel { CourseId = subCourseRequestDTO.CourseId, Name = substring }
                );
            }

            await _dataContext.SubCourseModel.AddRangeAsync(subCourseModels).ConfigureAwait(true);
            int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(true);

            if (rowsAffected > 0)
            {
                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
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
        catch (Exception ex)
        {
            // Handle exceptions appropriately, e.g., log or report the error
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something wend wrong."
                }
            );
        }
    }
}
