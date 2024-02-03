namespace CollegeAppDotnetWebApi;

public class ResponseDTO<T>
{
    public int StatusCode { get; set; } = StatusCodes.Status200OK;
    public string Message { get; set; } = "Successfull.";

    public T? Data { get; set; }
}
