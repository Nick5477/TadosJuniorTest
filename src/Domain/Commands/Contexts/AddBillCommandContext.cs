namespace Domain.Commands.Contexts
{
    public class AddBillCommandContext:ICommandContext
    {
        public int ClientId { get; set; }
        public decimal Sum { get; set; }
        public string DatabasePath { get; set; }
    }
}
