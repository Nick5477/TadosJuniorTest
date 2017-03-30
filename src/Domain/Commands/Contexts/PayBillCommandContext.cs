namespace Domain.Commands.Contexts
{
    public class PayBillCommandContext:ICommandContext
    {
        public int Id { get; set; }
        public string DatabasePath { get; set; }
        public bool IsSuccess { get; set; }
    }
}
