using LinksShorterer.Models;
using LinksShorterer.ShortererService;
using LinksShorterer.ShortLinkSearch;
using Microsoft.AspNetCore.Mvc;

namespace LinksShorterer.Controllers;

[ApiController]
[Route("api")]
public class LinksController : ControllerBase
{
    private readonly IShorterer _shorterer;
    private readonly IRedirector _redirector;
    private readonly IShortLinkSearch _shortLinkSearch;

    public LinksController(IShorterer shorterer, IRedirector redirector, IShortLinkSearch shortLinkSearch)
    {
        _shorterer = shorterer;
        _redirector = redirector;
        _shortLinkSearch = shortLinkSearch;
    }

    [HttpPost]
    [Route("generate")]
    public async Task<ActionResult<ResultLink>> GenerateShortLink([FromBody] Link link)
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

    [HttpPost]
    [Route("search")]
    public async Task<ActionResult<ShortLinkSearchResult>> SearchShortLinks([FromBody] ShortLinkSearchCriteria searchCriteria)
    {
        var result = await _shortLinkSearch.SearchAsync(searchCriteria);

        return Ok(result);
    }

    [HttpGet]
    [Route("~/{shortLinkName}")]
    public async Task<ActionResult> FollowTheLink([FromRoute] string shortLinkName)
    {
        string result;

        try
        {
            result = await _redirector.GetUrlAsync(shortLinkName);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        return RedirectPermanent(result);
    }
}
