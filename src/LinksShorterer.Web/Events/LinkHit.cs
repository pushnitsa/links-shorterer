namespace LinksShorterer.Events;

public class LinkHit : IEvent
{
    public LinkHit(string id, string shortName)
    {
        Id = id;
        ShortName = shortName;
    }

    public string Id { get; set; }
    public string ShortName { get; set; }
}
