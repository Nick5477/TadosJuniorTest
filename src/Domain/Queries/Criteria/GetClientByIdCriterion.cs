using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries.Criteria
{
    public class GetClientByIdCriterion:ICriterion
    {
        public int Id { get; set; }
    }
}
