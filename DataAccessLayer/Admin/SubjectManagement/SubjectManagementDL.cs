using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class SubjectManagementDL : ISubjectManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public SubjectManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > DeleteSubject(SubjectDeleteDTO subjectDeleteDTO)
    {
        try
        {
            SubjectModel? model = await _dataContext
                .SubjectModel
                .FirstOrDefaultAsync(x => x.Id == subjectDeleteDTO.SubjectId)
                .ConfigureAwait(true);

            if (model == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Record Found"
                    }
                );
            }

            _dataContext.SubjectModel.Remove(model);
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
    > EditSubjectName(SubjectRequestEditNameDTO subjectRequestEditNameDTO)
    {
        try
        {
            SubjectModel? model = await _dataContext
                .SubjectModel
                .FirstOrDefaultAsync(x => x.Id == subjectRequestEditNameDTO.SubjectId)
                .ConfigureAwait(true);

            if (model == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Record Found"
                    }
                );
            }

            model.Name = subjectRequestEditNameDTO.Name;
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

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetSubject(
        SubjectRequestDTO subjectRequestDTO
    )
    {
        try
        {
            List<string> subjectStrings = [];
            List<SubjectModel> subjectModels = [];

            if (subjectRequestDTO.Name.Contains("++"))
            {
                string pattern = @"(?<=\w)\+\+(?=\w)";
                var substrings = Regex.Split(subjectRequestDTO.Name, pattern);

                foreach (var substring in substrings)
                {
                    if (!string.IsNullOrEmpty(substring))
                    {
                        subjectStrings.Add(substring);
                    }
                }
            }
            else
            {
                subjectStrings.Add(subjectRequestDTO.Name);
            }

            foreach (var substring in subjectStrings)
            {
                subjectModels.Add(new SubjectModel { Name = substring });
            }

            await _dataContext.SubjectModel.AddRangeAsync(subjectModels).ConfigureAwait(true);

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
        catch (Exception)
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
    > ToggleSubjectStatus(SubjectStatusToggleRequestDTO subjectStatusToggleRequestDTO)
    {
        try
        {
            SubjectModel? model = await _dataContext
                .SubjectModel
                .FirstOrDefaultAsync(x => x.Id == subjectStatusToggleRequestDTO.SubjectId)
                .ConfigureAwait(true);

            if (model == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Subject Found with the given id."
                    }
                );
            }
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
}
