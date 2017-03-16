using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands.Contexts
{
    public class DeleteClientCommandContext:ICommandContext
    {
        public int Id { get; set; }
    }
}
