using LinksShorterer.Repositories;

namespace LinksShorterer.ShortLinkGenerator;

public class ShortLinkGeneratorService : IShortLinkGenerator
{
    private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int _shortUrlLength = 7;

    private readonly Random _random = new();
    private readonly Func<ILinkRepository> _linkRepositoryFactory;

    public ShortLinkGeneratorService(Func<ILinkRepository> linkRepositoryFactory)
    {
        _linkRepositoryFactory = linkRepositoryFactory;
    }

    public async Task<string> GenerateShortLinkAsync()
    {
        var charArray = new char[_shortUrlLength];
        string result;
        using var linkRepository = _linkRepositoryFactory();

        do
        {
            for (var i = 0; i < _shortUrlLength; i++)
            {
                var randomIndex = _random.Next(_alphabet.Length - 1);
                charArray[i] = _alphabet[randomIndex];
            }
            result = new string(charArray);
        } while (await linkRepository.IsLinkExistsAsync(result));

        return result;
    }
}
