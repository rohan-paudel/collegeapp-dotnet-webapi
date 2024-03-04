using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeAppDotnetWebApi;

[ApiController]
[Route("api/[controller]/[Action]")]
public class QuoteManagementController : ControllerBase
{
    private readonly IQuoteManagementDL _quoteManagementDL;

    public QuoteManagementController(IQuoteManagementDL quoteManagementDL)
    {
        _quoteManagementDL = quoteManagementDL;
    }

    [HttpPost]
    public async Task<Results<Ok<ResponseDTO<string>>, BadRequest<ResponseDTO<string>>>> SetQuote(
        QuoteRequestDTO quoteRequestDTO
    )
    {
        var result = await _quoteManagementDL.SetQuote(quoteRequestDTO).ConfigureAwait(false);
        return result;
    }

    [HttpGet]
    public async Task<
        Results<Ok<ResponseDTO<IEnumerable<QuoteResponseDTO>>>, BadRequest<ResponseDTO<string>>>
    > GetQuotes()
    {
        var result = await _quoteManagementDL.GetQuotes().ConfigureAwait(false);
        return result;
    }
}
