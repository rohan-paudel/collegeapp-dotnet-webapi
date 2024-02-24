using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class UserNoteManagementDL : IUserNoteManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public UserNoteManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > EditCollege(UserNoteUpdateDTO userNoteUpdateDTO)
    {
        try
        {
            var userNote = await _dataContext
                .UserNoteModel
                .FirstOrDefaultAsync(x => x.Id == userNoteUpdateDTO.Id)
                .ConfigureAwait(false);

            if (userNote == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong."
                    }
                );
            }

            _mapper.Map(userNoteUpdateDTO, userNote);
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
        Results<Ok<ResponseDTO<IEnumerable<UserNoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetUserNotes(int topicId, string studentId, bool? noteStatus)
    {
        try
        {
            IQueryable<UserNoteModel> queryUserNote = _dataContext.UserNoteModel;

            queryUserNote = queryUserNote.Where(
                x => x.StudentId == studentId && x.TopicId == topicId
            );

            if (noteStatus != null)
            {
                queryUserNote = queryUserNote.Where(x => x.Status == noteStatus);
            }

            queryUserNote = queryUserNote.OrderByDescending(e => e.Id);
            var userNoteModels = await queryUserNote
                .Select(p => _mapper.Map<UserNoteResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<UserNoteResponseDTO>>>(
                new() { Data = userNoteModels }
            );
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Something went wrong"
                }
            );
        }
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetUserNote(UserNoteRequestDTO userNoteRequestDTO)
    {
        try
        {
            await _dataContext
                .UserNoteModel
                .AddAsync(_mapper.Map<UserNoteModel>(userNoteRequestDTO))
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
                    Message = "Something went wrong."
                }
            );
        }
    }
}
