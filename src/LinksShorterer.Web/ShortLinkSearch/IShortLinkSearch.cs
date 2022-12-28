using LinksShorterer.Models;

namespace LinksShorterer.ShortLinkSearch;

public interface IShortLinkSearch
{
    Task<ShortLinkSearchResult> SearchAsync(ShortLinkSearchCriteria searchCriteria);
}
