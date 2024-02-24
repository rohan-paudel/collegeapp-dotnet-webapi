using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public class VdoCipherDL : IVdoCipherDL
{
    public async Task<
        Results<Ok<ResponseDTO<GetOtpResponseDTO>>, BadRequest<ResponseDTO<string>>>
    > GetOtpToPlayVideo(string videoPlayId, HttpClient httpClient)
    {
        try
        {
            var response = await httpClient
                .PostAsJsonAsync(
                    $"https://dev.vdocipher.com/api/videos/{videoPlayId}/otp",
                    new { ttl = 300 }
                )
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                // Read response content
                var responseData = await response
                    .Content
                    .ReadFromJsonAsync<GetOtpResponseDTO>()
                    .ConfigureAwait(false);
                ;

                return TypedResults.Ok<ResponseDTO<GetOtpResponseDTO>>(
                    new() { Data = responseData }
                );
            }
            else
            {
                // Handle unsuccessful response
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong"
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
                    Message = "Something went wrong"
                }
            );
        }
    }
}
