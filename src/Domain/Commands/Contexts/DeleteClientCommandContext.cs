namespace Domain.Commands.Contexts
{
    public class DeleteClientCommandContext:ICommandContext
    {
        public int Id { get; set; }
        public string DatabasePath { get; set; }
        public bool IsSuccess { get; set; }
    }
}
