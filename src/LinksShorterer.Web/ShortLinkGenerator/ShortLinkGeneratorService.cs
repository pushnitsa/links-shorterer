namespace LinksShorterer.ShortLinkGenerator;

public class ShortLinkGeneratorService : IShortLinkGenerator
{
    private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int _shortUrlLength = 7;

    private readonly Random _random = new();

    public ShortLinkGeneratorService()
    {

    }

    public Task<string> GenerateShortLinkAsync()
    {
        var result = new char[_shortUrlLength];

        for (var i = 0; i < _shortUrlLength; i++)
        {
            var randomIndex = _random.Next(_alphabet.Length - 1);
            result[i] = _alphabet[randomIndex];
        }

        return Task.FromResult(new string(result));
    }
}
