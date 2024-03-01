using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class ChapterTestDL : IChapterTestDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public ChapterTestDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<
            Ok<ResponseDTO<IsAlreadyGivenChapterTestResponseDTO>>,
            BadRequest<ResponseDTO<string>>
        >
    > CheckIfAlreadyGivenChapterTest(string studentId, int chapterTestId)
    {
        try
        {
            var exists = await _dataContext
                .ChapterTestUserDataModel
                .AnyAsync(x => x.StudentId == studentId && x.ChapterTestId == chapterTestId)
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IsAlreadyGivenChapterTestResponseDTO>>(
                new() { Data = new IsAlreadyGivenChapterTestResponseDTO { IsGiven = exists } }
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
        Results<
            Ok<ResponseDTO<UserChapterTestPerformanceResponseDTO>>,
            BadRequest<ResponseDTO<string>>
        >
    > GetUserScoreCardForChapterTest(string studentId, int chapterTestId)
    {
        try
        {
            var chapterTeetUserData = await _dataContext
                .ChapterTestUserDataModel
                .Where(x => x.StudentId == studentId && x.ChapterTestId == chapterTestId)
                .Select(
                    x =>
                        new UserChapterTestPerformanceResponseDTO
                        {
                            Id = x.Id,
                            Correct = x.Correct,
                            Incorrect = x.Incorrect,
                            Unanswered = x.Unanswered,
                            MarksObtained = x.MarksObtained,
                            TotalMark = x.TotalMark,
                            TotalQuestion = x.TotalQuestion
                        }
                )
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<UserChapterTestPerformanceResponseDTO>>(
                new() { Data = chapterTeetUserData }
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
    > PostChapterTest(PostChapterTestDTO postChapterTestDTO)
    {
        try
        {
            var chapterQuestions = await _dataContext
                .ChapterTestQuestionModel
                .Where(x => x.ChapterTestId == postChapterTestDTO.ChapterTestId)
                .Select(
                    x =>
                        new QuestionResponseFromDatabaseDTO
                        {
                            Id = x.Id,
                            AnswerId = x.AnswerId,
                            PositiveMark = x.PositiveMark,
                            NegativeMark = x.NegativeMark
                        }
                )
                .ToDictionaryAsync(x => x.Id)
                .ConfigureAwait(false); // Convert to dictionary

            int numberOfCorrect = 0;
            int numberOfIncorrect = 0;
            int numberOfUnanswerd = 0;
            float totalMarkObtained = 0.0f;
            float totalMarks = 0.0f;

            foreach (var option in postChapterTestDTO.ChapterTestDetailedData)
            {
                if (!chapterQuestions.TryGetValue(option.ChapterTestQuestionId, out var question))
                {
                    // Handle the case where the question ID was not found in the dictionary
                    continue;
                }

                if (option.UserAnswerId == null)
                {
                    numberOfUnanswerd++;
                    totalMarks += question.PositiveMark;
                    continue;
                }

                if (option.UserAnswerId == question.AnswerId)
                {
                    numberOfCorrect++;
                    totalMarkObtained += question.PositiveMark;
                    totalMarks += question.PositiveMark;
                }
                else
                {
                    numberOfIncorrect++;
                    totalMarkObtained -= question.NegativeMark;
                    totalMarks += question.PositiveMark;
                }
            }

            postChapterTestDTO.Correct = numberOfCorrect;
            postChapterTestDTO.Incorrect = numberOfIncorrect;
            postChapterTestDTO.Unanswered = numberOfUnanswerd;
            postChapterTestDTO.TotalMark = totalMarks;
            postChapterTestDTO.MarksObtained = totalMarkObtained;
            postChapterTestDTO.TotalQuestion = postChapterTestDTO.ChapterTestDetailedData.Count;

            await _dataContext
                .ChapterTestUserDataModel
                .AddAsync(_mapper.Map<ChapterTestUserDataModel>(postChapterTestDTO));
            int rowsAffected = await _dataContext.SaveChangesAsync();

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
            // Log the exception details here.
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
    > ResetTheGivenChapterTest(string studentId, int chapterTestId)
    {
        try
        {
            var chapterTestUserData = await _dataContext
                .ChapterTestUserDataModel
                .FirstOrDefaultAsync(
                    x => x.StudentId == studentId && x.ChapterTestId == chapterTestId
                )
                .ConfigureAwait(false);

            if (chapterTestUserData != null)
            {
                _dataContext.ChapterTestUserDataModel.Remove(chapterTestUserData);
                await _dataContext.SaveChangesAsync().ConfigureAwait(false);

                return TypedResults.Ok<ResponseDTO<string>>(new() { Data = "Successful" });
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Record not found"
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
                    Message = "Something went wrong"
                }
            );
        }
    }
}
