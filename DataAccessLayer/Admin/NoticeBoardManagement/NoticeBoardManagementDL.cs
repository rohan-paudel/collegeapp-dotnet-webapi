using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeAppDotnetWebApi;

public class NoticeBoardManagementDL : INoticeBoardManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public NoticeBoardManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > SetNoticeBoard(NoticeBoardRequestDTO noticeBoardRequestDTO)
    {
        try
        {
            if (noticeBoardRequestDTO.File != null)
            {
                string fileExtension = Path.GetExtension(noticeBoardRequestDTO.File.FileName)
                    .ToLower();
                try
                {
                    if (
                        fileExtension == ".pdf"
                        || fileExtension == ".png"
                        || fileExtension == ".jpeg"
                        || fileExtension == ".jpg"
                        || noticeBoardRequestDTO.File.Length < 1 * 1024 * 1024
                    )
                    {
                        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

                        // Save the file to a secure location with the unique filename
                        var filePath = Path.Combine("/data", "NoticeBoard", uniqueFileName); // Adjust this path to your desired folder
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!); // Create directory if it doesn't exist
                        using (var stream = new FileStream(filePath, FileMode.CreateNew))
                        {
                            await noticeBoardRequestDTO.File.CopyToAsync(stream);
                        }

                        noticeBoardRequestDTO.FileNameByDeveloper = uniqueFileName;
                    }
                    else
                    {
                        return TypedResults.BadRequest<ResponseDTO<string>>(
                            new()
                            {
                                StatusCode = StatusCodes.Status400BadRequest,
                                Message =
                                    "Invalid file. Only PDF, PNG, JPEG, JPG files up to 1MB are allowed."
                            }
                        );
                    }
                    // Generate a unique filename using GUID
                }
                catch (Exception)
                {
                    return TypedResults.BadRequest<ResponseDTO<string>>(
                        new()
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "Something went wrong with file upload."
                        }
                    );
                }
            }

            await _dataContext
                .NoticeBoardModel
                .AddAsync(_mapper.Map<NoticeBoardModel>(noticeBoardRequestDTO))
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
}
