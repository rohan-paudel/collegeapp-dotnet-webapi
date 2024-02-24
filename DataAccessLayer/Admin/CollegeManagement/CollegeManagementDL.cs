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
                .FirstOrDefaultAsync(x => x.Id == editCollegeRequestDTO.CollegeId)
                .ConfigureAwait(false);

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
            var rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(false);

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

    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<CollegeResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetCollege(string? searchTerm, bool? collegeStatus)
    {
        try
        {
            IQueryable<TejiloCollege> queryCourse = _dataContext.TejiloCollege;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                queryCourse = queryCourse.Where(
                    x =>
                        x.Name.ToLower().Contains(searchTerm.ToLower())
                        || x.Email.ToLower().Contains(searchTerm.ToLower())
                        || x.Mobile.Contains(searchTerm.ToLower())
                        || x.Telephone.Contains(searchTerm.ToLower())
                );
            }

            if (collegeStatus != null)
            {
                queryCourse = queryCourse.Where(x => x.Status == collegeStatus);
            }

            var collegeModels = await queryCourse
                .Include(x => x.Students)
                .OrderByDescending(e => e.Id)
                .Select(p => _mapper.Map<CollegeResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<CollegeResponseDTO>>>(
                new() { Data = collegeModels }
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
        Results<Ok<ResponseDTO<CollegeResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetCollegeById(string collegeId)
    {
        try
        {
            IQueryable<TejiloCollege> queryCourse = _dataContext.TejiloCollege;

            var collegeModels = await queryCourse
                .Where(p => p.Id == Int32.Parse(collegeId))
                .Select(p => _mapper.Map<CollegeResponseDTO>(p))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<CollegeResponseDTO>>(new() { Data = collegeModels });
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

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetCollege(
        CollegeRequestDTO collegeRequestDTO
    )
    {
        try
        {
            await _dataContext
                .TejiloCollege
                .AddAsync(_mapper.Map<TejiloCollege>(collegeRequestDTO))
                .ConfigureAwait(false);
            int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(false);

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
                .ConfigureAwait(false);

            if (model != null)
            {
                model.Status = !model.Status;
                int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(false);
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
