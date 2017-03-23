namespace Domain.Queries.Criteria
{
    public class GetPayedBillsSumCriterion:ICriterion
    {
        public int Count { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }

    }
}
