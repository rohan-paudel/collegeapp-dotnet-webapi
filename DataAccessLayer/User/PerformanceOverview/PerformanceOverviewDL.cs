using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class PerformanceOverviewDL : IPerformanceOverviewDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public PerformanceOverviewDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<PerformanceOverviewResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetPerformanceOverview(string studentId, int subcourseId)
    {
        try
        {
            int countsOfAllChapterTest = 0;

            var totalChapterCount = await _dataContext
                .CategoryCountModel
                .Select(x => new ChapterTestCountList { count = x.ChapterTestCount })
                .ToListAsync()
                .ConfigureAwait(false);

            var countOfTestGivenByUser = await _dataContext
                .ChapterTestUserDataModel
                .Where(x => x.StudentId == studentId)
                .CountAsync()
                .ConfigureAwait(false);

            var SubCourseById = await _dataContext
                .SubCourseModel
                .Where(x => x.Id == subcourseId)
                .Select(x => new SubCourseResponseOnlyIdDTO { Id = x.Id, Name = x.Name })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            foreach (ChapterTestCountList chapterTest in totalChapterCount)
            {
                countsOfAllChapterTest += chapterTest.count;
            }

            float percentage = (countOfTestGivenByUser / countsOfAllChapterTest) * 100;

            return TypedResults.Ok<ResponseDTO<PerformanceOverviewResponseDTO>>(
                new()
                {
                    Data = new PerformanceOverviewResponseDTO
                    {
                        PercentageForChapterTest = percentage,
                        TotalChapterTest = countsOfAllChapterTest,
                        ChaterTestGivenByUser = countOfTestGivenByUser,
                        SubCourse = SubCourseById
                    }
                }
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
}
