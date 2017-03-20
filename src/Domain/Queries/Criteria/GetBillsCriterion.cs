namespace Domain.Queries.Criteria
{
    public class GetBillsCriterion:ICriterion
    {
        public int Offset { get; set; }
        public int Count { get; set; }
    }
}
