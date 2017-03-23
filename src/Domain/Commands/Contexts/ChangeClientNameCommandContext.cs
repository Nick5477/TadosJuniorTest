namespace Domain.Commands.Contexts
{
    public class ChangeClientNameCommandContext:ICommandContext
    {
        public int Id { get; set; }
        public string NewName { get; set; }
    }
}
