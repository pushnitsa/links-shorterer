namespace LinksShorterer.ShortLinkGenerator;

public interface IShortLinkGenerator
{
    Task<string> GenerateShortLinkAsync();
}
