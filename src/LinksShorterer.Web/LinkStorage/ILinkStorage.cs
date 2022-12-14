namespace LinksShorterer.LinkStorage;

public interface ILinkStorage
{
    Task<string> CreateShortLinkAsync(string fullUrl, bool isPermanent, DateTime? expirationDate);
    Task<string> CreateShortLinkAsync(string fullUrl, string shortLinkName, bool isPermanent, DateTime? expirationDate);
    Task<string> GetFullUrlAsync(string shortLinkName);
}
