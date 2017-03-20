using Domain.Queries.Criteria;

namespace Infrastructure.Db.Queries
{ 
    public interface IQueryFor<out TResult>
    {
        TResult With<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion;
    }
}
