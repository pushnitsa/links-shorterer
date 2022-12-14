using LinksShorterer.Models;
using LinksShorterer.ShortererService;
using Microsoft.AspNetCore.Mvc;

namespace LinksShorterer.Controllers;

[ApiController]
[Route("api")]
public class LinksController : ControllerBase
{
    private readonly IShorterer _shorterer;
    private readonly IRedirector _redirector;

    public LinksController(IShorterer shorterer, IRedirector redirector)
    {
        _shorterer = shorterer;
        _redirector = redirector;
    }

    [HttpPost]
    [Route("generate")]
    public async Task<ActionResult<string>> GenerateShortLink([FromBody] SourceLink link)
    {
        var result = await _shorterer.GetShortLinkAsync(link);

        return Ok(result);
    }

    [HttpGet]
    [Route("~/{shortLinkName}")]
    public async Task<ActionResult> FollowTheLink([FromRoute] string shortLinkName)
    {
        var result = await _redirector.GetUrlAsync(shortLinkName);

        return RedirectPermanent(result);
    }
}
