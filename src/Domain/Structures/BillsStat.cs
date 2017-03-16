using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Structures
{
    public struct BillsStat
    {
        public int TotalCount;
        public int PayedCount;
        public int UnpayedCount;
        public decimal TotalSum;
        public decimal PayedSum;
        public decimal UnpayedSum;
    }
}
