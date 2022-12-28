using LinksShorterer.Models;
using System.Linq.Expressions;

namespace LinksShorterer.Repositories;

public class LinkEntitySpecification : ISpecification<LinkEntity>
{

    public LinkEntitySpecification(string? shortName, int take, int? skip)
    {
        if (!string.IsNullOrEmpty(shortName))
        {
            Criteria = x => x.ShortName == shortName;
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
