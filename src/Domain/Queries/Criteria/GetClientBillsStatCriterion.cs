﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries.Criteria
{
    public class GetClientBillsStatCriterion:ICriterion
    {
        public int ClientId { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }

    }
}