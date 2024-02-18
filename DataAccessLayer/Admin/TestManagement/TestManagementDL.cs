using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class TestManagementDL : ITestManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public TestManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<TestResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetTest(int? testType, string? name, bool? testStatus)
    {
        try
        {
            IQueryable<ChapterTestModel> queryChapterTest = _dataContext.ChapterTestModel;
            IQueryable<LiveTestModel> queryLiveTest = _dataContext.LiveTestModel;

            List<TestResponseDTO> listOfAllTest = [];

            if (!string.IsNullOrWhiteSpace(name))
            {
                queryChapterTest = queryChapterTest.Where(x => x.Name.ToLower().Contains(name));
                queryLiveTest = queryLiveTest.Where(x => x.Name.ToLower().Contains(name));
            }

            if (testStatus != null)
            {
                queryChapterTest = queryChapterTest.Where(x => x.Status == testStatus);
                queryLiveTest = queryLiveTest.Where(x => x.Status == testStatus);
            }

            if (testType != null)
            {
                if (testType == 0)
                {
                    var chapterTestModel = await queryChapterTest
                        .Include(p => p.Topic)
                        .ThenInclude(x => x.Subject)
                        .ThenInclude(y => y.SubCourses)
                        .ThenInclude(z => z.Course)
                        .OrderByDescending(e => e.Id)
                        .Select(p => _mapper.Map<TestResponseDTO>(p))
                        .ToListAsync()
                        .ConfigureAwait(false);

                    return TypedResults.Ok<ResponseDTO<IEnumerable<TestResponseDTO>>>(
                        new() { Data = chapterTestModel }
                    );
                }
                else if (testType == 1)
                {
                    var liveTestModel = await queryLiveTest
                        .Include(p => p.Subject)
                        .ThenInclude(x => x.SubCourses)
                        .ThenInclude(y => y.Course)
                        .OrderByDescending(e => e.Id)
                        .Select(p => _mapper.Map<TestResponseDTO>(p))
                        .ToListAsync()
                        .ConfigureAwait(false);

                    return TypedResults.Ok<ResponseDTO<IEnumerable<TestResponseDTO>>>(
                        new() { Data = liveTestModel }
                    );
                }
                else
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

            var chapterTestModelForBoth = await queryChapterTest
                .Include(p => p.Topic)
                .ThenInclude(x => x.Subject)
                .ThenInclude(y => y.SubCourses)
                .ThenInclude(z => z.Course)
                .Select(p => _mapper.Map<TestResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(false);

            var liveTestModelForBoth = await queryLiveTest
                .Include(p => p.Subject)
                .ThenInclude(x => x.SubCourses)
                .ThenInclude(y => y.Course)
                .Select(p => _mapper.Map<TestResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(false);

            listOfAllTest.AddRange(chapterTestModelForBoth);
            listOfAllTest.AddRange(liveTestModelForBoth);

            listOfAllTest = [.. listOfAllTest.OrderByDescending(d => d.CreatedAt)];

            return TypedResults.Ok<ResponseDTO<IEnumerable<TestResponseDTO>>>(
                new() { Data = listOfAllTest }
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
    > SetChapterTest(ChapterTestRequestDTO chapterTestRequestDTO)
    {
        try
        {
            // var data = await _dataContext
            //     .ChapterTestModel
            //     .Where(x => x.Id == subCourseRequestDTO.CourseId)
            //     .FirstOrDefaultAsync()
            //     .ConfigureAwait(false);

            await _dataContext
                .ChapterTestModel
                .AddAsync(_mapper.Map<ChapterTestModel>(chapterTestRequestDTO))
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
    > SetChapterTestQuestion(ChapterTestQuestionDTO chapterTestQuestionDTO)
    {
        try
        {
            int countOfNumberOfTrues = 0;

            foreach (var option in chapterTestQuestionDTO.Options)
            {
                if (option.IsCorrect)
                {
                    countOfNumberOfTrues++;
                }
            }

            if (countOfNumberOfTrues == 0)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One must be true."
                    }
                );
            }
            else if (countOfNumberOfTrues > 1)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Only one must be true."
                    }
                );
            }

            // var data = await _dataContext
            //     .ChapterTestModel
            //     .Where(x => x.Id == subCourseRequestDTO.CourseId)
            //     .FirstOrDefaultAsync()
            //     .ConfigureAwait(false);

            await _dataContext
                .ChapterTestQuestionModel
                .AddAsync(_mapper.Map<ChapterTestQuestionModel>(chapterTestQuestionDTO))
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

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetLiveTest(LiveTestRequestDTO liveTestRequestDTO)
    {
        try
        {
            await _dataContext
                .LiveTestModel
                .AddAsync(_mapper.Map<LiveTestModel>(liveTestRequestDTO))
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
}
