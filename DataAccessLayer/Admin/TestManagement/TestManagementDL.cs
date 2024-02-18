using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

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
}
