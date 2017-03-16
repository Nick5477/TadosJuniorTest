using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Commands.Contexts
{
    public class AddBillCommandContext:ICommandContext
    {
        public int ClientId { get; set; }
        public int Number { get; set; }
        public decimal Sum { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
        public string DisplayNumber { get; set; }
    }
}
