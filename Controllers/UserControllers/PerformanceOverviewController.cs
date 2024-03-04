using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class PerformanceOverviewController : ControllerBase
{
    private readonly IPerformanceOverviewDL _performanceOverviewDL;

    public PerformanceOverviewController(IPerformanceOverviewDL performanceOverviewDL)
    {
        _performanceOverviewDL = performanceOverviewDL;
    }

    [HttpGet]
    [Authorize(Policy = "PolicyForMobileDevice")]
    public async Task<
        Results<Ok<ResponseDTO<PerformanceOverviewResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetPerformanceOverview()
    {
        var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        int subcourseId = Int32.Parse(User.FindFirstValue("SubCourseId")!);
        return await _performanceOverviewDL
            .GetPerformanceOverview(studentId, subcourseId)
            .ConfigureAwait(false);
    }
}
