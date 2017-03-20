using System;
using System.Collections.Generic;
using Domain.Queries.Criteria;
using Domain.Services;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Bill.Queries
{
    class GetClientBillsQuery:IQuery<GetClientBillsCriterion,IEnumerable<Domain.Entities.Bill>>
    {
        private readonly IBillService _billService;

        public GetClientBillsQuery(IBillService billService)
        {
            if (billService==null)
                throw new ArgumentNullException(nameof(billService));
            _billService = billService;
        }
        public IEnumerable<Domain.Entities.Bill> Ask(GetClientBillsCriterion criterion)
        {
            List<Domain.Entities.Bill> clientBills =
                _billService.GetClientBills(criterion.Id, 
                criterion.Offset,
                criterion.Count);
            return clientBills;
        }
    }
}
