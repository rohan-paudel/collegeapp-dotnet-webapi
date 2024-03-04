using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeAppDotnetWebApi;

public class QuoteManagementDL : IQuoteManagementDL
{
    private readonly AppDataContext _dataContext;
    private readonly IMapper _mapper;

    public QuoteManagementDL(AppDataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<QuoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetQuotes()
    {
        try
        {
            var quotes = await _dataContext
                .QuoteModel
                .Select(x => new QuoteResponseDTO { Quote = x.Description, Title = x.Title })
                .ToListAsync()
                .ConfigureAwait(false);

            return TypedResults.Ok<ResponseDTO<IEnumerable<QuoteResponseDTO>>>(
                new() { Data = quotes }
            );
        }
        catch (Exception)
        {
            return TypedResults.BadRequest<ResponseDTO<string>>(
                new()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Someting went wrong"
                }
            );
        }
    }

    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetQuote(
        QuoteRequestDTO quoteRequestDTO
    )
    {
        try
        {
            await _dataContext
                .QuoteModel
                .AddAsync(_mapper.Map<QuoteModel>(quoteRequestDTO))
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
                    Message = "Someting went wrong"
                }
            );
        }
    }
}
