using LinksShorterer.Models;
using LinksShorterer.ShortererService;
using Microsoft.AspNetCore.Mvc;

namespace LinksShorterer.Controllers;

[ApiController]
[Route("api")]
public class LinksController : ControllerBase
{
    private readonly IShorterer _shorterer;

    public LinksController(IShorterer shorterer)
    {
        _shorterer = shorterer;
    }

    [HttpPost]
    [Route("generate")]
    public async Task<ActionResult<string>> GenerateShortLink([FromBody] SourceLink link)
    {
        var result = await _shorterer.GetShortLinkAsync(link);

        return Ok(result);
    }
}
