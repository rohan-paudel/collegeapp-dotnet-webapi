using Microsoft.AspNetCore.Identity;

namespace CollegeAppDotnetWebApi;

public class ResponseDTO<T>
{
    public int StatusCode { get; set; } = StatusCodes.Status200OK;
    public string Message { get; set; } = "Successfull.";

    public IEnumerable<IdentityError>? Errors { get; set; }

    public T? Data { get; set; }
}

public class ErrorOfIntity
{
    public string? Code { get; set; }
    public string? Description { get; set; }
}
