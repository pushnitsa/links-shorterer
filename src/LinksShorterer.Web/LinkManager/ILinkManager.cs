using LinksShorterer.Models;

namespace LinksShorterer.LinkManager;

public interface ILinkManager
{
    Task<string> CreateShortLinkAsync(SourceLink sourceLink);
    Task<string> GetFullUrlAsync(string shortLinkName);
}
