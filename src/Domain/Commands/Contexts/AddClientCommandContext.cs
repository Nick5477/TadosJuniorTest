using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Commands.Contexts
{
    public class AddClientCommandContext:ICommandContext
    {
        public string Inn { get; set; }
        public string Name { get; set; }
    }
}
