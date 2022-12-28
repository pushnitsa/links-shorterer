using LinksShorterer.Models;

namespace LinksShorterer.LinkManager;

public interface ILinkManager
{
    Task<string> CreateShortLinkAsync(Link sourceLink);
    Task<string> GetFullUrlAsync(string shortLinkName);
}
