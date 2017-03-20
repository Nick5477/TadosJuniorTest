namespace Infrastructure.Db.Queries
{
    public interface IQueryBuilder
    {
        IQueryFor<TResult> For<TResult>();
    }
}