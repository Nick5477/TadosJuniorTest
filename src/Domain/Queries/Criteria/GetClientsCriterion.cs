namespace Domain.Queries.Criteria
{
    public class GetClientsCriterion:ICriterion
    {
        public int Offset { get; set; }
        public int Count { get; set; }
    }
}
