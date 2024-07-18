using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class DiscussionManagementDL : IDiscussionManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public DiscussionManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<
            Ok<ResponseDTO<IEnumerable<DiscussionResponseDTO>>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetDiscussion(int collegeId, int topicId, int page, bool? discussionStatus)
    {
        try
        {
            IQueryable<DiscussionModel> queryDiscussion = _dataContext.DiscussionModel;

            queryDiscussion = queryDiscussion.Where(
                x => x.CollegeId == collegeId && x.TopicId == topicId
            );

            if (discussionStatus != null)
            {
                queryDiscussion = queryDiscussion.Where(x => x.Status == discussionStatus);
            }

            queryDiscussion = queryDiscussion.Skip((page - 1) * 50).Take(50);

            var noteModels = await queryDiscussion
                .Include(x => x.TejiloUser)
                .Include(x => x.Queries)
                .Select(p => _mapper.Map<DiscussionResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<DiscussionResponseDTO>>>(
                new()
                {
                    Data = noteModels,
                    // TotalPageCount = pageCount,
                    CurrentPageCount = page
                }
            );
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

    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<QueryResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetQuery(int discussionId, int page, bool? queryStatus)
    {
        try
        {
            IQueryable<QueryModel> queryQuery = _dataContext.QueryModel;

            queryQuery = queryQuery.Where(x => x.DiscussionId == discussionId);

            if (queryStatus != null)
            {
                queryQuery = queryQuery.Where(x => x.Status == queryStatus);
            }

            queryQuery = queryQuery.Skip((page - 1) * 10).Take(10);
            var queryModels = await queryQuery
                .Include(p => p.TejiloUser)
                .Select(p => _mapper.Map<QueryResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<QueryResponseDTO>>>(
                new() { Data = queryModels }
            );
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

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetDiscussion(DiscussionRequestDTO discussionRequestDTO)
    {
        try
        {
            var data = await _dataContext
                .TopicModel
                .Where(x => x.Id == discussionRequestDTO.TopicId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (data == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Such Topics Found."
                    }
                );
            }

            await _dataContext
                .DiscussionModel
                .AddAsync(_mapper.Map<DiscussionModel>(discussionRequestDTO))
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
                    Message = "Someting went wrong"
                }
            );
        }
    }

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetQuery(
        QueryRequestDTO queryRequestDTO
    )
    {
        try
        {
            List<string> topicStrings = [];
            QueryModel queryModel;

            var data = await _dataContext
                .DiscussionModel
                .Where(x => x.Id == queryRequestDTO.DiscussionId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (data == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Discussion Found with the given id."
                    }
                );
            }

            queryModel = _mapper.Map<QueryModel>(queryRequestDTO);

            await _dataContext.QueryModel.AddRangeAsync(queryModel).ConfigureAwait(false);
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
