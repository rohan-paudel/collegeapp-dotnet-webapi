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
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > PostChapterTest(PostChapterTestDTO postChapterTestDTO)
    {
        try
        {
            List<QuestionResponseFromDatabaseDTO> chapterQuestions = await _dataContext
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
                .ToListAsync()
                .ConfigureAwait(false);

            int numberOfCorrect = 0;
            int numberOfIncorrect = 0;
            int numberOfUnanswerd = 0;
            float totalMarkObtained = 0.0f;
            float totalMarks = 0.0f;

            foreach (var option in postChapterTestDTO.ChapterTestDetailedData)
            {
                QuestionResponseFromDatabaseDTO question = chapterQuestions.FirstOrDefault(
                    x => x.Id == option.ChapterTestQuestionId
                )!;

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
                .AddAsync(_mapper.Map<ChapterTestUserDataModel>(postChapterTestDTO))
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
