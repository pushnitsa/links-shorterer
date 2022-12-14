using LinksShorterer.Models;

namespace LinksShorterer.ShortererService;

public interface IShorterer
{
    Task<string> GetShortLinkAsync(SourceLink link);
}
