namespace Domain.Queries.Criteria
{
    public class GetClientBillsCriterion:ICriterion
    {
        public int Id { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
    }
}
