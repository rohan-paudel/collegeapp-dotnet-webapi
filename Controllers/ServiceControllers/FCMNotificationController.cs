using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("/api/[controller]/[Action]")]
public class FCMNotificationController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendMessageAsync([FromBody] MessageRequest request)
    {
        var message = new Message()
        {
            Notification = new Notification { Title = request.Title, Body = request.Body, },
            Data = new Dictionary<string, string>()
            {
                ["Title"] = request.Title,
                ["Body"] = request.Body
                // ["FirstName"] = "John",
                // ["LastName"] = "Doe"
            },
            Topic = request.Topic
        };

        var messaging = FirebaseMessaging.DefaultInstance;
        var result = await messaging.SendAsync(message).ConfigureAwait(false);

        if (!string.IsNullOrEmpty(result))
        {
            // Message was sent successfully
            return Ok("Message sent successfully!");
        }
        else
        {
            // There was an error sending the message
            throw new Exception("Error sending the message.");
        }
    }
}
