using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class TopicManagementController : ControllerBase
{
    private readonly ITopicManagementDL _topicManagementDL;

    public TopicManagementController(ITopicManagementDL topicManagementDL)
    {
        _topicManagementDL = topicManagementDL;
    }
}
