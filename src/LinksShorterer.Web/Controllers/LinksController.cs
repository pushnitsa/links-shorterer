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
    public async Task<ActionResult<ResultLink>> GenerateShortLink([FromBody] SourceLink link)
    {
        var result = new ResultLink();

        try
        {
            var shortLink = await _shorterer.GetShortLinkAsync(link);
            result.ShortLink = shortLink;

        }
        catch (InvalidOperationException ex)
        {
            result.Errors.Add(ex.Message);
        }

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
