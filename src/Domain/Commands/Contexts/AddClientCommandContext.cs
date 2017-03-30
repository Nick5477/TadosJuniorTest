namespace Domain.Commands.Contexts
{
    public class AddClientCommandContext:ICommandContext
    {
        public string Inn { get; set; }
        public string Name { get; set; }
        public string DatabasePath { get; set; }
    }
}
