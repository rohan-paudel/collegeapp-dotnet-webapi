using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class NoteManagementDL : INoteManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public NoteManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> DeleteNote(
        DeleteNoteRequestDTO deleteNoteRequestDTO
    )
    {
        try
        {
            NoteModel model = await _dataContext
                .NoteModel
                .FirstAsync(x => x.Id == deleteNoteRequestDTO.NoteId)
                .ConfigureAwait(false);
            if (model != null)
            {
                _dataContext.NoteModel.Remove(model);
                int rowsAffected = await _dataContext.SaveChangesAsync().ConfigureAwait(false);
                if (rowsAffected > 0)
                {
                    if (File.Exists(Path.Combine("/data", "Notes", model.FileName)))
                    {
                        File.Delete(Path.Combine("/data", "Notes", model.FileName));
                    }
                    return TypedResults.Ok<ResponseDTO<string>>(new());
                }
                else
                {
                    return TypedResults.BadRequest<ResponseDTO<string>>(
                        new()
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "No Record Found"
                        }
                    );
                }
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Record Found"
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

    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<NoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetNotes(int? topicId, string? noteName, int page, bool? noteStatus)
    {
        try
        {
            IQueryable<NoteModel> queryNote = _dataContext.NoteModel;

            if (topicId != null)
            {
                queryNote = queryNote.Where(x => x.TopicId == topicId);
            }

            if (!string.IsNullOrWhiteSpace(noteName))
            {
                queryNote = queryNote.Where(x => x.Name.ToLower().Contains(noteName));
            }

            if (noteStatus != null)
            {
                queryNote = queryNote.Where(x => x.Status == noteStatus);
            }

            queryNote = queryNote.OrderByDescending(e => e.Id);

            queryNote = queryNote.Skip((page - 1) * 10).Take(10);

            var noteModels = await queryNote
                .Include(p => p.Topic)
                .Select(p => _mapper.Map<NoteResponseDTO>(p))
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<NoteResponseDTO>>>(
                new()
                {
                    Data = noteModels,
                    // TotalPageCount = pageCount,
                    CurrentPageCount = page
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

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetNote(
        NoteRequestDTO noteRequestDTO
    )
    {
        if (noteRequestDTO.File == null || noteRequestDTO.File.Length == 0)
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new() { StatusCode = StatusCodes.Status400BadRequest, Message = "No File Uploaded" }
            );

        if (
            Path.GetExtension(noteRequestDTO.File.FileName).ToLower() != ".pdf"
            || noteRequestDTO.File.Length > 3 * 1024 * 1024
        )
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid file. Only PDF files up to 3MB are allowed."
                }
            );
        }

        try
        {
            var data = await _dataContext
                .TopicModel
                .Where(x => x.Id == noteRequestDTO.TopicId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (data == null)
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Such Topics Found."
                    }
                );
            }

            try
            {
                // Generate a unique filename using GUID
                var uniqueFileName = $"{Guid.NewGuid()}.pdf";

                // Save the file to a secure location with the unique filename
                var filePath = Path.Combine("/data", "Notes", uniqueFileName); // Adjust this path to your desired folder
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!); // Create directory if it doesn't exist
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await noteRequestDTO.File.CopyToAsync(stream);
                }

                noteRequestDTO.FileNameByDeveloper = uniqueFileName;
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

            await _dataContext
                .NoteModel
                .AddAsync(_mapper.Map<NoteModel>(noteRequestDTO))
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

    public async Task<
        Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>
    > ToggleNoteStatus(NoteStatusToggleRequestDTO noteStatusToggleRequestDTO)
    {
        try
        {
            NoteModel? model = await _dataContext
                .NoteModel
                .FirstOrDefaultAsync(x => x.Id == noteStatusToggleRequestDTO.NoteId)
                .ConfigureAwait(false);

            if (model != null)
            {
                model.Status = !model.Status;
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
                            Message = "No Record Found"
                        }
                    );
                }
            }
            else
            {
                return TypedResults.BadRequest<ResponseDTO<string>>(
                    new()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "No Record Found"
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
