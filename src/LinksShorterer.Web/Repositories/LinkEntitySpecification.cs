using LinksShorterer.Models;
using System.Linq.Expressions;

namespace LinksShorterer.Repositories;

public class LinkEntitySpecification : ISpecification<LinkEntity>
{

    public LinkEntitySpecification(string? searchPhrase, int take, int? skip)
    {
        if (!string.IsNullOrEmpty(searchPhrase))
        {
            Criteria = x => x.ShortName.Contains(searchPhrase) || x.FullUrl.Contains(searchPhrase);
        }

        Take = take;

        if (skip.HasValue)
        {
            Skip = skip.Value;
        }
    }

    public Expression<Func<LinkEntity, bool>>? Criteria { get; }

    public int Take { get; set; }
    public int? Skip { get; set; }
}
