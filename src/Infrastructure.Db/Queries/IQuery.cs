using Domain.Queries.Criteria;

namespace Infrastructure.Db.Queries
{

    public interface IQuery<in TCriterion, out TResult>
        where TCriterion : ICriterion
    {
        TResult Ask(TCriterion criterion);
    }
}
