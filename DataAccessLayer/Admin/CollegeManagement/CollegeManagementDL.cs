using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class CollegeManagementDL : ICollegeManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public CollegeManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteCollege(
        CollegeDeleteDTO collegeDeleteDTO
    )
    {
        throw new NotImplementedException();
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditCollege(EditCollegeRequestDTO editCollegeRequestDTO)
    {
        try
        {
            var college = await _dataContext
                .TejiloCollege
                .FirstOrDefaultAsync(x => x.Id == editCollegeRequestDTO.CollegeId);

            if (college == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No such college found"
                    }
                );
            }

            _mapper.Map(editCollegeRequestDTO, college);
            // _dataContext.TejiloCollege.Update(_mapper.Map<TejiloCollege>(editCollegeRequestDTO));
            var rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(true);

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
                        Message = "Someting went wrong"
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

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetCollege(
        CollegeRequestDTO collegeRequestDTO
    )
    {
        try
        {
            await _dataContext
                .TejiloCollege
                .AddAsync(_mapper.Map<TejiloCollege>(collegeRequestDTO))
                .ConfigureAwait(true);
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
    > ToggleCollegeStatus(CollegeStatusToggleRequestDTO collegeStatusToggleRequestDTO)
    {
        try
        {
            TejiloCollege? model = await _dataContext
                .TejiloCollege
                .FirstOrDefaultAsync(x => x.Id == collegeStatusToggleRequestDTO.CollegeId)
                .ConfigureAwait(true);

            if (model != null)
            {
                model.Status = !model.Status;
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
}
