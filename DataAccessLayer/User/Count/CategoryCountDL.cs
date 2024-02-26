using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class CategoryCountDL : ICategoryCountDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public CategoryCountDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<CategoryCountResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetTopicsCategoryCount(int topicId)
    {
        try
        {
            IQueryable<CategoryCountModel> queryCategoryCount = _dataContext.CategoryCountModel;

            queryCategoryCount = queryCategoryCount.Where(x => x.TopicId == topicId);

            var categoryCountModels = await queryCategoryCount
                .ProjectTo<CategoryCountResponseDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<CategoryCountResponseDTO>>(
                new() { Data = categoryCountModels }
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
    > UpdateNoteCount(int topicId)
    {
        try
        {
            CategoryCountModel? model = await _dataContext
                .CategoryCountModel
                .FirstOrDefaultAsync(x => x.TopicId == topicId)
                .ConfigureAwait(false);
            if (model != null)
            {
                await _dataContext
                    .Database
                    .ExecuteSqlAsync(
                        @$"
            UPDATE CategoryCountModel
            SET NoteCount = (
                SELECT COUNT(*)
                FROM NoteModel
                WHERE TopicId = {topicId}
            )
            WHERE TopicId = {topicId};
        "
                    )
                    .ConfigureAwait(false);

                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
            }
            else
            {
                var newCategoryCount = new CategoryCountModel
                {
                    VideoCount = 0,
                    NoteCount = 1,
                    ChapterTestCount = 0,
                    TopicId = topicId
                };

                await _dataContext
                    .CategoryCountModel
                    .AddAsync(newCategoryCount)
                    .ConfigureAwait(false);
                await _dataContext.SaveChangesAsync().ConfigureAwait(false);
                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
            }
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
    > UpdateVideoCount(int topicId)
    {
        try
        {
            CategoryCountModel? model = await _dataContext
                .CategoryCountModel
                .FirstOrDefaultAsync(x => x.TopicId == topicId)
                .ConfigureAwait(false);
            if (model != null)
            {
                await _dataContext
                    .Database
                    .ExecuteSqlAsync(
                        @$"
            UPDATE CategoryCountModel
            SET VideoCount = (
                SELECT COUNT(*)
                FROM VideoModel
                WHERE TopicId = {topicId}
            )
            WHERE TopicId = {topicId};
        "
                    )
                    .ConfigureAwait(false);

                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
            }
            else
            {
                var newCategoryCount = new CategoryCountModel
                {
                    VideoCount = 1,
                    NoteCount = 0,
                    ChapterTestCount = 0,
                    TopicId = topicId
                };

                await _dataContext
                    .CategoryCountModel
                    .AddAsync(newCategoryCount)
                    .ConfigureAwait(false);
                await _dataContext.SaveChangesAsync().ConfigureAwait(false);
                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successfull" });
            }
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
}
