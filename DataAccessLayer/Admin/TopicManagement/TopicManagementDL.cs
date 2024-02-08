using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class TopicManagementDL : ITopicManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public TopicManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > DeleteTopic(TopicDeleteDTO topicDeleteDTO)
    {
        try
        {
            TopicModel? model = await _dataContext
                .TopicModel
                .FirstOrDefaultAsync(x => x.Id == topicDeleteDTO.TopicId)
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

            _dataContext.TopicModel.Remove(model);
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
    > EditTopicName(TopicRequestEditNameDTO topicRequestEditNameDTO)
    {
        try
        {
            SubjectModel? subjectModel = null;

            if (topicRequestEditNameDTO.SubjectId != null)
            {
                subjectModel = await _dataContext
                    .SubjectModel
                    .FirstOrDefaultAsync(x => x.Id == topicRequestEditNameDTO.SubjectId)
                    .ConfigureAwait(true);
                if (subjectModel == null)
                {
                    return TypedResults.BadRequest<ResponseDTO<string>>(
                        new()
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "No such subject found"
                        }
                    );
                }
            }
            TopicModel? model = await _dataContext
                .TopicModel
                .FirstOrDefaultAsync(x => x.Id == topicRequestEditNameDTO.TopicId)
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

            model.Name = topicRequestEditNameDTO.Name;
            if (topicRequestEditNameDTO.SubjectId != null)
            {
                model.SubjectId = topicRequestEditNameDTO.SubjectId;
            }

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

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetTopic(
        TopicRequestDTO topicRequestDTO
    )
    {
        try
        {
            List<string> topicStrings = [];
            List<TopicModel> topicModels = [];

            var data = await _dataContext
                .SubjectModel
                .Where(x => x.Id == topicRequestDTO.SubjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(true);

            if (data == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Subject Found with the given id."
                    }
                );
            }

            if (topicRequestDTO.Name.Contains("++"))
            {
                string pattern = @"(?<=\w)\+\+(?=\w)";
                var substrings = Regex.Split(topicRequestDTO.Name, pattern);

                foreach (var substring in substrings)
                {
                    if (!string.IsNullOrEmpty(substring))
                    {
                        topicStrings.Add(substring);
                    }
                }
            }
            else
            {
                topicStrings.Add(topicRequestDTO.Name);
            }

            foreach (var substring in topicStrings)
            {
                topicModels.Add(
                    new TopicModel { SubjectId = topicRequestDTO.SubjectId, Name = substring }
                );
            }

            await _dataContext.TopicModel.AddRangeAsync(topicModels).ConfigureAwait(true);
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
                    Message = "Something went wrong."
                }
            );
        }
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleTopicStatus(TopicStatusToggleRequestDTO topicStatusToggleRequestDTO)
    {
        try
        {
            TopicModel? model = await _dataContext
                .TopicModel
                .FirstOrDefaultAsync(x => x.Id == topicStatusToggleRequestDTO.TopicId)
                .ConfigureAwait(true);

            if (model == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Topic Found with the given id."
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
