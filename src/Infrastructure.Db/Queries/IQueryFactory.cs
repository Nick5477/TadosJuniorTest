using Domain.Queries.Criteria;

namespace Infrastructure.Db.Queries
{

    public interface IQueryFactory
    {
        IQuery<TCriterion, TResult> Create<TCriterion, TResult>() where TCriterion : ICriterion;
    }
}
