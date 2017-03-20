using System;
using System.Collections.Generic;
using Domain.Queries.Criteria;
using Domain.Services;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Bill.Queries
{
    class GetBillsQuery:IQuery<GetBillsCriterion,IEnumerable<Domain.Entities.Bill>>
    {
        private readonly IBillService _billService;

        public GetBillsQuery(IBillService billService)
        {
            if (billService==null)
                throw new ArgumentNullException(nameof(billService));
            _billService = billService;
        }

        public IEnumerable<Domain.Entities.Bill> Ask(GetBillsCriterion criterion)
        {
            List<Domain.Entities.Bill> bills=_billService.GetBills(criterion.Offset, criterion.Count);
            return bills;
        }
    }
}
