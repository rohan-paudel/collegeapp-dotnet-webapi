using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public interface IPerformanceOverviewDL
{
    public Task<
        Results<Ok<ResponseDTO<PerformanceOverviewResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetPerformanceOverview(string studentId, int subcourseId);
}
